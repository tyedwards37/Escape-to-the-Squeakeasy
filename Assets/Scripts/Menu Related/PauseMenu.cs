using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] CreditsPage creditsPage;

    private void OnEnable()
    {
        creditsPage.ImmediatelyCloseCreditsPage();
    }

    public void ResumeGame()
    {
        Game.Instance.TogglePause();
    }

    public void ViewCredits()
    {
        creditsPage.OpenCreditsPage();
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
