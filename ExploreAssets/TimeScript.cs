using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class TimeScript : ExploreScript {
    GameObject sun;
    bool dayover = false;

	void Start () {
        sun = GameObject.Find("Sun");
	}
	
	void Update () {
        if (sun.transform.position.x <= 7.68) {
            sun.transform.Translate(Vector2.right * Time.fixedDeltaTime * 5f);
        }
        else {
            if (!dayover) {
                dayover = true;
                EndDay(0);
            }
        }
    }
}
