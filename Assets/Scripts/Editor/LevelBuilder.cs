// using UnityEngine;
// using UnityEditor;
// using UnityEditor.SceneManagement;
// using TMPro;

// public class LevelBuilder
// {
//     [MenuItem("Tools/Build Level 1")]
//     public static void BuildLevel1()
//     {
//         // 1. Open the scene
//         string scenePath = "Assets/Levels/Level 1.unity";
//         var scene = EditorSceneManager.OpenScene(scenePath);
//         if (!scene.IsValid())
//         {
//             Debug.LogError("Failed to open scene: " + scenePath);
//             return;
//         }

//         // 2. Find parent Level GameObject
//         GameObject levelParent = GameObject.Find("Level");
//         if (levelParent == null)
//         {
//             Debug.LogError("Failed to find GameObject named 'Level'");
//             return;
//         }

//         // 3. Find Room 1 and Room 2
//         Transform room1Trans = levelParent.transform.Find("Room 1");
//         Transform room2Trans = levelParent.transform.Find("Room 2");

//         if (room1Trans == null || room2Trans == null)
//         {
//             Debug.LogError("Failed to find Room 1 or Room 2 in Level parent");
//             return;
//         }

//         // Let's delete existing Room 3, Room 4, Room 5 if they exist, so we can rebuild cleanly
//         string[] roomsToClean = { "Room 3", "Room 4", "Room 5" };
//         foreach (string name in roomsToClean)
//         {
//             Transform t = levelParent.transform.Find(name);
//             if (t != null)
//             {
//                 Object.DestroyImmediate(t.gameObject);
//             }
//         }

//         // 4. Load prefabs
//         GameObject rangedEnemyPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Enemy/Ranged Enemy Holder.prefab");
//         GameObject checkpointPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Collectibles/Checkpoint.prefab");
//         GameObject healthPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Collectibles/HealthCollectible.prefab");
//         GameObject boxPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Obstacles/Box.prefab");

//         // 5. Build Tutorial UI on UI Canvas
//         ConfigureTutorialUI();

//         // 6. Create Room 3 (Double-Jump Tutorial)
//         GameObject room3Go = Object.Instantiate(room2Trans.gameObject, levelParent.transform);
//         room3Go.name = "Room 3";
//         room3Go.transform.position = new Vector3(38.86f, 0f, 0f);
//         Room room3Comp = room3Go.GetComponent<Room>();
//         ClearRoomEntities(room3Go);

//         // Add platforms for double-jump in Room 3
//         if (boxPrefab != null)
//         {
//             // Platform 1
//             GameObject box1 = (GameObject)PrefabUtility.InstantiatePrefab(boxPrefab, room3Go.transform);
//             box1.transform.localPosition = new Vector3(-3f, -2f, 0f);
//             box1.transform.localScale = new Vector3(3f, 0.5f, 1f);

//             // Platform 2 (higher)
//             GameObject box2 = (GameObject)PrefabUtility.InstantiatePrefab(boxPrefab, room3Go.transform);
//             box2.transform.localPosition = new Vector3(2f, 0f, 0f);
//             box2.transform.localScale = new Vector3(3f, 0.5f, 1f);
//         }

//         // Add health collectibles in Room 3
//         if (healthPrefab != null)
//         {
//             GameObject health1 = (GameObject)PrefabUtility.InstantiatePrefab(healthPrefab, room3Go.transform);
//             health1.transform.localPosition = new Vector3(-3f, -1.2f, 0f);

//             GameObject health2 = (GameObject)PrefabUtility.InstantiatePrefab(healthPrefab, room3Go.transform);
//             health2.transform.localPosition = new Vector3(2f, 0.8f, 0f);
//         }

//         // Add Room 3 Tutorial Trigger
//         CreateTutorialTrigger(room3Go, new Vector3(-4f, -2.5f, 0f), new Vector2(6f, 6f), 
//             "Press Space twice in mid-air\nto perform a Double Jump!");

//         // 7. Create Room 4 (Ranged Combat)
//         GameObject room4Go = Object.Instantiate(room2Trans.gameObject, levelParent.transform);
//         room4Go.name = "Room 4";
//         room4Go.transform.position = new Vector3(58.29f, 0f, 0f);
//         Room room4Comp = room4Go.GetComponent<Room>();
//         ClearRoomEntities(room4Go);

//         // Add high platform for Ranged Enemy in Room 4
//         if (boxPrefab != null)
//         {
//             GameObject enemyPlatform = (GameObject)PrefabUtility.InstantiatePrefab(boxPrefab, room4Go.transform);
//             enemyPlatform.transform.localPosition = new Vector3(2f, -1.5f, 0f);
//             enemyPlatform.transform.localScale = new Vector3(3f, 0.5f, 1f);
//         }

