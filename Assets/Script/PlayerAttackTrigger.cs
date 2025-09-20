using UnityEngine;

public class PlayerAttackTrigger : MonoBehaviour
{
    public Transform owner;
    public int damage = 5;
    public void Start()
    {
        Destroy(gameObject, 0.2f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyBase>().TakeDamage(damage,owner);
        }
    }
    public void SetDamage(int Num,Transform owner)
    {
        damage = Num;
        this.owner = owner;
    }
}
