using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D physics;

    public float maxOxygen = 500f;
    public float currentOxygen = 0f;

    public float maxPlayerHealth = 100f;
    public float currentPlayerHealth = 0f;

    public Transform groundDetector;
    public bool grounded = true;

    public float maxSpeed = 10f;

    public float speed = 300f;
    public float jumpSpeed = 30f;

    public bool isFacingRight = false;

    public float shootTimer = 0.3f;

    private float timer = 3f;

    public GameObject hearth1;
    public GameObject hearth2;
    public GameObject hearth3;
    public GameObject hearth4;
    public GameObject hearth5;

    public GameObject scoreDisplay;

    public GameObject bullet;
    public GameObject oxygenBar;

    public int score = 0;

	// Use this for initialization
	void Start ()
    {
        currentPlayerHealth = maxPlayerHealth;

        physics = GetComponent<Rigidbody2D>();

        InvokeRepeating("decreaseOxygen", 0.5f, 0.5f);

        currentOxygen = maxOxygen;
    }
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            physics.AddForce(new Vector2(speed * Time.deltaTime, 0f));
        }
        else if (Input.GetKey(KeyCode.A))
        {
            physics.AddForce(new Vector2(-speed * Time.deltaTime, 0f));
        }
        else
            if (grounded)
                physics.velocity = new Vector2(Vector2.zero.x, physics.velocity.y);

        physics.velocity = new Vector2(Mathf.Clamp(physics.velocity.x, -maxSpeed, maxSpeed), physics.velocity.y);

        if (Input.GetKey(KeyCode.A) && !isFacingRight)
            Flip();
        else if (Input.GetKey(KeyCode.D) && isFacingRight)
            Flip();

        grounded = Physics2D.OverlapCircle(groundDetector.position, 0.15f);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            transform.GetChild(2).GetComponent<AudioSource>().Play();
            physics.AddForce(new Vector2(0, jumpSpeed));
        }

        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0)
        {
            shootTimer = 0;
        }

        if (Input.GetKey(KeyCode.LeftControl) && shootTimer == 0)
        {
            transform.GetChild(1).GetComponent<AudioSource>().Play();

            bullet.GetComponent<BulletController>().facingRight = isFacingRight;

            if (isFacingRight)
            {
                Instantiate(bullet, transform.position + new Vector3(-1, 0, 0), Quaternion.identity);
                physics.AddForce(new Vector2(2, 0), ForceMode2D.Impulse);
            }
            else
            {
                Instantiate(bullet, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
                physics.AddForce(new Vector2(-2, 0), ForceMode2D.Impulse);
            }

            shootTimer = 0.3f;
        }

        if (currentPlayerHealth <= 0f)
        {
            transform.GetChild(3).GetComponent<AudioSource>().Play();

            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                SceneManager.LoadScene("GameOver");
                //Destroy(gameObject);S
                //Destroy(this);
            }
        }

        if (currentOxygen == 0)
        {
            currentPlayerHealth = 0;
        }

        if (currentPlayerHealth >= 81f && currentPlayerHealth <= 100f)
        {
            hearth1.SetActive(true);
            hearth2.SetActive(true);
            hearth3.SetActive(true);
            hearth4.SetActive(true);
            hearth5.SetActive(true);
        }

        if (currentPlayerHealth >= 61f && currentPlayerHealth <= 80f)
        {
            hearth1.SetActive(false);
            hearth2.SetActive(true);
            hearth3.SetActive(true);
            hearth4.SetActive(true);
            hearth5.SetActive(true);
        }

        if (currentPlayerHealth >= 41f && currentPlayerHealth <= 60f)
        {
            hearth1.SetActive(false);
            hearth2.SetActive(false);
            hearth3.SetActive(true);
            hearth4.SetActive(true);
            hearth5.SetActive(true);
        }
        if (currentPlayerHealth >= 21f && currentPlayerHealth <= 40f)
        {
            hearth1.SetActive(false);
            hearth2.SetActive(false);
            hearth3.SetActive(false);
            hearth4.SetActive(true);
            hearth5.SetActive(true);
        }

        if (currentPlayerHealth >= 1f && currentPlayerHealth <= 20f)
        {
            hearth1.SetActive(false);
            hearth2.SetActive(false);
            hearth3.SetActive(false);
            hearth4.SetActive(false);
            hearth5.SetActive(true);
        }

        if (currentPlayerHealth <= 0f)
        {
            hearth1.SetActive(false);
            hearth2.SetActive(false);
            hearth3.SetActive(false);
            hearth4.SetActive(false);
            hearth5.SetActive(false);
        }

        updateScore();
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void decreaseOxygen()
    {
        currentOxygen -= 5f;

        float tempHealth = currentOxygen / maxOxygen;
        setOxygenBar(tempHealth);
    }

    public void decreaseHealth(float health)
    {
        currentPlayerHealth -= health;
    }

    public void setOxygenBar(float oxygen)
    {
        oxygenBar.transform.localScale = new Vector2(Mathf.Clamp(oxygen, 0f, 1f), oxygenBar.transform.localScale.y);
    }

    private void updateScore()
    {
        scoreDisplay.GetComponent<Text>().text = "Score: " + GameObject.Find("Spaceman").GetComponent<PlayerController>().score;
    }
}
