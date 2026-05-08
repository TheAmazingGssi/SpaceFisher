using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void GoToMiniGame()
    {
        SceneManager.LoadScene(Constants.Scenes.Minigame);
    }
}
