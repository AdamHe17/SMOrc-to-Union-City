using UnityEngine;
using System.Collections;

public class PlayerStamina : MonoBehaviour {
	
	
	public float max_stamina = 100f;
	public float cur_stamina = 0f;
	public float stamina_regen = .05f;
	public GameObject staminaBar;
	
	// Use this for initialization
	void Start () {
		cur_stamina = max_stamina / 4;
	}
	
	// Update is called once per frame
	void Update () {
		Regen (stamina_regen);
		SetStaminaBar (cur_stamina);
	}
	public void Regen(float amount)
	{
		if (cur_stamina + amount > max_stamina)
			cur_stamina = max_stamina;
		else
		cur_stamina += amount;
		SetStaminaBar (cur_stamina);
	}
	
	public void SetStaminaBar(float stamina){
		float calc_stamina = stamina / max_stamina;
		staminaBar.transform.localScale = new Vector3 (calc_stamina, transform.localScale.y, transform.localScale.z);
	}
}
