using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

    private Rigidbody2D physics;
    private GameObject player;

    public bool facingRight;

    public bool doesDamage = true;

    private float timer = 2f;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Spaceman");
        if (player)
        {
            PlayerController controller = player.GetComponent<PlayerController>();
        }

        physics = GetComponent<Rigidbody2D>();

        if (facingRight)
        {
            physics.AddForce(new Vector2(-20, 0), ForceMode2D.Impulse);
        }
        else
        {
            physics.AddForce(new Vector2(20, 0), ForceMode2D.Impulse);
        }
    }
	
	// Update is called once per frame
	void Update () {

	}

    void OnCollisionEnter2D(Collision2D collision)
    {
		Destroy (gameObject);

        if (doesDamage)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyHealth>().decreaseHealth(25f);
            }

            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerController>().decreaseHealth(10f);
            }

            doesDamage = false;
        }
    }
}
