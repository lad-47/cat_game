using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sceneName = "";

    private void Update() {}

    public void LoadTargetScene()
    {
        print("hi2");
        SceneManager.LoadScene(sceneName);
    }
}
