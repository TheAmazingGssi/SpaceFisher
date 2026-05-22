using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfMinigameUIManager : MonoBehaviour
{
    [SerializeField] GameObject ParentObject;

    private void OnEnable()
    {
        Bus<MinigameStart>.OnEvent += OnMinigameStart;
        Bus<MinigameEnd>.OnEvent += OnMinigameEnd;
    }
    private void OnDisable()
    {
        Bus<MinigameStart>.OnEvent -= OnMinigameStart;
        Bus<MinigameEnd>.OnEvent -= OnMinigameEnd;
    }

    private void OnMinigameStart(MinigameStart e)
    {
        StartCoroutine(SetMenuActiveNextFrame(false));
    }
    private void OnMinigameEnd(MinigameEnd e)
    {
        ParentObject.SetActive(true);
    }

    public void GoToAquariumScene()
    {
        SceneManager.LoadScene(Constants.Scenes.Ship);
    }
    IEnumerator SetMenuActiveNextFrame(bool value)
    {
        yield return null;
        ParentObject.SetActive(value);
    }
}
