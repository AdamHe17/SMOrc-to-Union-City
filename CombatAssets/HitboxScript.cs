using UnityEngine;
using System.Collections;

public class HitboxScript : MonoBehaviour {

	GameObject man;
	GameObject targeter;

	void Start(){
		man = GameObject.Find ("Manager");
	}

	public void Function(){
		if (man.GetComponent<Manager> ().clickable) {
			man.GetComponent<Manager> ().targeting = transform.parent.parent.GetComponent<Attacker> ().partynumber;
			switch (man.GetComponent<Manager> ().targeter) {
			case 1:
				targeter = GameObject.Find ("dude1");
				break;
			case 2:
				targeter = GameObject.Find ("dude2");
				break;
			case 3:
				targeter = GameObject.Find ("dude3");
				break;
			}
			targeter.GetComponent<Attacker>().Execute();
		}
	}
}
