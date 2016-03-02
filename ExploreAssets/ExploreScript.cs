﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class ExploreScript: MonoBehaviour {

    GameObject persistentDataObject;

    Button endDayButton;
    Dictionary<string, Image> actionPoints = new Dictionary<string, Image>();
    protected static int actionCount;
    protected static int actionPointLimit = 5;

    public static Text member1HP, Supply;

    protected CanvasGroup exploreEvent;
    protected Button event1, event2, event3, event4, event5;
    protected GameObject ev;

    protected System.Random rnd = new System.Random();

    GameObject building, house, store;

    GameObject sun;
    GameObject sky;
    SpriteRenderer skycolor;
    bool dayover = false;

    // Use this for initialization
    void Start() {
        persistentDataObject = GameObject.Find("PersistentData");
        DontDestroyOnLoad(persistentDataObject);

        building = GameObject.Find("Building");
        house = GameObject.Find("House");
        store = GameObject.Find("Store");

        endDayButton = GameObject.Find("EndDayButton").GetComponent<Button>();
        endDayButton.onClick.AddListener(() => EndDay(2));

        Image member1 = GameObject.Find("Member1").GetComponent<Image>();
        member1HP = member1.GetComponentsInChildren<Text>()[3];
        member1HP.text = DataScript.p1hp.ToString();
        Debug.Log(DataScript.p1hp);
        Supply = GameObject.Find("SupplyValue").GetComponent<Text>();
        Supply.text = DataScript.supply.ToString();


        for (int i=1; i<= actionPointLimit; i++) {
            String tempName = String.Format("AP{0}", i.ToString());
            actionPoints.Add(tempName, GameObject.Find(tempName).GetComponent<Image>());
            actionPoints[tempName].color = Color.red;
        }
        actionCount = actionPointLimit;

        exploreEvent = GameObject.Find("ExploreEvent").GetComponent<CanvasGroup>();
        exploreEvent.alpha = 0;
        ev = GameObject.Find("Event");
        event1 = GameObject.Find("Event1").GetComponent<Button>();
        event2 = GameObject.Find("Event2").GetComponent<Button>();
        event3 = GameObject.Find("Event3").GetComponent<Button>();
        event4 = GameObject.Find("Event4").GetComponent<Button>();
        event5 = GameObject.Find("Event5").GetComponent<Button>();

        sun = GameObject.Find("Sun");
        sky = GameObject.Find("sky");
        skycolor = sky.transform.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        float speedmultiplier = 15f;
        if (!dayover)
        {
            //moving the sun up/down
            
            if (sun.transform.position.x <= -7.5)
            {
                CustomTransUp(sun, 0.1f * speedmultiplier);
            }
            else if (sun.transform.position.x <= -7)
            {
                CustomTransUp(sun, 0.05f * speedmultiplier);
            }
            else if (sun.transform.position.x <= -6.5)
            {
                CustomTransUp(sun, 0.03f * speedmultiplier);
            }
            else if (sun.transform.position.x <= 0)
            {
                CustomTransUp(sun, 0.005f * speedmultiplier);
            }
            else if (sun.transform.position.x <= 6.5)
            {
                CustomTransDown(sun, 0.005f * speedmultiplier);
            }
            else if (sun.transform.position.x <= 7)
            {
                CustomTransDown(sun, 0.03f * speedmultiplier);
            }
            else if (sun.transform.position.x <= 7.5)
            {
                CustomTransDown(sun, 0.05f * speedmultiplier);
            }
            else
            {
                CustomTransDown(sun, 0.1f * speedmultiplier);
            }

            if (sun.transform.position.x <= 3)
            {
                //Debug.Log("Before Adjust r: " + skycolor.color.r + " g: " + skycolor.color.g + " b: " + skycolor.color.b);
                skycolor.color = new Color(skycolor.color.r + Time.fixedDeltaTime * .005f * speedmultiplier * -0.07059f, skycolor.color.g + Time.fixedDeltaTime * .005f * speedmultiplier * .596078f, skycolor.color.b + Time.fixedDeltaTime * .005f * speedmultiplier * 0.321569f);
                //Debug.Log("After Adjust r: " + skycolor.color.r + " g: " + skycolor.color.g + " b: " + skycolor.color.b);
            }
            else
            {
                //Color skycolor = sky.transform.GetComponent<SpriteRenderer>().color;
                skycolor.color = new Color(skycolor.color.r + Time.fixedDeltaTime * .01f * speedmultiplier * 0.160784f, skycolor.color.g + Time.fixedDeltaTime * .01f * speedmultiplier * -.86275f, skycolor.color.b + Time.fixedDeltaTime * .01f * speedmultiplier * -0.61961f);
            }
        }
        //moving the sun left/right
        if (sun.transform.position.x <= 8.43) {
            sun.transform.Translate(Vector2.right * Time.fixedDeltaTime * 0.03f * speedmultiplier);
            
        }
        else {
            if (!dayover) {
                dayover = true;
                EndDay(0);
            }
        }
    }

    //these are for moving the sun up and down
    void CustomTransUp(GameObject obj, float speed){
        obj.transform.Translate(Vector2.up * Time.fixedDeltaTime * speed);
    }
    void CustomTransDown(GameObject obj, float speed)
    {
        obj.transform.Translate(Vector2.down * Time.fixedDeltaTime * speed);
    }



    void onAwake() {
        DontDestroyOnLoad(this);
    }

    protected void EndDay(int type) {
        // update persistent data
        DataScript.p1hp = (int.Parse(member1HP.text));
        DataScript.supply = (int.Parse(Supply.text));

        // 0 for timeout, 1 for no ap
        ClearEvents();
        if (type == 0) {
            exploreEvent.alpha = 1;
            event1.GetComponentInChildren<Text>().text = "There is no more time in the day";

            event5.GetComponentInChildren<Text>().text = "1. Brave the night";
            event5.onClick.AddListener(() => SceneManager.LoadScene("CombatScene"));
        }
        else if (type == 1) {
            exploreEvent.alpha = 1;
            event1.GetComponentInChildren<Text>().text = "You ran out of AP";

            event5.GetComponentInChildren<Text>().text = "1. End the day";
            event5.onClick.AddListener(() => SceneManager.LoadScene("CombatScene"));
        }
        else {
            exploreEvent.alpha = 1;
            event1.GetComponentInChildren<Text>().text = "Prepare for the night";

            event4.GetComponentInChildren<Text>().text = "1. Changed my mind";
            event4.onClick.AddListener(() => Confirmed());

            event5.GetComponentInChildren<Text>().text = "2. Ready";
            event5.onClick.AddListener(() => SceneManager.LoadScene("CombatScene"));
        }
    }

    protected void LowerAP(int amount) {
        for (int i = 0; i < amount; i++) {
            if (actionCount > 0) {
                String tempName = String.Format("AP{0}", actionCount.ToString());
                actionPoints[tempName].color = Color.grey;
                actionCount--;
                if (actionCount == 0) {
                    building.GetComponent<Collider2D>().enabled = false;
                    house.GetComponent<Collider2D>().enabled = false;
                    store.GetComponent<Collider2D>().enabled = false;
                }
            }
        }
    }

    protected void ClearEvents() {
        event1.GetComponentInChildren<Text>().text = "";
        event1.GetComponent<Button>().onClick.RemoveAllListeners();
        event1.interactable = true;

        event2.GetComponentInChildren<Text>().text = "";
        event2.GetComponent<Button>().onClick.RemoveAllListeners();
        event2.interactable = true;

        event3.GetComponentInChildren<Text>().text = "";
        event3.GetComponent<Button>().onClick.RemoveAllListeners();
        event3.interactable = true;

        event4.GetComponentInChildren<Text>().text = "";
        event4.GetComponent<Button>().onClick.RemoveAllListeners();
        event4.interactable = true;

        event5.GetComponentInChildren<Text>().text = "";
        event5.GetComponent<Button>().onClick.RemoveAllListeners();
        event5.interactable = true;
    }

    protected void Confirmed() {
        exploreEvent.alpha = 0;
    }
}
