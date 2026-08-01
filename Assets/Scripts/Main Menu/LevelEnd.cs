using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool levelEnding = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || levelEnding)
            return;

        levelEnding = true;

        // Unlock the next level based on the completed level's build index
        LevelSelection.UnlockNextLevel(SceneManager.GetActiveScene().buildIndex);

        if (animator == null && LevelUIManager.Instance != null)
        {
            animator = LevelUIManager.Instance.animator;
        }

        if (animator != null)
            animator.Play("LevelOutro");

        if (LevelUIManager.Instance != null)
        {
            LevelUIManager.Instance.CompleteLevel();
        }
        else
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
    }
}