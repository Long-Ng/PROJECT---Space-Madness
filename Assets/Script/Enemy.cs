using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] float enemyShotTimer;
    [SerializeField] float minEnemyShotTimer= 0.2f;
    [SerializeField] float maxEnemyShotTimer= 2f;
    [SerializeField] GameObject enemyBullet;
    [SerializeField] GameObject explodingVFX;
    [Header("Audio Volume Control")]
    [Range(-30, 0)] [SerializeField] float bulletSpeed = - 45;
    [SerializeField] AudioClip enemyExplosionSFX;
    [Range(0,1)][SerializeField] float volume1 = 0.5f;
    [SerializeField] AudioClip enemyLaserSFX;
    [Range(0, 1)] [SerializeField] float volume2 = 0.5f;

    void Start()
    {
       enemyShotTimer = UnityEngine.Random.Range(minEnemyShotTimer, maxEnemyShotTimer);
    }
    void Update()
    {
        EnemyShotCountdown();
        if (health <= 0)
            Die();
    }

    private void EnemyShotCountdown()
    {
        enemyShotTimer = enemyShotTimer - Time.deltaTime;
        if(enemyShotTimer <= 0)
        {
            Fire();
            enemyShotTimer = UnityEngine.Random.Range(minEnemyShotTimer, maxEnemyShotTimer);
        }
    }

    private void Fire()
    {
        var enemyLaser = Instantiate(enemyBullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        AudioSource.PlayClipAtPoint(enemyLaserSFX, Camera.main.transform.position, volume2);
        enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
    }

    private void Die()
    {
        Destroy(gameObject);
        GameObject deathExplosion = Instantiate(explodingVFX, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(enemyExplosionSFX, Camera.main.transform.position, volume1);
        Destroy(deathExplosion, 2);
    }
}
