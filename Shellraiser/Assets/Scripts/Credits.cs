using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public void Back()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
