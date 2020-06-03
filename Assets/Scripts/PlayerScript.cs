using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    Powerup powerup;

    [Header("shield")]
    public bool giveShield;
    public GameObject shield;
    public float shieldDuration = 5f;
    public GameObject Shieldtopickup;
    int shieldpowerup = 1;

    [Header("Big Gun powerup")]
    public bool giveGun;
    public GameObject gun;
    public float gunDuraction = 5f;
    public GameObject GuntoPickup;
    int gunpowerup = 2;
    public GameObject Bluelaser;
    public float blueprojectileSpeedFiringPeriod = 0.3f;
    public float blueprojectileSpeed = 2f;
    public float laserGunDuration = 3f;


    [Header("Player Movement")]
    [SerializeField] float _xmoveSpeed = 10f;
    [SerializeField] float _ymoveSpeed = 5f;
    [SerializeField] float _padding = 0.5f;
    public float health = 500f;
    public float deathvolume = 0.4f;
    public AudioClip deathSound;

    public float shootsoundvolume = 0.15f;
    public AudioClip shootSound;

    Coroutine firingCoroutine;


    
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    [Header("Projectile")]
    public float projectileSpeedFiringPeriod = 0.1f;
    public GameObject Greenlaser;
    public float projectileSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _SetUpMoveBoundaries();
        
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        fire();
        Hassheild();

    }

    public void  Hassheild()
    {
        if (giveShield)
        {
            GameObject shieldymcshield = Instantiate(shield, transform.position, Quaternion.identity) as GameObject;
            giveShield = false;
            shieldymcshield.transform.parent = gameObject.transform;
            Destroy(shieldymcshield, shieldDuration * Time.deltaTime);
        }
    }


    IEnumerator fireContinuously()
    {
        while (true)
        {
            if (giveGun)
            {
                GameObject laser = Instantiate(Bluelaser, transform.position, Quaternion.identity) as GameObject;
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, blueprojectileSpeed);
                yield return new WaitForSeconds(blueprojectileSpeedFiringPeriod);
            }
            else
            {
                GameObject laser = Instantiate(Greenlaser, transform.position, Quaternion.identity) as GameObject;
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                yield return new WaitForSeconds(projectileSpeedFiringPeriod);
            }

            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootsoundvolume);
        }
    }

    private void fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(fireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    private void Move()
    {
        //float deltaX = Input.GetAxis("Horizontal");
        // float newXPos = transform.position.x + deltaX;
        // transform.position = new Vector2(newXPos, transform.position.y);

        
        transform.position = new Vector2(Mathf.Clamp(transform.position.x + Input.GetAxis("Horizontal") * Time.deltaTime * _xmoveSpeed, xMin, xMax), transform.position.y);
        transform.position = new Vector2(transform.position.x , Mathf.Clamp(transform.position.y + Input.GetAxis("Vertical") * Time.deltaTime * _ymoveSpeed, yMin, yMax));

    }

    private void _SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + _padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - _padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + _padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - _padding;
    }

    private void OnTriggerEnter2D(Collider2D otherthingthatjustbumpedintous)
    {
        if (otherthingthatjustbumpedintous.gameObject.tag == "shield")
        {
            giveShield = true;
            Destroy(otherthingthatjustbumpedintous.gameObject);
        }else
        if (otherthingthatjustbumpedintous.gameObject.tag == "Gun")
        {
            giveGun = true;
            StartCoroutine(BigGunEnumerator());
        }
        DamageDealer damageDealer = otherthingthatjustbumpedintous.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        death(damageDealer);     
    }

    IEnumerator BigGunEnumerator()
    {
        yield return new WaitForSeconds(laserGunDuration * Time.deltaTime);
        giveGun = false;
    }

    private void death(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        FindObjectOfType<GameSession>().removefromHealth(damageDealer.GetDamage());
        damageDealer.Hit();
        if (health < 1f)
        {
            die();
        }
    }

    private void die()
    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathvolume);
    }
}
