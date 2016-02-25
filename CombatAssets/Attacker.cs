using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Attacker : MonoBehaviour {

	GameObject manager;
	Manager man;
	GameObject spot;//spotlight

	GameObject self;
	Text damagetxt;

	int count;

	GameObject h1;
	GameObject h2;//attack
	GameObject h3;//highlights
	GameObject h4;

	GameObject damageText;
	GameObject damageSplash;

	public float opacity;
	public bool dead;
	bool displayingDamage;
	

	public int partynumber;
	float opacity_step;
	public GameObject healthBar;
	public float max_health;
	public float cur_health;
	public float tic_health;

	public GameObject staminaBar;
	public float max_stamina;
	public float cur_stamina;
	public float tic_stamina;

	public GameObject readyBar;
	public float max_ready;
	public float cur_ready;
	public float tic_ready;

	GameObject targeting;
	public GameObject attack1;
	public GameObject attack2;
	public GameObject attack3;
	public GameObject attack4;

	void Update(){
		if (!dead) {
			if (displayingDamage){
				Text dmgtxt = targeting.transform.FindChild("Canvas1").FindChild("Text").GetComponent<Text>();
				Renderer splotchcolor = targeting.transform.FindChild("Canvas1").FindChild ("blood_splotch").GetComponent<Renderer>();
				dmgtxt.color = new Vector4(dmgtxt.color.r,dmgtxt.color.g,dmgtxt.color.b,dmgtxt.color.a*.98f);
				splotchcolor.material.color = new Vector4(splotchcolor.material.color.r,splotchcolor.material.color.g,splotchcolor.material.color.b,splotchcolor.material.color.a*0.98f);
				count++;
				Debug.Log (count);
				if(count == 90){
					displayingDamage = false;
					count = 0;
					self.transform.localScale = new Vector3 (self.transform.localScale.x * .4f, self.transform.localScale.y * .4f, self.transform.localScale.z);
					targeting.transform.localScale = new Vector3 (targeting.transform.localScale.x * .5f, targeting.transform.localScale.y * 0.5f, targeting.transform.localScale.z);
					dmgtxt.color = new Vector4(dmgtxt.color.r,dmgtxt.color.g,dmgtxt.color.b,0f);
					splotchcolor.material.color = new Vector4(splotchcolor.material.color.r,splotchcolor.material.color.g,splotchcolor.material.color.b,0f);
					man.pause = false;
					man.clickable = false;
					man.targeter = 0;
					man.targeting = 0;
				}
			}else if (!man.pause) {
					if (tic_health != 0)
						changeHP (tic_health);
					if (tic_stamina != 0)
						changeS (tic_stamina);
					if (tic_ready != 0)
						changeR (tic_ready);
					opacity = 0;
				} else if (man.targeter == partynumber) {//this means the game is waiting for an input
					if (opacity < 15) {
						opacity = 250;
						opacity_step -= 2;
					} else if (opacity < 64) {				
						opacity = 64;
						opacity_step = 2;
					} else if (opacity > 255) {
						opacity = 255;
						opacity_step = -2;
					}
					opacity += opacity_step;
					if (partynumber < 11)
						EditOpacity (spot, opacity);
				}
			}else {
			self.transform.localScale = new Vector3(self.transform.localScale.x*.98f, self.transform.localScale.y*.96f,1f);
			if(self.transform.localScale.y < .01)
			{
				Destroy (self);
			}
		}
	}
	
	void Start(){
		dead = false;
		opacity = 0;
		manager = GameObject.Find ("Manager");
		man = manager.GetComponent<Manager> ();
		changeHP (0);	
		changeS (0);
		changeR (0);

		switch (partynumber) {
		case 1:
			self = GameObject.Find ("dude1");
			break;
		case 2:
			self = GameObject.Find ("dude2");
			break;
		case 3:
			self = GameObject.Find ("dude3");
			break;
		case 11:
			self = GameObject.Find ("dude11");
			break;
		case 12:
			self = GameObject.Find ("dude12");
			break;
		case 13:
			self = GameObject.Find ("dude13");
			break;
		}

		//if (partynumber == 12) {
		//if (partynumber > 10) {
			damageText = self.transform.FindChild ("Canvas1").FindChild ("Text").gameObject;
			damageSplash = self.transform.FindChild ("Canvas1").FindChild ("blood_splotch").gameObject;
			damageText.GetComponent<Text> ().color = new Vector4 (damageText.GetComponent<Text> ().color.r, damageText.GetComponent<Text> ().color.g, damageText.GetComponent<Text> ().color.b, 0);
			EditOpacity (damageSplash, 0);
			//}
		//}
		if (partynumber < 11) {
			//man.Members[partynumber] = 1;
			spot = self.transform.FindChild ("spotlight").gameObject;
			EditOpacity (spot, 0);
			h1 = self.transform.FindChild ("Canvas").FindChild ("Attacks").FindChild ("Attack1").FindChild ("highlight1").gameObject;
			h2 = self.transform.FindChild ("Canvas").FindChild ("Attacks").FindChild ("Attack2").FindChild ("highlight2").gameObject;
			h3 = self.transform.FindChild ("Canvas").FindChild ("Attacks").FindChild ("Attack3").FindChild ("highlight3").gameObject;
			h4 = self.transform.FindChild ("Canvas").FindChild ("Attacks").FindChild ("Attack4").FindChild ("highlight4").gameObject;
			EditOpacity (h1, 0);
			EditOpacity (h2, 0);
			EditOpacity (h3, 0);
			EditOpacity (h4, 0);
		}
	}

	public void Execute(){
		if (cur_stamina > staminaCost && cur_ready == 100) {
			switch (man.targeting) {
			case 1:
				targeting = GameObject.Find ("dude1");
				break;
			case 2:
				targeting = GameObject.Find ("dude2");
				break;
			case 3:
				targeting = GameObject.Find ("dude3");
				break;
			case 11:
				targeting = GameObject.Find ("dude11");
				break;
			case 12:
				targeting = GameObject.Find ("dude12");
				break;
			case 13:
				targeting = GameObject.Find ("dude13");
				break;
			}

			float damage = Random.Range (-minDamage,-maxDamage);

			DisplayDamage(damage);

			//if(partynumber == 12){

			//damagetxt.text = Mathf.RoundToInt(damage).ToString();
			//EditOpacity(damageText,255);

			//damageText.GetComponent<Text>().color = new Vector4(damageText.GetComponent<Text>().color.r,damageText.GetComponent<Text>().color.g,damageText.GetComponent<Text>().color.b,1);
			//EditOpacity(damageSplash,255);
			//}

			targeting.GetComponent<Attacker> ().changeHP (damage);
			targeting.GetComponent<Attacker> ().changeR (-hitStun);
			changeR (-max_ready);
			changeS (-staminaCost);
			curAttack = 0;
//			man.pause = false;
//			man.clickable = false;
//			man.targeter = 0;
//			man.targeting = 0;
			opacity = 0;
			EditOpacity (spot, 0);
			EditOpacity (h1, 0);
			EditOpacity (h2, 0);
			EditOpacity (h3, 0);
			EditOpacity (h4, 0);
		}
	}

	public void changeR(float amount){
		cur_ready += amount;
		if (cur_ready > max_ready)
			cur_ready = max_ready;
		if (cur_ready == max_ready) {
			if(partynumber > 10){
				EnemyAttack();
			}else{
			man.pause = !man.pause;
				man.targeter = partynumber;
			}
		}
		if (cur_ready < 0)
			cur_ready = 0;
		SetBar (readyBar, cur_ready/max_ready);
	}
	
	public void changeHP(float amount){
		cur_health += amount;
		if (cur_health > max_health)
			cur_health = max_health;
		if (cur_health < 0) {
			Die ();
			cur_health = 0;
		}
		SetBar (healthBar, cur_health/max_health);
	}

	public void changeS(float amount){
		if (partynumber < 11) {
			cur_stamina += amount;
			if (cur_stamina > max_stamina)
				cur_stamina = max_stamina;
			if (cur_stamina < 0)
				cur_stamina = 0;
			SetBar (staminaBar, cur_stamina / max_stamina);
		}
	}

	public void SetBar(GameObject bar, float new_stat){
		bar.transform.localScale = new Vector3 (new_stat, bar.transform.localScale.y, bar.transform.localScale.z);
	}



	void EditOpacity(GameObject obj,float opac)
	{
		obj.GetComponent<Renderer>().material.color = new Vector4 (obj.GetComponent<Renderer> ().material.color.r,
		                                                         obj.GetComponent<Renderer> ().material.color.g,
		                                                         obj.GetComponent<Renderer> ().material.color.b,
		                                                         opac/255);
	}
	
	float minDamage;
	float maxDamage;
	float hitStun;
	float staminaCost;
	int curAttack;
	Attack AttackStats;
	
	public void AttackClick(int attackno){
		if (man.targeter == partynumber) {
			if (attackno == curAttack) {
				man.clickable = false;
				EditOpacity (h1, 0);
				EditOpacity (h2, 0);
				EditOpacity (h3, 0);
				EditOpacity (h4, 0);
				curAttack = 0;
				return;
			}
			switch (attackno) {
			case 1:
				EditOpacity (h1, 255);
				EditOpacity (h2, 0);
				EditOpacity (h3, 0);
				EditOpacity (h4, 0);
				AttackStats = transform.FindChild ("Canvas").FindChild ("Attacks").FindChild ("Attack1").GetComponent<Attack> ();
				break;
			case 2:
				EditOpacity (h1, 0);
				EditOpacity (h2, 255);
				EditOpacity (h3, 0);
				EditOpacity (h4, 0);
				AttackStats = transform.FindChild ("Canvas").FindChild ("Attacks").FindChild ("Attack2").GetComponent<Attack> ();
				break;
			case 3:
				EditOpacity (h1, 0);
				EditOpacity (h2, 0);
				EditOpacity (h3, 255);
				EditOpacity (h4, 0);
				AttackStats = transform.FindChild ("Canvas").FindChild ("Attacks").FindChild ("Attack3").GetComponent<Attack> ();
				break;
			case 4:
				EditOpacity (h1, 0);
				EditOpacity (h2, 0);
				EditOpacity (h3, 0);
				EditOpacity (h4, 255);
				AttackStats = transform.FindChild ("Canvas").FindChild ("Attacks").FindChild ("Attack4").GetComponent<Attack> ();
				break;
			}
			staminaCost = AttackStats.staminaCost;
			if (cur_stamina < staminaCost)
			{
				man.clickable = false;
				EditOpacity (h1, 0);
				EditOpacity (h2, 0);
				EditOpacity (h3, 0);
				EditOpacity (h4, 0);
				curAttack = 0;
				return;
			}
			curAttack = attackno;
			minDamage = AttackStats.minDamage;
			maxDamage = AttackStats.maxDamage;
			hitStun = AttackStats.hitStun;
			man.clickable = true;
		}
	}

	void EnemyAttack(){
		if (man.playerdeaths == 3)
			return;
		int attack = Random.Range (1, 4);
		switch (attack) {
		case 1:
			targeting = GameObject.Find ("dude1");
			if(targeting.GetComponent<Attacker>().dead){
				EnemyAttack ();
				return;
			}
			break;
		case 2:
			targeting = GameObject.Find ("dude2");
			if(targeting.GetComponent<Attacker>().dead){
				EnemyAttack ();
				return;
			}
			break;
		case 3:
			targeting = GameObject.Find ("dude3");
			if(targeting.GetComponent<Attacker>().dead){
				EnemyAttack ();
				return;
			}
			break;
		}
		Attacker att = targeting.GetComponent<Attacker> ();
		float dmg = Random.Range (-25, -30);
		att.changeHP (dmg);
		att.changeR (-70);
		changeR (-max_ready);
		DisplayDamage(dmg);
	}

	void Die(){
		cur_stamina = 0;
		cur_ready = 0;
		cur_health = 0;
		changeHP (0);
		changeS (0);
		changeR (0);
		dead = true;
		Destroy (transform.FindChild ("Canvas").gameObject);
		if (partynumber > 10) {
			man.deathcount++;
            if (man.deathcount == 3) {
                SceneManager.LoadScene("GameOverScene");
            }
        } else {
            man.playerdeaths++;
            if (man.playerdeaths == 3) {
                SceneManager.LoadScene("ExploreScene");
            }
		}
		//EditOpacity (transform.FindChild ("Canvas").gameObject, 0);
	}

	void DisplayDamage(float damage){
		man.pause = true;
		man.clickable = false;
		displayingDamage = true;
		self.transform.localScale = new Vector3 (self.transform.localScale.x * 2.5f, self.transform.localScale.y * 2.5f, self.transform.localScale.z);
		targeting.transform.localScale = new Vector3 (targeting.transform.localScale.x * 2.0f, targeting.transform.localScale.y * 2.0f, targeting.transform.localScale.z);
		Text temptxt = targeting.transform.FindChild("Canvas1").FindChild("Text").GetComponent<Text>();
		temptxt.text = Mathf.RoundToInt(damage).ToString();
		Text dmgtxt = targeting.transform.FindChild("Canvas1").FindChild("Text").GetComponent<Text>();
		Renderer splotchcolor = targeting.transform.FindChild("Canvas1").FindChild ("blood_splotch").GetComponent<Renderer>();
		dmgtxt.color = new Vector4(dmgtxt.color.r,dmgtxt.color.g,dmgtxt.color.b,1f);
		splotchcolor.material.color = new Vector4(splotchcolor.material.color.r,splotchcolor.material.color.g,splotchcolor.material.color.b,1f);
	}
}