using UnityEngine;

public class SlimeAttackTrigger : MonoBehaviour
{
    public int damage = 5;
    public Transform owner;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().TakeDamage(damage,owner);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, 0.2f);
        }
    }
    public void SetDamage(int Num,Transform owner)
    {
        damage = Num;
        this.owner = owner;
    }
}
