using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
public class LevelSO : ScriptableObject
{
    [Header("Pointers")]
    public List<LevelSO> previousLevels;
    public List<LevelSO> nextLevels;

    [Header("Map Related")]
    public GameObject levelPrefab;
    public List<Vector3> startingPosition;
    public List<Vector3> returningToLevelStartingPosition;

    public virtual void OnLevelStart()
    {
        
    }

    public virtual void OnLevelEnd()
    {
        Game.Instance.RemoveHamster();
    }
}
