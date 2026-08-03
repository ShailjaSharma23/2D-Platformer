using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsPage;
    [SerializeField] private GameObject levelsPage;
    [SerializeField] private GameObject analyticsPage;
    [SerializeField] private GameObject menuPage;

    private void Awake()
    {
        settingsPage.SetActive(false);
        levelsPage.SetActive(false);
        analyticsPage.SetActive(false);
        menuPage.SetActive(true);
    }

    #region Main Menu

    public void StartGame()
    {
        levelsPage.SetActive(true);
    }

    public void Settings()
    {
        settingsPage.SetActive(true);
    }

    public void Analytics()
    {
        analyticsPage.SetActive(true);
    }

    #endregion


    #region Game Actions

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void GoBack()
    {
        SceneManager.LoadScene(0);
    }
    public void GoBackToMainMenu()
    {
        settingsPage.SetActive(false);
        levelsPage.SetActive(false);
        analyticsPage.SetActive(false);
        menuPage.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    #endregion
    
}