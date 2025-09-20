using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    [Header("组件")]
    public Rigidbody2D rb;
    public Animator animator_player;
    public Animator animator_slash;
    public Animator animator_sword;
    public SpriteRenderer spriteRenderer;
    public GameObject swordTrigger;
    public Transform swordPos;
    public Transform owner;
    public Slider hpBar;
    public GameObject DeathVFX;
    public GameObject DeadUI;
    [Header("属性")]
    public float speed = 5f;
    public int AttackDamage = 5;
    public float hp=100;
    public float mxhp=100;
    public float h, v;
    public bool isDead=false;
    public AudioSource AttackSound;
    void Start()
    {
        hp =mxhp;
        hpBar.value = hp/mxhp;
    }


    void Update()
    {
        if(!isDead){
        Move();
        anim();
        Attack();
        }
        else{
        rb.linearVelocity=Vector2.zero;
        }
    }
    public void Move()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        rb.linearVelocity = new Vector2(h * speed, v * speed);
        if (h < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (h > 0)
        {
            spriteRenderer.flipX = false;
        }
    }
    public void anim()
    {
        if (h != 0 || v != 0)
        {
            animator_player.SetBool("idle_run", true);
        }
        else
        {
            animator_player.SetBool("idle_run", false);
        }
    }
    public void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator_slash.SetTrigger("IsAttack1");
            animator_sword.SetTrigger("Attack1");
            GameObject go = Instantiate(swordTrigger, swordPos.position, swordPos.rotation, swordPos);
            go.GetComponent<PlayerAttackTrigger>().SetDamage(AttackDamage + (int)Random.Range(0, 5),transform);
            AttackSound.PlayOneShot(AttackSound.clip);

        }
    }
    public void TakeDamage(int damage,Transform owner)
    {
        CancelInvoke(nameof(GetHitEnd));
        animator_player.SetBool("GetHit", true);
        Invoke(nameof(GetHitEnd), 0.24f);
        hp -= damage;
        hpBar.value = hp/mxhp;
        if(hp<=0)
        {
            Instantiate(DeathVFX, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            isDead = true;
            DeadUI.SetActive(true);
        }
        Debug.Log($"Player took {damage} damage!");
    }
    public void GetHitEnd()
    {
        animator_player.SetBool("GetHit", false);
    }
}
