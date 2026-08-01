using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    private const string HIGHEST_LEVEL_KEY = "HighestUnlockedLevel";

    [Header("Lock Settings")]
    [SerializeField] private Sprite lockSprite;

#if UNITY_EDITOR
    private void Awake()
    {
        CopyLockIconInEditor();
        if (lockSprite == null)
        {
            lockSprite = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/lock_icon.jpg");
        }
    }

    private void CopyLockIconInEditor()
    {
        string destPath = "Assets/Sprites/lock_icon.jpg";
        if (!System.IO.File.Exists(destPath))
        {
            string srcPath = @"C:\Users\shail\.gemini\antigravity\brain\3cce9f72-334f-4b2a-98b0-bde54ed80f1b\lock_icon_1785522823594.jpg";
            if (System.IO.File.Exists(srcPath))
            {
                try
                {
                    System.IO.Directory.CreateDirectory("Assets/Sprites");
                    System.IO.File.Copy(srcPath, destPath, true);
                    UnityEditor.AssetDatabase.Refresh();
                    Debug.Log("Lock icon successfully copied to Assets/Sprites/lock_icon.jpg");
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Error copying lock icon: " + e.Message);
                }
            }
        }
    }
#endif

    private void Start()
    {
        UpdateLevelButtons();
    }

    public static int GetHighestUnlockedLevel()
    {
        return PlayerPrefs.GetInt(HIGHEST_LEVEL_KEY, 1);
    }

    public static void UnlockNextLevel(int completedLevelIndex)
    {
        int currentHighest = GetHighestUnlockedLevel();
        if (completedLevelIndex >= currentHighest)
        {
            PlayerPrefs.SetInt(HIGHEST_LEVEL_KEY, completedLevelIndex + 1);
            PlayerPrefs.Save();
            Debug.Log($"Level progress saved: Unlocked level {completedLevelIndex + 1}");
        }
    }

    public static void ResetProgress()
    {
        PlayerPrefs.DeleteKey(HIGHEST_LEVEL_KEY);
        PlayerPrefs.Save();
        Debug.Log("Level progression progress reset.");
    }

    public void UpdateLevelButtons()
    {
        int highestUnlocked = GetHighestUnlockedLevel();
        Transform levelsParent = transform.Find("Levels");

        if (levelsParent != null)
        {
            Button[] buttons = levelsParent.GetComponentsInChildren<Button>(true);
            foreach (Button btn in buttons)
            {
                string name = btn.gameObject.name;
                if (name.StartsWith("level"))
                {
                    string numStr = name.Substring(5);
                    if (int.TryParse(numStr, out int levelNum))
                    {
                        bool isUnlocked = levelNum <= highestUnlocked;
                        btn.interactable = isUnlocked;

                        if (!isUnlocked)
                        {
                            ApplyLockVisuals(btn);
                        }
                    }
                }
            }
        }
    }

    private void ApplyLockVisuals(Button btn)
    {
        // 1. Instantiates lock icon overlay if lockSprite is available
        if (lockSprite != null)
        {
            Transform existingLock = btn.transform.Find("LockIcon");
            if (existingLock == null)
            {
                GameObject lockIconObj = new GameObject("LockIcon");
                lockIconObj.transform.SetParent(btn.transform, false);
                
                Image lockImage = lockIconObj.AddComponent<Image>();
                lockImage.sprite = lockSprite;
                
                RectTransform rect = lockImage.rectTransform;
                rect.anchoredPosition = Vector2.zero;
                rect.sizeDelta = new Vector2(30, 30);
                
                lockImage.color = Color.white;
            }
        }

        // 2. Tints the level button itself to be darker and semi-transparent
        Image btnImage = btn.GetComponent<Image>();
        if (btnImage != null)
        {
            btnImage.color = new Color(0.4f, 0.4f, 0.4f, 0.8f);
        }
    }

    public void LoadLevel(int levelIndex)
    {
        // Safeguard loading levels via index checks
        // Level index 0 is Main Menu, Level 1 is always unlocked.
        if (levelIndex == 0 || levelIndex <= GetHighestUnlockedLevel())
        {
            SceneManager.LoadScene(levelIndex);
        }
        else
        {
            Debug.LogWarning($"Cannot load level {levelIndex} as it is currently locked!");
        }
    }
}

