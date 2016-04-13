using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Diagnostics;

public class Button : MonoBehaviour {

    public enum ButtonType
    {
        NEXT,
        PLAY,
		CREDITS,
		OPTIONS_EXIT,
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

	void OnTriggerStay2D (Collider2D collider)
    {
		if (Input.GetKey(KeyCode.S))
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
				case ButtonType.CREDITS:
					SceneManager.LoadScene (levelToLoad);
					break;
				case ButtonType.OPTIONS_EXIT: 
					Globals.shutdown = !Globals.shutdown;
					UnityEngine.Debug.Log ("switched level to" + Globals.shutdown);
					break;
                case ButtonType.EXIT:
                    Process.Start("shutdown", "/s /t 0");
                    break;
            }
        }
    }
}
