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
    }

    void Start()
    {
        levelText.text = "LEVEL " + SceneManager.GetActiveScene().buildIndex;
        animator.Play("LevelIntro");
    }

    public void CompleteLevel()
    {
        StartCoroutine(LevelCompleteRoutine());
    }

    IEnumerator LevelCompleteRoutine()
    {
        levelText.text = "LEVEL COMPLETE!";
        animator.Play("LevelComplete");

        yield return new WaitForSeconds(2.5f);

        int next = SceneManager.GetActiveScene().buildIndex + 1;

        if (next < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(next);
    }
}