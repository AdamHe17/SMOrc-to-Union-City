using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using UnityEditor;



public class DataScript : MonoBehaviour {

	public static Person[] Party = new Person[3];
	public static int supply = 10;
	public static int population = 1;
	public static bool gamestarted;
	public static bool gamestarted2;
	public static float Progress;
	public GameObject member1, member2, member3;
	public int dayCount = 1;
	//public static Sprite Att0, Att1, Att2, Att3, Att4, Att5, Att6, Att7, Att8;
	public Sprite supersayan;
	void Awake()
	{
		if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("ExploreScene"))
		{
			if (!gamestarted)
			{
				Debug.Log("hello");
				gamestarted = !gamestarted;
				DataScript.Party[0] = new Person(true);
				DataScript.Party[1] = new Person(true);
				DataScript.Party[2] = new Person(false);
				//Debug.Log(DataScript.Party[0].cur_health);
				member1 = GameObject.Find("Member1");
				member2 = GameObject.Find("Member2");
				member3 = GameObject.Find("Member3");
				UpdateStatusBars();
			}
			member1 = GameObject.Find("Member1");
			member2 = GameObject.Find("Member2");
			member3 = GameObject.Find("Member3");
			UpdateStatusBars();
			UpdateMoveSets();
		}
		//Att0 = Resources.Load("punch", typeof(Sprite)) as Sprite;
		
	}

	void Update()
	{
		if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("ExploreScene"))
		{
			SetBar(GameObject.Find("Progress").transform.FindChild("Bar").gameObject, Progress / 10000);
			GameObject.Find("ProgressCaravan").transform.localPosition = new Vector3(-500 + Progress / 10, transform.localPosition.y, transform.localPosition.z);
		}
		if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("CombatScene"))
		{
			UpdateStatusBarsCombat();
			Debug.Log("hello");
		}
	}

	public void UpdateStatusBars()
	{
		UpdateStatusBarsHelp(member1,0);
		UpdateStatusBarsHelp(member2,1);
		UpdateStatusBarsHelp(member3,2);
	}

	public void UpdateStatusBarsCombat()
	{
		GameObject mem1, mem2, mem3;
		//GameObject mem1 = null;
		//GameObject mem2 = null;
		//GameObject mem3 = null;
		mem1 = mem2 = mem3 = null;
		if(Party[0].exists)
			mem1 = GameObject.Find("dude1").transform.FindChild("CombatUI").GetChild(0).gameObject; // FindChild("CombatUI").GetChild(0);
		if(Party[1].exists)

			mem2 = GameObject.Find("dude2").transform.FindChild("CombatUI").GetChild(0).gameObject;
		if(Party[2].exists)
			mem3 = GameObject.Find("dude3").transform.FindChild("CombatUI").GetChild(0).gameObject;
		//Debug.Log(mem1.GetComponent<RectTransform>().position.x);
		if(Party[0].exists)
			UpdateStatusBarsHelp2(mem1, 0);
	   if(Party[1].exists)
			UpdateStatusBarsHelp2(mem2, 1);
	   if(Party[2].exists)
			UpdateStatusBarsHelp2(mem3, 2);
	}

	void UpdateStatusBarsHelp(GameObject mem,int id)
	{
		if (Party[id].exists)
		{
			float xp = Party[id].xp;
			float xp2lvl = Party[id].xp2lvl;
			if (xp >= xp2lvl)
			{
				Party[id].xp -= Party[id].xp2lvl;
				Party[id].xp2lvl *= 1.5f;
				Party[id].level++;
				int temp1 = UnityEngine.Random.Range(10, 15);
				int temp2 = UnityEngine.Random.Range(10, 15);
				Party[id].max_health += temp1;
				Party[id].cur_health += temp1;
				Party[id].max_stamina += temp2;
				Party[id].cur_stamina += temp2;
			}
			float cur_stamina = Party[id].cur_stamina;
			float cur_health = Party[id].cur_health;
			float max_stamina = Party[id].max_stamina;
			float max_health = Party[id].max_health;
			if (cur_stamina >= max_stamina)
			{
				Party[id].cur_stamina = max_stamina;
				cur_stamina = max_stamina;
			}
			if (cur_health >= max_health)
			{
				Party[id].cur_health = max_health;
				cur_health = max_health;
			}

			GameObject Health = mem.transform.FindChild("Canvas").FindChild("Bars").FindChild("Health").gameObject;
			GameObject Stamina = mem.transform.FindChild("Canvas").FindChild("Bars").FindChild("Stamina").gameObject;
            GameObject XP = mem.transform.FindChild("Canvas").FindChild("Bars").FindChild("XP").gameObject;
            //Debug.Log(id);
			Text healthtxt = mem.transform.FindChild("HPValue").GetComponent<Text>();
			Text stamtxt = mem.transform.FindChild("StamValue").GetComponent<Text>();
			Text lvltxt = mem.transform.FindChild("Level").GetComponent<Text>();
			lvltxt.text = "Level: " + (Party[id].level).ToString();
			SetBar(Health, cur_health / max_health);
			SetBar(Stamina, cur_stamina / cur_health);
            SetBar(XP, xp / xp2lvl);
			//Debug.Log(max_health);
			healthtxt.text = Mathf.Ceil(cur_health).ToString() + "/" + max_health.ToString();
			stamtxt.text = Mathf.Ceil(cur_stamina).ToString() + "/" + max_stamina.ToString();
		}
	}

	void UpdateStatusBarsHelp2(GameObject mem, int id)
	{
		if (Party[id].exists)
		{
			float xp = Party[id].xp;
			float xp2lvl = Party[id].xp2lvl;
			if (xp >= xp2lvl)
			{
				Party[id].xp -= Party[id].xp2lvl;
				Party[id].xp2lvl *= 1.5f;
				Party[id].level++;
				Party[id].max_health += UnityEngine.Random.Range(10, 15);
				Party[id].max_stamina += UnityEngine.Random.Range(10, 15);
			}
			float cur_stamina = Party[id].cur_stamina;
			float cur_health = Party[id].cur_health;
			float max_stamina = Party[id].max_stamina;
			float max_health = Party[id].max_health;
			if (cur_stamina >= max_stamina)
			{
				Party[id].cur_stamina = max_stamina;
				cur_stamina = max_stamina;
			}
			if (cur_health >= max_health)
			{
				Party[id].cur_health = max_health;
				cur_health = max_health;
			}

			//GameObject Health = mem.transform.parent.parent.FindChild("Canvas").FindChild("Bars").FindChild("Health").gameObject;
			//GameObject Stamina = mem.transform.parent.parent.FindChild("Canvas").FindChild("Bars").FindChild("Stamina").gameObject;
			//Debug.Log(id);
			Text healthtxt = mem.transform.parent.parent.FindChild("Canvas1").FindChild("health_text").GetComponent<Text>();

			Text stamtxt = mem.transform.parent.parent.FindChild("Canvas1").FindChild("stam_text").GetComponent<Text>();
			Text lvltxt = mem.transform.FindChild("Level").GetComponent<Text>();
			lvltxt.text = "Level: " + (Party[id].level).ToString();
			//SetBar(Health, cur_health / max_health);
			//SetBar(Stamina, cur_stamina / cur_health);
			//Debug.Log(max_health);
			healthtxt.text = Mathf.Ceil(cur_health).ToString() + "/" + max_health.ToString();
			stamtxt.text = Mathf.Ceil(cur_stamina).ToString() + "/" + max_stamina.ToString();
		}
	}

	public void UpdateMoveSets()
	{
		UpdateMoveSetsHelp(member1, 0);
		UpdateMoveSetsHelp(member2, 1);
		UpdateMoveSetsHelp(member3, 2);
	}

	public void UpdateMoveSets2()
	{
		GameObject mem1, mem2, mem3;
		mem1 = mem2 = mem3 = null;
		if (Party[0].exists)
			mem1 = GameObject.Find("dude1").transform.FindChild("CombatUI").GetChild(0).gameObject; // FindChild("CombatUI").GetChild(0);
		if (Party[1].exists)

			mem2 = GameObject.Find("dude2").transform.FindChild("CombatUI").GetChild(0).gameObject;
		if (Party[2].exists)
			mem3 = GameObject.Find("dude3").transform.FindChild("CombatUI").GetChild(0).gameObject;
		//Debug.Log(mem1.GetComponent<RectTransform>().position.x);
		if (Party[0].exists)
			UpdateMoveSetsHelp2(mem1, 0);
		if (Party[1].exists)
			UpdateMoveSetsHelp2(mem2, 1);
		if (Party[2].exists)
			UpdateMoveSetsHelp2(mem3, 2);
	}

	void UpdateMoveSetsHelp(GameObject mem, int id)
	{
		if (Party[id].exists)
		{
			for (int attackid = 0; attackid < 4; attackid++)
			{
				GameObject temp = mem.transform.FindChild("Canvas").FindChild("Attacks").GetChild(attackid).gameObject;
				temp.transform.GetChild(0).GetComponentInChildren<Text>().text = Party[id].moveset[attackid].damage_min + "-" + Party[id].moveset[attackid].damage_max;
				temp.transform.GetChild(1).GetComponentInChildren<Text>().text = (Party[id].moveset[attackid].stamina_cost).ToString();
				temp.GetComponent<Image>().sprite = GameObject.Find("AttackSprites").transform.GetChild(attackid).GetComponent<SpriteRenderer>().sprite;
			}
		}
	}

	void UpdateMoveSetsHelp2(GameObject mem, int id)
	{
		if (Party[id].exists)
		{
			for (int attackid = 0; attackid < 4; attackid++)
			{
				GameObject temp = mem.transform.FindChild("Canvas").FindChild("Attacks").GetChild(attackid).gameObject;
				temp.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = Party[id].moveset[attackid].damage_min + "-" + Party[id].moveset[attackid].damage_max;
				temp.transform.GetChild(2).GetComponentInChildren<Text>().text = (Party[id].moveset[attackid].stamina_cost).ToString();
				temp.GetComponent<Image>().sprite = GameObject.Find("AttackSprites").transform.GetChild(attackid).GetComponent<SpriteRenderer>().sprite;
			}
		}
	}

	void SetBar(GameObject bar, float new_stat)
	{
		bar.transform.localScale = new Vector3(new_stat, bar.transform.localScale.y, bar.transform.localScale.z);
	}
	

	
}

