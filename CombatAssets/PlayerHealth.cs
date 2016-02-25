using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public float max_health = 100f;
	public float cur_health = 0f;

	public GameObject healthBar;
	// Use this for initialization
	void Start () {
		cur_health = max_health;
	}
	
	// Update is called once per frame
	void Update () {
		//SetHealthBar(cur_health);
	}


	public void Damage(float min_change, float max_change){
		float change = Mathf.Floor(Random.Range (min_change, max_change + 1));
		if (cur_health - change < 0)
			cur_health = 0;
		else
		cur_health -= change;
		SetHealthBar (cur_health);
	}

	public void SetHealthBar(float health){
		float calc_health = health / max_health;
		healthBar.transform.localScale = new Vector3 (calc_health, 1, transform.localScale.z);
	}
}
