using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private CreditsPage creditsPage;

    private void Start()
    {
        ResetUI();
        AudioInterface.Instance.PlayStoryTheme();
    }

    public void OnPlayPress()
    {
        // Load the next scene after main menu
        SceneManager.LoadScene(1);
    }

    public void OnCreditsPress()
    {
        creditsPage.OpenCreditsPage();
    }

    public void OnQuitPress()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
    
    private void ResetUI() {
        creditsPage.gameObject.SetActive(true);
    }
}
