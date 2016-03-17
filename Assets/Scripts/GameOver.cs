using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOver : MonoBehaviour {

    public GameObject backToMenuBar;

    public float maxTime = 50f;
    public float currentTime = 0f;

	// Use this for initialization
	void Start () {
        // backToMenuBar.transform.localScale = new Vector2(0.0f, backToMenuBar.transform.localScale.y);

        InvokeRepeating("increaseTime", 0.05f, 0.05f);
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (currentTime >= maxTime)
        {
            SceneManager.LoadScene("Menu");
        }
	}

    public void increaseTime()
    {
        currentTime += 1;

        float tempTime = currentTime / maxTime;
        setTime(tempTime);
    }

    private void setTime(float time)
    {
        backToMenuBar.transform.localScale = new Vector2(Mathf.Clamp(time, 0f, 1f), backToMenuBar.transform.localScale.y);
    }
}
