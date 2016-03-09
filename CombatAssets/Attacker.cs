using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Attacker : MonoBehaviour {
	
	GameObject manager;
	Manager man;
	GameObject spot;//spotlight
	GameObject spot2;//arrow_spotlight
	
	GameObject self;//references some dude
	
	GameObject h1;//attack
	GameObject h2;//border
	GameObject h3;//boxes
	GameObject h4;//when clicked
	
	int count;//used for animating attack
	bool displayingDamage;//when true, we are displaying damage and count increments
	GameObject damageText;//used for changing
	GameObject damageSplash;//the opacity of damage
	//Text damagetxt;//used for editing the text when doing damage
	//GameObject health_txt;
	Text healthtxt;
	//GameObject stam_txt;
	Text stamtxt;
	
	float opacity;//used for highlight
	float opacity_step;
	float displacement;
	float displacement_step;
	
	public bool dead;
	
	public int partynumber;//1,2,3 for us 11,12,13 for them;
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
	
	GameObject targeting;//who we are attacking
	public GameObject attack1;
	public GameObject attack2;
	public GameObject attack3;
	public GameObject attack4;

	float Ddamage, Dready1, Dready2, Dstamina;
	float initscalex, initscaley;
	int[] bench = new int[5];


	
	void Update(){
		if (!dead) {
			if (displayingDamage){//damage animation
				///Get Hold of Splotch+Text
				Text dmgtxt = null;
				Renderer splotchcolor = new Renderer();
				if (targeting != null)
				{
					dmgtxt = targeting.transform.FindChild("Canvas1").FindChild("dmg_text").GetComponent<Text>();
					splotchcolor = targeting.transform.FindChild("Canvas1").FindChild("blood_splotch").GetComponent<Renderer>();
				}
				//Make them fade over time
				
				count++;
				//Debug.Log (count);
				if (count < bench[0])//enlarge attacker and receiver
				{
					self.transform.localScale = new Vector3(self.transform.localScale.x * 1.02f, self.transform.localScale.y * 1.02f, self.transform.localScale.z);
					if (targeting != null && targeting != self)//dont enlarge if self target
						targeting.transform.localScale = new Vector3(targeting.transform.localScale.x * 1.01f, targeting.transform.localScale.y * 1.01f, targeting.transform.localScale.z);
				}
				else if (count == bench[0])//display damage
				{
					//retrieve the text and blood_splotch
					dmgtxt.text = Mathf.Floor(Ddamage*120).ToString();//set the text

					//make them visible
					dmgtxt.color = new Vector4(dmgtxt.color.r, dmgtxt.color.g, dmgtxt.color.b, 1f);
					splotchcolor.material.color = new Vector4(splotchcolor.material.color.r, splotchcolor.material.color.g, splotchcolor.material.color.b, 1f);
				}
				else if (count < bench[2])//start decrementing health
				{
					//if (partynumber < 11)
					//{
					if (targeting != null) { 
						targeting.GetComponent<Attacker>().changeHP(Ddamage);
						targeting.GetComponent<Attacker>().changeR(Dready1);
				}
					//}
					//else
					//{
					  //  Attacker att = targeting.GetComponent<Attacker>();
						//att.changeHP (Ddamage);
						//att.changeR (Dready1);
				   // }
				}
				else if (count < bench[3])//start reducing the size
				{
					self.transform.localScale = new Vector3(self.transform.localScale.x * .99f, self.transform.localScale.y * .99f, self.transform.localScale.z);
					if (targeting != null && targeting != self)
						targeting.transform.localScale = new Vector3(targeting.transform.localScale.x * .995f, targeting.transform.localScale.y * .995f, targeting.transform.localScale.z);
				}
				else if (count == bench[3])
				{//when the animation phase is over
					displayingDamage = false;
					count = 0;
					//resize to normal
					self.transform.localScale = new Vector3 (initscalex,initscaley, self.transform.localScale.z);
					if (targeting != null && targeting != self)
					{
						if (targeting.GetComponent<Attacker>().cur_health >= 0)
						{
							targeting.transform.localScale = new Vector3(targeting.GetComponent<Attacker>().initscalex, targeting.GetComponent<Attacker>().initscaley, targeting.transform.localScale.z);
						}
						//make the splotches go away
						dmgtxt.color = new Vector4(dmgtxt.color.r, dmgtxt.color.g, dmgtxt.color.b, 0f);
						splotchcolor.material.color = new Vector4(splotchcolor.material.color.r, splotchcolor.material.color.g, splotchcolor.material.color.b, 0f);
					}
						man.pause = false;
						man.clickable = false;
						man.targeter = 0;
						man.targeting = 0;
					
				}
				if (count > bench[1])
				{
					if (dmgtxt != null)
					{
						dmgtxt.color = new Vector4(dmgtxt.color.r, dmgtxt.color.g, dmgtxt.color.b, dmgtxt.color.a * .98f);
						splotchcolor.material.color = new Vector4(splotchcolor.material.color.r, splotchcolor.material.color.g, splotchcolor.material.color.b, splotchcolor.material.color.a * 0.98f);
					}
				}
				if (count < bench[4])
				{
					if (partynumber < 11)
					{
						changeR(Dready2);
						changeS(Dstamina);
					}
					else
					{
						changeR(Dready2);
					}
				}
			}else if (!man.pause) {//game is waiting to see who goes next
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
				} else if (opacity < 191) {				
					opacity = 191;
					opacity_step = 2;
				} else if (opacity > 255) {
					opacity = 255;
					opacity_step = -2;
				}
				if (displacement == 0)
				{
					displacement_step = 0.003f;
				}
				if (displacement < -0.025)
					displacement_step = 0.0015f;
				if (displacement > 0.025)
					displacement_step = -0.0015f;
				displacement += displacement_step;
				opacity += opacity_step;
				if (partynumber < 11)
				{
					EditOpacity(spot, opacity);
					EditOpacity(spot2, opacity);
					spot2.transform.position = new Vector3(spot2.transform.position.x, spot2.transform.position.y + displacement, spot2.transform.position.z);
				}
				
			}
		}else {//if you are dead you will shrink
			self.transform.localScale = new Vector3(self.transform.localScale.x*.98f, self.transform.localScale.y*.96f,1f);
			if(self.transform.localScale.y < .01)
			{
				Destroy (self);
			}
		}
	}
	
	void Start(){//mostly just initializing variables used for later
		dead = false;
		opacity = 0;
		manager = GameObject.Find ("Manager");
		man = manager.GetComponent<Manager> ();
		
		
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
			//Destroy(self);
			break;
		case 13:
			self = GameObject.Find ("dude13");
			//Destroy(self);
			break;
		}

		if (partynumber < 11)
		{
			spot = self.transform.FindChild("spotlight").gameObject;
			spot2 = self.transform.FindChild("arrow_spotlight").gameObject;
			EditOpacity(spot, 0);
			EditOpacity(spot2, 0);
			h1 = self.transform.FindChild("CombatUI").GetChild(0).FindChild("Canvas").FindChild("Attacks").FindChild("Attack1").FindChild("highlight").gameObject;
			h2 = self.transform.FindChild("CombatUI").GetChild(0).FindChild("Canvas").FindChild("Attacks").FindChild("Attack2").FindChild("highlight").gameObject;
			h3 = self.transform.FindChild("CombatUI").GetChild(0).FindChild("Canvas").FindChild("Attacks").FindChild("Attack3").FindChild("highlight").gameObject;
			h4 = self.transform.FindChild("CombatUI").GetChild(0).FindChild("Canvas").FindChild("Attacks").FindChild("Attack4").FindChild("highlight").gameObject;
			//Text bitchplease = self.transform.FindChild("Canvas1").FindChild("health").gameObject.transform.GetComponent<Text>();
			
			//stamtxt = self.transform.FindChild("Canvas1").FindChild("stam_text").GetComponent<Text>();
			EditOpacity2(h1, 0);
			EditOpacity2(h2, 0);
			EditOpacity2(h3, 0);
			EditOpacity2(h4, 0);
			//health_txt = self.transform.FindChild("Canvas1").FindChild("health_text").gameObject;
			//stam_txt = self.transform.FindChild("Canvas1").FindChild("stam_text").gameeObject;
			
		}
		healthtxt = self.transform.FindChild("Canvas1").FindChild("health_text").GetComponent<Text>();
		damageText = self.transform.FindChild ("Canvas1").FindChild ("dmg_text").gameObject;
		damageSplash = self.transform.FindChild ("Canvas1").FindChild ("blood_splotch").gameObject;
		damageText.GetComponent<Text> ().color = new Vector4 (damageText.GetComponent<Text> ().color.r, damageText.GetComponent<Text> ().color.g, damageText.GetComponent<Text> ().color.b, 0);

		EditOpacity (damageSplash, 0);

		if (partynumber < 11)
		{
			int i = partynumber - 1;
			if(!DataScript.Party[i].exists){
				Destroy(self);
			}
			cur_health = DataScript.Party[i].cur_health;
			max_health = DataScript.Party[i].max_health;
			//Debug.Log(cur_stamina);
			cur_stamina = DataScript.Party[i].cur_stamina;
		   // Debug.Log(cur_stamina);
			max_stamina = DataScript.Party[i].max_stamina;
		}
		changeHP(0);
		changeS(0);
		changeR(0);
		initscalex = self.transform.localScale.x;
		initscaley = self.transform.localScale.y;
		bench[0] = 30;//enlarging
		bench[1] = bench[0]+51;  //start to fade damage (one before and you show damage)
		bench[2] = bench[1] + 70;//start to decrease health
		bench[3] = bench[2] + 60;//decrease size
		bench[4] = bench[0] + 30;//adjust stamina and readiness
	}
	
	public void Execute(){//this is only used for Player-controlled characters; called when attacking something else
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

			//bench[0] = 30;//enlarging
			//bench[1] = bench[0] + 51;  //start to fade damage (one before and you show damage)
			//bench[2] = bench[1] + 70;//start to decrease health
			//bench[3] = bench[2] + 60;//decrease size
			//bench[4] = bench[0] + 30;//adjust stamina and readiness


			Ddamage = damage/(bench[2]-bench[0]-1);
			Dready1 = -hitStun / (bench[2] - bench[0] - 1);
			Dready2 = -max_ready/bench[4];
			Dstamina = -staminaCost / bench[4];
			//targeting.GetComponent<Attacker> ().changeHP (damage);
			//targeting.GetComponent<Attacker> ().changeR (-hitStun);
			//changeR (-max_ready);
			//changeS (-staminaCost);
			curAttack = 0;
			
			opacity = 0;
			EditOpacity (spot, 0);
			EditOpacity(spot2, 0);
			EditOpacity2 (h1, 255);
			EditOpacity2 (h2, 0);
			EditOpacity2 (h3, 0);
			EditOpacity2 (h4, 0);
		}
	}
	
	public void changeR(float amount){//Readiness
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
	
	public void changeHP(float amount){//Health
		cur_health += amount;
		if (cur_health > max_health)
			cur_health = max_health;
		if (cur_health < 0) {
			Die ();
			cur_health = 0;
		}
		//if (partynumber < 11)
		//{
			string one = Mathf.Ceil(cur_health).ToString();
			string two = max_health.ToString();
			healthtxt.text = one + "/" + two;
	   // }
			
		SetBar (healthBar, cur_health/max_health);
	}
	
	public void changeS(float amount){//Stamina
		if (partynumber < 11) {
			cur_stamina += amount;
			if (cur_stamina > max_stamina)
				cur_stamina = max_stamina;
			if (cur_stamina < 0)
				cur_stamina = 0;
			if (partynumber < 11)
			{
				string one = Mathf.Ceil(cur_stamina    ).ToString();
				string two = max_stamina.ToString();
				//stamtxt.text = one + "/" + two;
			}
			SetBar (staminaBar, cur_stamina / max_stamina);
		}
	}
	
	public void SetBar(GameObject bar, float new_stat){//Scale the bar to the approriate value
		bar.transform.localScale = new Vector3 (new_stat, bar.transform.localScale.y, bar.transform.localScale.z);
	}
	
	
	
	void EditOpacity(GameObject obj,float opac)//only works for GAMEOBJECTS
	{
		obj.GetComponent<Renderer>().material.color = new Vector4 (obj.GetComponent<Renderer>().material.color.r,//keep original color
																   obj.GetComponent<Renderer>().material.color.g,
																   obj.GetComponent<Renderer>().material.color.b,
																   opac/255);
	}
	void EditOpacity2(GameObject obj, float opac)//only works for GAMEOBJECTS
	{
		obj.GetComponent<Image>().color = new Vector4(obj.GetComponent<Image>().color.r,//keep original color
																   obj.GetComponent<Image>().color.g,
																   obj.GetComponent<Image>().color.b,
																   opac / 255);
	}


	//these stats are used to change hp,stamina,readiness etc.
	float minDamage;
	float maxDamage;
	float hitStun;
	float staminaCost;
	int curAttack;
	Attack AttackStats;//AttackStats are held in the Attack.cs folder
	
	public void AttackClick(int attackno){//we are clicking an attack for a player who is ready to attack
		if (man.targeter == partynumber) {
			if (attackno == curAttack) {//deselecting an attack
				man.clickable = false;
				EditOpacity2 (h1, 0);
				EditOpacity2 (h2, 0);
				EditOpacity2 (h3, 0);
				EditOpacity2 (h4, 0);
				curAttack = 0;
				return;
			} 
			switch (attackno) {
			case 1:
				EditOpacity2 (h1, 255);
				EditOpacity2 (h2, 0);
				EditOpacity2 (h3, 0);
				EditOpacity2 (h4, 0);
				AttackStats = DataScript.Party[partynumber-1].moveset[0];//transform.FindChild ("Canvas2").FindChild ("Attacks").FindChild ("Attack1").GetComponent<Attack> ();
				break;
			case 2:
				EditOpacity2 (h1, 0);
				EditOpacity2 (h2, 255);
				EditOpacity2 (h3, 0);
				EditOpacity2 (h4, 0);
				AttackStats = DataScript.Party[partynumber - 1].moveset[1];
				break;
			case 3:
				EditOpacity2 (h1, 0);
				EditOpacity2 (h2, 0);
				EditOpacity2 (h3, 255);
				EditOpacity2 (h4, 0);
				AttackStats = DataScript.Party[partynumber - 1].moveset[2];
				break;
			case 4:
				EditOpacity2 (h1, 0);
				EditOpacity2 (h2, 0);
				EditOpacity2 (h3, 0);
				EditOpacity2 (h4, 255);
				AttackStats = DataScript.Party[partynumber - 1].moveset[3];
				break;
			}
			staminaCost = AttackStats.stamina_cost;
			if (cur_stamina < staminaCost)//don't have the stamina, don't select
			{
				man.clickable = false;
				EditOpacity2 (h1, 0);
				EditOpacity2 (h2, 0);
				EditOpacity2 (h3, 0);
				EditOpacity2 (h4, 0);
				curAttack = 0;
				return;
			}
			//setup stats for being able to attack an enemy
			curAttack = attackno;
			minDamage = AttackStats.damage_min;
			maxDamage = AttackStats.damage_max;
			hitStun = AttackStats.hit_stun;
			man.clickable = true;
		}
	}
	
	void EnemyAttack(){//Enemies don't use the Execute() Function they get this instead;
		if (DataScript.population == 0)//if we are dead, don't spam attack
			return;
		int attack = Random.Range (1, 4);//People 1-3
		switch (attack) {
		case 1:
			targeting = GameObject.Find ("dude1");
			if (targeting != null && targeting.GetComponent<Attacker>().dead)
			{//don't attack dead people
				EnemyAttack ();
				return;
			}
			break;
		case 2:
			targeting = GameObject.Find ("dude2");
			if(targeting != null && targeting.GetComponent<Attacker>().dead){
				EnemyAttack ();
				return;
			}
			break;
		case 3:
			targeting = GameObject.Find ("dude3");
			if (targeting != null && targeting.GetComponent<Attacker>().dead)
			{
				EnemyAttack ();
				return;
			}
			break;
		}
		//similar to execute but no AttackStats
		//Attacker att = targeting.GetComponent<Attacker> ();
		float dmg = Random.Range (-25, -30);
		Ddamage = dmg / (bench[2] - bench[0] - 1);
		Dready1 = -hitStun / (bench[2] - bench[0] - 1);
		Dready2 = -max_ready / bench[4];

		//att.changeHP (dmg);
		//att.changeR (-70);
		//changeR (-max_ready);
		DisplayDamage(dmg);
	}
	
	void Die(){//called when we have 0 HP
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
			if (man.deathcount == 3)
			{
				UpdateDataScript();
				SceneManager.LoadScene("ExploreScene");
			}
		} else {
			DataScript.Party[partynumber - 1].exists = false;
			DataScript.population--;
			if (DataScript.population == 0) {
				SceneManager.LoadScene ("GameOverScene");
			}
		}
	}

	void UpdateDataScript()
	{
	   GameObject guy = GameObject.Find("askdjalksdj");
	   for (int i = 0; i < 3; i++)
	   {
		   if (!DataScript.Party[i].exists)
			   continue;
		   switch (i)
		   {
			   case 0:
				   guy = GameObject.Find("dude1");
				   break;
			   case 1:
				   guy = GameObject.Find("dude2");
				   break;
			   case 2:
				   guy = GameObject.Find("dude3");
				   break;
		   }
		   DataScript.Party[i].cur_health = guy.GetComponent<Attacker>().cur_health;
		   DataScript.Party[i].cur_stamina = guy.GetComponent<Attacker>().cur_stamina;
		   DataScript.Party[i].exists = !(guy.GetComponent<Attacker>().dead);
	   }
	   }
	
	void DisplayDamage(float damage){//called after both Execute() and DisplayDamage
		//pause the game and switch to animation state
		man.pause = true;
		man.clickable = false;
		displayingDamage = true;
		
		//enlarge self and enemy
		//self.transform.localScale = new Vector3 (self.transform.localScale.x * 2.5f, self.transform.localScale.y * 2.5f, self.transform.localScale.z);
		//if(targeting != self)
		//targeting.transform.localScale = new Vector3 (targeting.transform.localScale.x * 2.0f, targeting.transform.localScale.y * 2.0f, targeting.transform.localScale.z);
		
		////retrieve the text and blood_splotch
		//Text dmgtxt = targeting.transform.FindChild("Canvas1").FindChild("dmg_text").GetComponent<Text>();
		//Renderer splotchcolor = targeting.transform.FindChild("Canvas1").FindChild ("blood_splotch").GetComponent<Renderer>();
		//dmgtxt.text = Mathf.Floor(damage).ToString();//set the text
		
		////make them visible
		//dmgtxt.color = new Vector4(dmgtxt.color.r,dmgtxt.color.g,dmgtxt.color.b,1f);
		//splotchcolor.material.color = new Vector4(splotchcolor.material.color.r,splotchcolor.material.color.g,splotchcolor.material.color.b,1f);
	}
}