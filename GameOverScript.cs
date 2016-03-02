using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour {

	Button again;

	// Use this for initialization
	void Start () {
		again = GameObject.Find("AgainBtn").GetComponent<Button>();
		again.onClick.AddListener(() => SceneManager.LoadScene("Play"));
		//this doesn't reset DataScript b/c it can't be destroyed.
	}
	
	// Update is called once pe
	void Update () {
	
	}
}
