using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeadUi : MonoBehaviour
{
    public void RestartButton(){
        SceneManager.LoadScene(1);
    }
    public void BackMainButton(){
        SceneManager.LoadScene(0);
    }
}
