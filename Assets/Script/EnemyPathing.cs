using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> waypoints;
    float moveSpeed;
    int waypointIndex = 0;

	void Start ()
    {
        moveSpeed = waveConfig.GetEnemyMoveSpeedInThisWave();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
	}
    void Update ()
    {
        MoveEnemy();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void MoveEnemy()
    {
        if (waypointIndex < waypoints.Count)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementInThisFrame = Time.deltaTime * moveSpeed;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementInThisFrame);
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
