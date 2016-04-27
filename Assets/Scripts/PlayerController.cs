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

    private float timer = 1f;

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
		if (Input.GetKey (KeyCode.Q)) // Check if Q key is pressed 
		{
			SceneManager.LoadScene("Menu"); // Back to MainMenu
		}
        if (Input.GetKey(KeyCode.D)) // Check if D key is pressed
        {
            physics.AddForce(new Vector2(speed * Time.deltaTime, 0f)); // Move player right
        }
        else if (Input.GetKey(KeyCode.A)) // Check if A key is pressed
        {
            physics.AddForce(new Vector2(-speed * Time.deltaTime, 0f)); // Move player left
        }
        else // Check if A and D keys are not being pressed
            if (grounded) // Check if player is on the ground
            physics.velocity = new Vector2(Vector2.zero.x, physics.velocity.y); // Stop the player

        physics.velocity = new Vector2(Mathf.Clamp(physics.velocity.x, -maxSpeed, maxSpeed), physics.velocity.y); // Limit the players speed

        if (Input.GetKey(KeyCode.A) && !isFacingRight) // Check if A key is pressed and player is not facing right
            Flip(); // Flip the player
        else if (Input.GetKey(KeyCode.D) && isFacingRight) // Check if D key is pressed and player is facing right
            Flip(); // Flip the player

        grounded = Physics2D.OverlapCircle(groundDetector.position, 0.15f); // Check if the player is on the ground and store it to grounded variable

		if (Input.GetKeyDown(KeyCode.W) && grounded) // Check if spacebar has been pressed and the player is on the ground
        {
            transform.GetChild(2).GetComponent<AudioSource>().Play(); // Play the jump sound
            physics.AddForce(new Vector2(0, jumpSpeed)); // Propel the player up
        }

        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0)
        {
            shootTimer = 0;
        }

		if (Input.GetKey(KeyCode.Space) && shootTimer == 0)
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

        if (currentPlayerHealth <= 0f) // Check if players health is less than or equal to zero
        {
            transform.GetChild(3).GetComponent<AudioSource>().Play(); // Play the death sound

            timer -= Time.deltaTime; // Remove delta time from game over screens loading timer

            if (timer <= 0) // Check if game over screens loading timer is less than or equal to zero
            {
                SceneManager.LoadScene("GameOver"); // Load the game over screen
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
