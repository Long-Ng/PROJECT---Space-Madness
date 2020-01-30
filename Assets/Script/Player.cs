using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //CONFIG
    [Header("Player Stats")] 
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] float health = 200f;
    [SerializeField] float collisionDamage = 50;
    [Header("Player Bullets")]
    [SerializeField] float projectileFirePeriod = 0.25f;
    [Range(0, 30)] [SerializeField] float playerBulletSpeed = 15;
    [SerializeField] GameObject playerLaser;
    [Header("Audio Volume Control")]
    [SerializeField] AudioClip playerExplosionSFX;
    [Range(0,1)][SerializeField] float volume1 = 0.5f;
    [SerializeField] AudioClip playerLaserSFX;
    [Range(0, 1)] [SerializeField] float volume2 = 0.5f;
    [SerializeField] AudioClip playerOnHitSFX;
    [Range(0, 1)] [SerializeField] float volume3 = 0.5f;
    [SerializeField] AudioClip playerLaserReleaseSFX;
    [Range(0, 1)] [SerializeField] float volume4 = 0.5f;
    //VARIABLE
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    Coroutine firingCoroutine;
    //REFERENCES
    CurrentLevelStat currentlevel;


    void Start()
    {
        currentlevel = FindObjectOfType<CurrentLevelStat>();
        SetUpMoveLimit();
    }


    void Update ()
    {
        Move();
        Fire();

        if (health <= 0)
            Die();
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(playerExplosionSFX, Camera.main.transform.position, volume1);
        Destroy(gameObject);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            AudioSource.PlayClipAtPoint(playerLaserReleaseSFX, Camera.main.transform.position, volume4);
            StopCoroutine(firingCoroutine);       
        }
        
    }
    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos =Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);

        var deltaY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY,yMin,yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }
    private void SetUpMoveLimit()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

    }
    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(playerLaser, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, playerBulletSpeed);
            if (playerLaserSFX)
            {
                AudioSource.PlayClipAtPoint(playerLaserSFX, Camera.main.transform.position, volume2);
            }
                yield return new WaitForSeconds(projectileFirePeriod);

        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        DamageDealer damageDealer = collider.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        AudioSource.PlayClipAtPoint(playerOnHitSFX, Camera.main.transform.position, volume3);
        health = health - damageDealer.GetDamage();
        damageDealer.Hit();
    }


        public float GetPlayerHealth()
        {
            return health;
        }
}




