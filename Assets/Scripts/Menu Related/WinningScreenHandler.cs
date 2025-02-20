using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningScreenHandler : MonoBehaviour
{
    public void OnReturnToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
        AudioInterface.Instance.StopThemeMusic();
    }
}
