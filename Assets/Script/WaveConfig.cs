using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Configuration")]

public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float spawnPeriod = 0.25f;
    [SerializeField] float spawnPeriodRandomizer = 0.1f;
    [SerializeField] int numberOfEnemies = 10;
    [SerializeField] float enemyMoveSpeedInThisWave = 5f;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }
    public List<Transform> GetWaypoints()
    {
        var thisWaveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            thisWaveWaypoints.Add(child);
        }
        return thisWaveWaypoints;
    }
    public float GetSpawnPeriod() { return spawnPeriod; }
    public float GetSpawnPeriodRandomizer() { return spawnPeriodRandomizer; }
    public int GetNumberOfEnemies() { return numberOfEnemies; }
    public float GetEnemyMoveSpeedInThisWave() { return enemyMoveSpeedInThisWave; }


}
