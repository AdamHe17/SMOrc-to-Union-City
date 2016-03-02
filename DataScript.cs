using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;


public class DataScript : MonoBehaviour {

    public static Person[] Party = new Person[3];
	public static int supply = 10;
    public static int population = 1;
    public static bool gamestarted = false;
    void Awake()
    {
        if (!gamestarted)
        {
            gamestarted = !gamestarted;
            DataScript.Party[0] = new Person(true);
            DataScript.Party[1] = new Person(true);
            DataScript.Party[2] = new Person(true);
            Debug.Log(DataScript.Party[0].cur_health);
        }
        
    }
    

	
}

public struct Person
{
    public int level;
    public float cur_health, max_health, cur_stamina, max_stamina, xp, xp2lvl;
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
    }
    
}