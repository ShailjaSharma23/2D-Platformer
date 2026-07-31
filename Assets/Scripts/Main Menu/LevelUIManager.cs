using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUIManager : MonoBehaviour
{
    public static LevelUIManager Instance;

    public TMP_Text levelText;
    public Animator animator;

    private void Awake()
    {
        Instance = this;
        if (levelText == null)
            levelText = GetComponent<TMP_Text>();
        if (levelText == null)
            levelText = GetComponentInChildren<TMP_Text>(true);

        if (animator == null)
            animator = GetComponent<Animator>();

        // Disable other child TMP_Text GameObjects to avoid duplicate/overlapping text from manual scene edits
        if (levelText != null)
        {
            foreach (TMP_Text childText in GetComponentsInChildren<TMP_Text>(true))
            {
                if (childText != levelText)
                {
                    childText.gameObject.SetActive(false);
                }
            }
        }
    }

    void Start()
    {
        if (levelText != null)
            levelText.text = "LEVEL " + SceneManager.GetActiveScene().buildIndex;
        if (animator != null)
            animator.Play("LevelIntro");
    }

    public void CompleteLevel()
    {
        StartCoroutine(LevelCompleteRoutine());
    }

    IEnumerator LevelCompleteRoutine()
    {
        if (levelText != null)
            levelText.text = "LEVEL COMPLETE!";
        if (animator != null)
            animator.Play("LevelOutro");

        yield return new WaitForSeconds(2.5f);

        int next = SceneManager.GetActiveScene().buildIndex + 1;

        if (next < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(next);
        else
            SceneManager.LoadScene(0);
    }
}