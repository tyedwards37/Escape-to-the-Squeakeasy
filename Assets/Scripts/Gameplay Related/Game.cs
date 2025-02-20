using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game Instance;

    [SerializeField] private UI ui;
    [SerializeField] private Transform levelPivotPoint;
    [SerializeField] private List<LevelSO> levels;
    [SerializeField] GameObject hamsterPrefab;
    [SerializeField] private List<string> inventoryItems;
    [SerializeField] private List<string> unlockedDoors;
    public bool isGamePaused { get; private set; }
    private Hamster hamster;
    private LevelSO currentLevel;
    private GameObject currentLevelObject;
    public bool hasMadeFirstMove;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        LoadLevel(levels[0], levels[0].startingPosition[0]);
        AudioInterface.Instance.PlayMainTheme();
        AudioInterface.Instance.PlayWinSound();
    }

    public void LoadNextLevel(int index)
    {
        if(currentLevel.nextLevels[index] != null) {
            LevelSO level = currentLevel.nextLevels[index];
            Vector3 spawnPos = level.startingPosition[0]; // !!! This may break stuff but YOLO - Dylan

            LoadLevel(level, spawnPos);

            ui.TriggerHintExit();
        } else {
            Debug.Log("There are no next levels!");
        }
    }

    public void LoadPreviousLevel(int index)
    {
        if(currentLevel.previousLevels[0] != null) {
            LevelSO level = currentLevel.previousLevels[0];
            Vector3 spawnPos = level.returningToLevelStartingPosition[index];

            LoadLevel(level, spawnPos);
        } else {
            Debug.Log("There are no previous levels!");
        }
    }

    public void LoadWinningScene()
    {
        SceneManager.LoadScene("Winning Screen");
    }

    private void LoadLevel(LevelSO level, Vector3 spawnPosition)
    {
        ClearCurrentLevel();

        RemoveHamster();

        currentLevel = level;
        currentLevelObject = Instantiate(level.levelPrefab, levelPivotPoint.position, Quaternion.identity);

        PlaceHamster(spawnPosition);

        level.OnLevelStart();
        hasMadeFirstMove = false;
        AudioInterface.Instance.PlayOpenDoor();
    }

    private void ClearCurrentLevel()
    {
        if(currentLevelObject != null) {
            currentLevel.OnLevelEnd();
            Destroy(currentLevelObject);
        }
    }

    public void PlaceHamster(Vector3 location)
    {
        GameObject hamsterObject = Instantiate(hamsterPrefab, location, Quaternion.identity);
        hamster = hamsterObject.GetComponent<Hamster>();
    }
    
    public void RemoveHamster()
    {
        if(hamster != null)
        {
            Destroy(hamster.gameObject);
        }
    }

    public void AddItemToInventory(string item)
    {
        if(!inventoryItems.Contains(item))
        {
            AudioInterface.Instance.PlayCollectKey();
            inventoryItems.Add(item);
            ui.ShowInventoryItem(item);
        }
    }

    public void AddDoorToUnlocked(string door)
    {
        if(!unlockedDoors.Contains(door))
        {
            AudioInterface.Instance.PlayOpenDoor();
            unlockedDoors.Add(door);
        }
    }

    public bool HasItemInInventory(string item)
    {
        for(int i = 0; i < inventoryItems.Count; ++i)
        {
            if(inventoryItems[i] == item) {
                return true;
            }
        }

        return false;
    }

    public bool HasUnlockedDoor(string door) {
        for(int i = 0; i < unlockedDoors.Count; ++i) {
            if(unlockedDoors[i] == door) {
                return true;
            }
        }

        return false;
    }

    public Hamster GetHamster()
    {
        return hamster;
    }

    public Vector3 GetHamsterPosition()
    {
        return hamster.transform.position;
    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;
        Time.timeScale = isGamePaused ? 0 : 1;
        ui.TogglePausedUI();
    }
}