//         // Add Ranged Enemy in Room 4
//         GameObject enemyHolder = null;
//         if (rangedEnemyPrefab != null)
//         {
//             enemyHolder = (GameObject)PrefabUtility.InstantiatePrefab(rangedEnemyPrefab, room4Go.transform);
//             enemyHolder.transform.localPosition = new Vector3(2f, -1.0f, 0f);
//             SetRoomEnemies(room4Comp, new GameObject[] { enemyHolder });
//         }

//         // Add Room 4 Tutorial Trigger
//         CreateTutorialTrigger(room4Go, new Vector3(-5f, -2.5f, 0f), new Vector2(6f, 6f), 
//             "Left-Click to shoot Fireballs\nto take down distant enemies!");

//         // 8. Create Room 5 (Gatehouse Exit)
//         GameObject room5Go = Object.Instantiate(room2Trans.gameObject, levelParent.transform);
//         room5Go.name = "Room 5";
//         room5Go.transform.position = new Vector3(77.72f, 0f, 0f);
//         Room room5Comp = room5Go.GetComponent<Room>();
//         ClearRoomEntities(room5Go);

//         // Add Checkpoint in Room 5
//         if (checkpointPrefab != null)
//         {
//             GameObject checkpoint = (GameObject)PrefabUtility.InstantiatePrefab(checkpointPrefab, room5Go.transform);
//             checkpoint.transform.localPosition = new Vector3(-2f, -3.0f, 0f);
//         }

//         // Remove door in Room 5
//         Transform doorInRoom5 = room5Go.transform.Find("Door");
//         if (doorInRoom5 != null)
//         {
//             Object.DestroyImmediate(doorInRoom5.gameObject);
//         }

//         // Add Room 5 Tutorial Trigger
//         CreateTutorialTrigger(room5Go, new Vector3(-4f, -2.5f, 0f), new Vector2(6f, 6f), 
//             "Touch Checkpoints to save progress.\nThis is the end of the outskirts!");

//         // 9. Configure Tutorial Triggers for Room 1 and Room 2
//         CreateTutorialTrigger(room1Trans.gameObject, new Vector3(-3f, -3f, 0f), new Vector2(8f, 6f),
//             "Move left/right with A/D.\nPress Space to Jump.");

//         CreateTutorialTrigger(room2Trans.gameObject, new Vector3(-3f, -3f, 0f), new Vector2(6f, 6f),
//             "Press E to perform a Sword Attack\nto defeat the Castle Guard.");

//         // 10. Configure doors and transitions
//         ConfigureDoor(room1Trans.Find("Door"), room1Trans, room2Trans);
//         ConfigureDoor(room2Trans.Find("Door"), room2Trans, room3Go.transform);
//         ConfigureDoor(room3Go.transform.Find("Door"), room3Go.transform, room4Go.transform);
//         ConfigureDoor(room4Go.transform.Find("Door"), room4Go.transform, room5Go.transform);

//         // Set room occlusion states at start
//         room3Comp.ActivateRoom(false);
//         room4Comp.ActivateRoom(false);
//         room5Comp.ActivateRoom(false);

//         // Mark scene dirty and save
//         EditorUtility.SetDirty(levelParent);
//         EditorSceneManager.MarkSceneDirty(scene);
//         EditorSceneManager.SaveScene(scene);

//         Debug.Log("Level 1 successfully built with 5 rooms, doors, and dynamic tutorial triggers!");
//     }

//     private static void ClearRoomEntities(GameObject roomGo)
//     {
//         for (int i = roomGo.transform.childCount - 1; i >= 0; i--)
//         {
//             Transform child = roomGo.transform.GetChild(i);
//             string name = child.name;
//             if (name != "Ground" && name != "WallRight" && name != "Door" && !name.StartsWith("Ground") && !name.StartsWith("Wall"))
//             {
//                 Object.DestroyImmediate(child.gameObject);
//             }
//         }

//         Room room = roomGo.GetComponent<Room>();
//         if (room != null)
//         {
//             SetRoomEnemies(room, new GameObject[0]);
//         }
//     }

//     private static void SetRoomEnemies(Room room, GameObject[] enemies)
//     {
//         var so = new SerializedObject(room);
//         SerializedProperty prop = so.FindProperty("enemies");
//         if (prop != null && prop.isArray)
//         {
//             prop.ClearArray();
//             prop.arraySize = enemies.Length;
//             for (int i = 0; i < enemies.Length; i++)
//             {
//                 prop.GetArrayElementAtIndex(i).objectReferenceValue = enemies[i];
//             }
//             so.ApplyModifiedProperties();
//         }
//     }

//     private static void ConfigureDoor(Transform doorTrans, Transform prevRoom, Transform nextRoom)
//     {
//         if (doorTrans == null) return;
//         Door door = doorTrans.GetComponent<Door>();
//         if (door == null) return;

//         var so = new SerializedObject(door);
//         SerializedProperty prevProp = so.FindProperty("prevRoom");
//         SerializedProperty nextProp = so.FindProperty("nextRoom");
//         SerializedProperty camProp = so.FindProperty("cam");