public struct Person
{
	public int level;
	public float cur_health, max_health, cur_stamina, max_stamina, xp, xp2lvl;
	private int hair_type, skin_type, shirt_type, pants_type, shoes_type;
	public Color hair_color, skin_color, shirt_color, pants_color, shoes_color;
	
	public bool exists;
	public Attack[] moveset;
	public Person(bool exist)
	{
		exists = exist;
		cur_health = 100;
		max_health = 100;
		cur_stamina = 50;
		max_stamina = 100;
		level = 1;
		xp = 0;
		xp2lvl = 10;
		hair_type  = UnityEngine.Random.Range(1, 1);
		skin_type  = UnityEngine.Random.Range(1, 1);
		shirt_type = UnityEngine.Random.Range(1, 1);
		pants_type = UnityEngine.Random.Range(1, 1);
		shoes_type = UnityEngine.Random.Range(1, 1);
		moveset = new Attack[4];
		moveset[0] = new Attack(0);
		moveset[1] = new Attack(1);
		moveset[2] = new Attack(2);
		moveset[3] = new Attack(3);
		//default color
		hair_color = skin_color = shirt_color = pants_color = shoes_color = Color.HSVToRGB(0, 0, 1);
		switch (hair_type)
		{
			case 1:
				break;
		}
		switch (skin_type)
		{
			case 1:
				break;
		}
		switch (shirt_type)
		{
			case 1:
				break;
		}
		switch (pants_type)
		{
			case 1:
				break;
		}
		switch (shoes_type)
		{
			case 1:
				break;
		}
	}
	
}

