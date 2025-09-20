using UnityEngine;
using UnityEngine.Tilemaps;
public class GameObjectHide : MonoBehaviour
{
    public Tilemap tilemap;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            tilemap.color = new Color(1f, 1f, 1f, 0.95f);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            tilemap.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
