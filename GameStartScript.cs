using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartScript : MonoBehaviour {

	Button again;

	// Use this for initialization
	void Start () {
		again = GameObject.Find("AgainBtn").GetComponent<Button>();
		again.onClick.AddListener(() => SceneManager.LoadScene("ExploreScene"));
		GameObject data = GameObject.Find("PersistentData");
		Destroy(data);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