//         if (prevProp != null) prevProp.objectReferenceValue = prevRoom;
//         if (nextProp != null) nextProp.objectReferenceValue = nextRoom;
        
//         if (camProp != null && camProp.objectReferenceValue == null)
//         {
//             CameraController cam = Object.FindAnyObjectByType<CameraController>();
//             if (cam != null)
//             {
//                 camProp.objectReferenceValue = cam;
//             }
//         }

//         so.ApplyModifiedProperties();
//         doorTrans.localPosition = new Vector3(9.22f, -1.49f, 0f);
//     }

//     private static void CreateTutorialTrigger(GameObject roomGo, Vector3 localPos, Vector2 triggerSize, string message)
//     {
//         // Check if trigger already exists in this room, clean it up if so
//         Transform oldTrigger = roomGo.transform.Find("TutorialTrigger");
//         if (oldTrigger != null)
//         {
//             Object.DestroyImmediate(oldTrigger.gameObject);
//         }

//         GameObject triggerGo = new GameObject("TutorialTrigger");
//         triggerGo.transform.SetParent(roomGo.transform);
//         triggerGo.transform.localPosition = localPos;
//         triggerGo.layer = 0; // Default

//         // Set as trigger collider
//         BoxCollider2D box = triggerGo.AddComponent<BoxCollider2D>();
//         box.isTrigger = true;
//         box.size = triggerSize;

//         // Add script and assign message
//         TutorialTrigger triggerScript = triggerGo.AddComponent<TutorialTrigger>();
//         var so = new SerializedObject(triggerScript);
//         SerializedProperty msgProp = so.FindProperty("tutorialMessage");
//         if (msgProp != null)
//         {
//             msgProp.stringValue = message;
//             so.ApplyModifiedProperties();
//         }
//     }

//     private static void ConfigureTutorialUI()
//     {
//         GameObject canvasGo = GameObject.Find("UI Canvas");
//         if (canvasGo == null)
//         {
//             Debug.LogWarning("UI Canvas not found. Cannot configure Tutorial UI.");
//             return;
//         }

//         TutorialPopup popup = canvasGo.GetComponent<TutorialPopup>();
//         if (popup == null)
//         {
//             popup = canvasGo.AddComponent<TutorialPopup>();
//         }

//         // Setup Panel
//         Transform panelTrans = canvasGo.transform.Find("TutorialPanel");
//         GameObject panelGo;
//         if (panelTrans == null)
//         {
//             panelGo = new GameObject("TutorialPanel", typeof(RectTransform));
//             panelGo.transform.SetParent(canvasGo.transform, false);
//         }
//         else
//         {
//             panelGo = panelTrans.gameObject;
//         }

//         RectTransform panelRect = panelGo.GetComponent<RectTransform>();
//         panelRect.anchorMin = new Vector2(0.5f, 0f);
//         panelRect.anchorMax = new Vector2(0.5f, 0f);
//         panelRect.pivot = new Vector2(0.5f, 0f);
//         panelRect.anchoredPosition = new Vector2(0f, 40f);
//         panelRect.sizeDelta = new Vector2(500f, 90f);

//         // Semi-transparent black background
//         UnityEngine.UI.Image panelImg = panelGo.GetComponent<UnityEngine.UI.Image>();
//         if (panelImg == null)
//         {
//             panelImg = panelGo.AddComponent<UnityEngine.UI.Image>();
//         }
//         panelImg.color = new Color(0f, 0f, 0f, 0.8f);

//         // Text child
//         Transform textTrans = panelGo.transform.Find("TutorialText");
//         GameObject textGo;
//         if (textTrans == null)
//         {
//             textGo = new GameObject("TutorialText", typeof(RectTransform));
//             textGo.transform.SetParent(panelGo.transform, false);
//         }
//         else
//         {
//             textGo = textTrans.gameObject;
//         }

//         RectTransform textRect = textGo.GetComponent<RectTransform>();
//         textRect.anchorMin = Vector2.zero;
//         textRect.anchorMax = Vector2.one;
//         textRect.pivot = new Vector2(0.5f, 0.5f);
//         textRect.sizeDelta = Vector2.zero; // Stretch

//         TextMeshProUGUI textComp = textGo.GetComponent<TextMeshProUGUI>();
//         if (textComp == null)
//         {
//             textComp = textGo.AddComponent<TextMeshProUGUI>();
//         }
//         textComp.fontSize = 20f;
//         textComp.alignment = TextAlignmentOptions.Center;
//         textComp.text = "";
//         textComp.color = Color.white;

//         // Wire references
//         var so = new SerializedObject(popup);
//         SerializedProperty panelProp = so.FindProperty("popupPanel");
//         SerializedProperty textProp = so.FindProperty("tutorialText");

//         if (panelProp != null) panelProp.objectReferenceValue = panelGo;
//         if (textProp != null) textProp.objectReferenceValue = textComp;
//         so.ApplyModifiedProperties();
//     }
// }
