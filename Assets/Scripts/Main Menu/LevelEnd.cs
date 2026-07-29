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

        animator.SetTrigger("LevelOutro");
    }

    public void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
    }
}