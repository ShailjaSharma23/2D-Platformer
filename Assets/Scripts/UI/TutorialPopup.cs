using UnityEngine;
using TMPro;

public class TutorialPopup : MonoBehaviour
{
    private static TutorialPopup instance;
    public static TutorialPopup Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TutorialPopup>();
                if (instance == null)
                {
                    GameObject canvasGo = GameObject.Find("UI Canvas");
                    if (canvasGo != null)
                    {
                        instance = canvasGo.AddComponent<TutorialPopup>();
                        instance.InitializeDynamicPopup();
                    }
                }
            }
            return instance;
        }
    }

    [SerializeField] private GameObject popupPanel;
    [SerializeField] private TextMeshProUGUI tutorialText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            InitializeDynamicPopup();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void InitializeDynamicPopup()
    {
        if (popupPanel == null)
        {
            Transform panelTrans = transform.Find("TutorialPanel");
            if (panelTrans != null)
            {
                popupPanel = panelTrans.gameObject;
            }
        }

        if (tutorialText == null && popupPanel != null)
        {
            Transform textTrans = popupPanel.transform.Find("TutorialText");
            if (textTrans != null)
            {
                tutorialText = textTrans.GetComponent<TextMeshProUGUI>();
            }
        }

        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
        }
    }

    public void ShowTutorial(string message)
    {
        if (tutorialText != null)
        {
            tutorialText.text = message;
        }
        if (popupPanel != null)
        {
            popupPanel.SetActive(true);
        }
    }

    public void HideTutorial()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
        }
    }
}
