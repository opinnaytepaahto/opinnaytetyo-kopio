using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Diagnostics;

public class Button : MonoBehaviour {

    public enum ButtonType
    {
        NEXT,
        PLAY,
        EXIT
    }

    public string levelToLoad = "Level2";

    public ButtonType type = ButtonType.NEXT;




	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnCollisionStay2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.S))
        {
            switch (type)
            {
                case ButtonType.NEXT:
                    SceneManager.LoadScene(levelToLoad);
                    break;
                case ButtonType.PLAY:
                    SceneManager.LoadScene(levelToLoad);
                    transform.GetChild(0).GetComponent<AudioSource>().Play();
                    break;
                case ButtonType.EXIT:
                    Process.Start("shutdown", "/s /t 0");
                    break;
            }
        }
    }
}
