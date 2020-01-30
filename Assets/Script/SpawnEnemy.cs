using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] int waveIndex;
    [SerializeField] bool looping = false;
    IEnumerator Start()
    {
        do
        {
            waveIndex = startingWave;
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping == true);

    }
    private IEnumerator SpawnAllWaves()
    {
        for (; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currnetWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInThisWave(currnetWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInThisWave(WaveConfig waveConfig)
    {
        for (int i = 0; i <waveConfig.GetNumberOfEnemies(); i++)
        {
            var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetSpawnPeriod() + waveConfig.GetSpawnPeriodRandomizer());
        }
    }

	
}
