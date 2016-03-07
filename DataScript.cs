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
	GameObject member1, member2, member3;
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
				DataScript.Party[2] = new Person(true);
				//Debug.Log(DataScript.Party[0].cur_health);
				member1 = GameObject.Find("Member1");
				member2 = GameObject.Find("Member2");
				member3 = GameObject.Find("Member3");
				UpdateStatusBars();
			}
			UpdateStatusBars();
		}
		
	}
	public void UpdateStatusBars()
	{
		UpdateStatusBarsHelp(member1,0);
		UpdateStatusBarsHelp(member2,1);
		UpdateStatusBarsHelp(member3,2);
	}

	void UpdateStatusBarsHelp(GameObject mem,int id)
	{
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
		//Debug.Log(id);
		Text healthtxt = mem.transform.FindChild("HPValue").GetComponent<Text>();
		Text stamtxt = mem.transform.FindChild("StamValue").GetComponent<Text>();
		SetBar(Health, cur_health / max_health);
		SetBar(Stamina, cur_stamina / cur_health);
		Debug.Log(max_health);
		healthtxt.text = Mathf.Ceil(cur_health).ToString() + "/" + max_health.ToString();
		stamtxt.text = Mathf.Ceil(cur_stamina).ToString() + "/" + max_stamina.ToString();
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
		switch (hair_type)
		{
			case 1:
				hair_color = Color.HSVToRGB(0, 0, 1);
				break;
		}
		switch (skin_type)
		{
			case 1:
				skin_color = Color.HSVToRGB(0, 0, 1);
				break;
		}
		switch (shirt_type)
		{
			case 1:
				shirt_color = Color.HSVToRGB(0, 0, 1);
				break;
		}
		switch (pants_type)
		{
			case 1:
				pants_color = Color.HSVToRGB(0, 0, 1);
				break;
		}
		switch (shoes_type)
		{
			case 1:
				shoes_color = Color.HSVToRGB(0, 0, 1);
				break;
		}
	}
	
}