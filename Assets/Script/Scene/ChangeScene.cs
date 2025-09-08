using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private SceneName sceneName;

    public void Change()
    {
        SceneManager.LoadScene(sceneName.ToString());
    }
}

public enum SceneName
{
    GameScene,
    MenuScene
}

