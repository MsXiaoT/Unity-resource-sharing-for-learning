
using System;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Random = UnityEngine.Random;

public class EnemyBase : MonoBehaviour
{   
    [Header("组件")]
    public Animator animator_enemy;
    public GameObject EnemyAttackPosition;
    public Transform AttackPos;
    public Rigidbody2D rb;
    public Player player;
    public Transform owner;
    public Slider hpBar;
    public GameObject DeathShader;
    [Header("属性")]
    public float hp=50;
    public float maxhp=50;
    public int damage = 5;
    public bool CanAttack = true;
    public bool IsRange = false;
    [Header("怪物移动")]
    public Transform[] movPos;
    public float moveSpeed= 3.0f;
    public Transform target;
    public bool waitidle = false;
    public void Start()
    {
        hp = maxhp;
        hpBar.value = hp/maxhp;
        hpBar.gameObject.SetActive(false);
        if(movPos.Length>1){
            target = GetTarget();
        }
    }
    public void Update()
    {
        CreateAttackTrigger();
        if(!animator_enemy.GetBool("GetHit"))
        Move();
        Death();
    }
    public void TakeDamage(int damage,Transform owner)
    {

        CancelInvoke(nameof(GetHitEnd));
        CancelInvoke(nameof(Hpbarhit));
        Vector2 dir = (transform.position - owner.position).normalized;
        rb.linearVelocity = dir * 10f;
        hp-= damage;
        hpBar.value = hp/maxhp;
        hpBar.gameObject.SetActive(true);
        animator_enemy.SetBool("GetHit", true);
        Invoke(nameof(GetHitEnd), 0.24f);
        Invoke(nameof(Hpbarhit),2f);
    }
    public void Hpbarhit()
    {
        hpBar.gameObject.SetActive(false);
    }
    public void GetHitEnd()
    {
        animator_enemy.SetBool("GetHit", false);
    }
    public void SetDamage(int Num,Transform owner)
    {
        damage = Num;
        this.owner = owner;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IsRange = true;
            player = other.GetComponent<Player>();
        }
    }
    public void CreateAttackTrigger()
    {
        if (IsRange)
        {
            if (CanAttack)
            {
                CancelInvoke(nameof(CanAttackEnd));
                GameObject go = Instantiate(EnemyAttackPosition, transform.position, AttackPos.rotation, AttackPos);
                go.GetComponent<SlimeAttackTrigger>().SetDamage(damage + (int)Random.Range(0, 5),transform);
                CanAttack = false;
                Invoke(nameof(CanAttackEnd), 0.3f);
            }
            if (Vector2.Distance(transform.position, player.transform.position) > 2f)
            {
                IsRange = false;
            }
        }
    }
    public void CanAttackEnd()
    {
        CanAttack = true;
    }
    public void Death(){
        if(hp<=0){     
            GameObject go=Instantiate(DeathShader,transform.position,transform.rotation);
            Destroy(gameObject);
            Destroy(go,2f);
        }
    }
    public void Move(){
        if(waitidle){
            animator_enemy.SetBool("Run",false);
            return;
            }
        Vector3 dir = (target.position - transform.position).normalized;
        if(Vector3.Distance(transform.position,target.position)>0.5f){
            rb.linearVelocity = dir * moveSpeed;
            animator_enemy.SetBool("Run",true);
        }
        else{
            target = GetTarget();
            rb.linearVelocity = Vector2.zero;
            waitidle = true;
            Invoke(nameof(SetWaitIdle),4.0f);
        }
    }
    public void SetWaitIdle(){
        waitidle = false;

    }
    public Transform GetTarget(){
        if(movPos.Length<2){
            return transform;
        }
        Transform tar = movPos[Random.Range(0,movPos.Length)];
        if(tar==target){
            return GetTarget();
        }
        else{
            return tar;
        }
}
}