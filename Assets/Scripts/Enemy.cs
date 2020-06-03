using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Enemey Stats")]
    public float health = 500;
    public int scoreValue = 150;
    public GameObject shielddrop;
    public int ShieldDropRate = 10;
    public int gundroprate = 15;
    public GameObject gunDrop;

    [Header("Shooting")]
    public float shotCounter;
    public float minTimeBetweenShots = 0.2f;
    public float maxTimebetweenShots = 3f;
    public GameObject projectile;
    public float projectileSpeed = -2f;

    [Header("SFX")]
    public GameObject deathExplosion;
    public float DeathTimeDelay;
    public AudioClip deathSound;
    public float deathvolume = 0.4f;
    public float shootsoundvolume = 0.15f;
    public AudioClip shootSound;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimebetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimebetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(
            projectile,
            transform.position,


            Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootsoundvolume);
    }

    private void OnTriggerEnter2D(Collider2D otherthingthatjustbumpedintous)
    {
        DamageDealer damageDealer = otherthingthatjustbumpedintous.gameObject.GetComponent<DamageDealer>();
        death(damageDealer);
    }

    private void death(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health < 1f)
        {
            int gunupchance = 2;
            int powerupchance = 1;
            if (Random.Range(0, ShieldDropRate) == powerupchance)
            {
                Instantiate(shielddrop, transform.position, Quaternion.identity);
            }else
                if (Random.Range(0, gundroprate) == gunupchance)
            {
                Instantiate(gunDrop, transform.position, Quaternion.identity);
            }

            Die();
        }
        

        
    }

    private void Die()
    {

        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        GameObject explosion = Instantiate(
            deathExplosion,
            transform.position,
            Quaternion.identity);
        Destroy(explosion, DeathTimeDelay);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathvolume);
    }
}
