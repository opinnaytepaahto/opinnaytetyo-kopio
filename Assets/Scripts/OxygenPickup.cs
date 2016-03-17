using UnityEngine;
using System.Collections;

public class OxygenPickup : MonoBehaviour {

    private Rigidbody2D physics;

    void Start()
    {
        // 88 ostin nahkasandaalit
        physics = GetComponent<Rigidbody2D>();
        physics.AddForce(new Vector2(0, 100f), ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().currentOxygen += 100;

            Destroy(gameObject);
        }
    }
}
