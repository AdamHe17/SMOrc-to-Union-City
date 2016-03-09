using UnityEngine;
using System.Collections;
using System;

public class InitAttacker : MonoBehaviour {

    DataScript data;
    Attacker atk;
    System.Random rnd = new System.Random();

    // Use this for initialization
    void Start() {
        atk = this.GetComponent<Attacker>();
        data = GameObject.Find("PersistentData").GetComponent<DataScript>();

        atk.max_health = rnd.Next(30, 40) + rnd.Next(5, 10) * data.dayCount;
        atk.cur_health = atk.max_health;
        Debug.Log("Hi");
    }
}
