using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuHudController : MonoBehaviour {

	public Text shutdownText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Globals.shutdown)
			shutdownText.text = "SHUTDOWN: ON";
		else
			shutdownText.text = "SHUTDOWN: OFF";
	}
}
