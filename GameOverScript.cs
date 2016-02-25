using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour {

    Button again;

	// Use this for initialization
	void Start () {
        again = GameObject.Find("AgainBtn").GetComponent<Button>();
        again.onClick.AddListener(() => SceneManager.LoadScene("ExploreScene"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
