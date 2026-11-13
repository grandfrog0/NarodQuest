using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void GoScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void GoScene(string text)
    {
        SceneManager.LoadScene(text);
    }
}
