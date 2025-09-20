using UnityEngine;
using UnityEngine.UI;

public class CursorImage : MonoBehaviour
{
    public Image cursorImage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            cursorImage.color = Color.red;
            transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            cursorImage.color = Color.white;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