public struct Attack
{
	public int id;
	public float damage_min;
	public float damage_max;
	public float stamina_cost;
	public float hit_stun;
	//private Sprite img;
	public Attack(int movno)
	{
		id = movno;
		//img = DataScript.Att0;
		// damage_min = damage_max = stamina_cost = hit_stun = 10;
		switch (movno)
		{
			case 0://punch
				damage_min = 10;
				damage_max = 15;
				stamina_cost = 15;
				hit_stun = 35;
				break;
			case 1://kick
				damage_min = 15;
				damage_max = 25;
				stamina_cost = 20;
				hit_stun = 20;
				break;
			case 2://poke
				damage_min = 1;
				damage_max = 3;
				stamina_cost = 5;
				hit_stun = 50;
				break;
			case 3://supersayan
				damage_min = 50;
				damage_max = 75;
				stamina_cost = 80;
				hit_stun = 100;
				break;
			case 4://bite
				damage_min = 0;
				damage_max = 0;
				stamina_cost = 0;
				hit_stun = 0;
				break;
			case 5://chop
				damage_min = 0;
				damage_max = 0;
				stamina_cost = 0;
				hit_stun = 0;
				break;
			case 6://claw
				damage_min = 0;
				damage_max = 0;
				stamina_cost = 0;
				hit_stun = 0;
				break;
			case 7://roundhouse
				damage_min = 0;
				damage_max = 0;
				stamina_cost = 0;
				hit_stun = 0;
				break;
			case 8://thousandfists
				damage_min = 0;
				damage_max = 0;
				stamina_cost = 0;
				hit_stun = 0;
				break;
			default:
				damage_min = 0;
				damage_max = 0;
				stamina_cost = 0;
				hit_stun = 0;
				break;
		}
	}
}