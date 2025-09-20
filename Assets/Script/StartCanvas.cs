using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartCanvas : MonoBehaviour
{
   public void StartGameButton(){
    SceneManager.LoadScene(1);
   }
   public void QuitGameButton(){
    Application.Quit();
   }
}
