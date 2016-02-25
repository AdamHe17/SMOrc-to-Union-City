using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	//public int AttackNo;
	public float minDamage;
	public float maxDamage;
	public float staminaCost;
	public float hitStun;
	public int belongsTo;

	void Start(){
		belongsTo = transform.parent.parent.parent.GetComponent<Attacker> ().partynumber;
	}
}
