using UnityEngine;
using System.Collections;

public class EnemyTemp : MonoBehaviour {

	public PlayerReady ready;
	public PlayerHealth health;
	public PlayerReady enemyread;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (ready.cur_ready == 100) {
			ready.cur_ready = 0;
			health.Damage (10, 15);
			enemyread.Regen (-50);
		}
	}
}
