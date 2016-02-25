using UnityEngine;
using System.Collections;

public class PlayerReady : MonoBehaviour {


	public float max_ready = 100f;
	public float cur_ready = 0f;
	public float ready_regen = .5f;
	public GameObject ReadyBar;

	// Use this for initialization
	void Start () {
		cur_ready = max_ready / 4;
	}
	
	// Update is called once per frame
	void Update () {
		Regen (ready_regen);
		SetReadyBar (cur_ready);
	}
	public void Regen(float amount)
	{
		if (cur_ready + amount > max_ready)
			cur_ready = max_ready;
		else
			if (cur_ready + amount < 0)
			cur_ready = 0;
		else
		cur_ready += amount;
		SetReadyBar (cur_ready);
	}

	public void SetReadyBar(float ready){
		float calc_ready = ready / max_ready;
		ReadyBar.transform.localScale = new Vector3 (calc_ready, transform.localScale.y, transform.localScale.z);
	}
}
