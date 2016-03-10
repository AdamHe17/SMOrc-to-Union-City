using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class DudeBuildingScript : ExploreScript {

    GameObject data;

    void Start() {

    }

    void Awake() {
        data = GameObject.Find("PersistentData");
    }

    void OnMouseDown() {
        if (Math.Abs(pos - this.transform.position.x - 6f) < 4f && !fastforward) {
            TalkToDude();
        }
        else {
            TooFar();
        }
    }

    void Update() {
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            ExecuteEvents.Execute(event1.gameObject, pointer, ExecuteEvents.submitHandler);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            ExecuteEvents.Execute(event2.gameObject, pointer, ExecuteEvents.submitHandler);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            ExecuteEvents.Execute(event3.gameObject, pointer, ExecuteEvents.submitHandler);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            ExecuteEvents.Execute(event4.gameObject, pointer, ExecuteEvents.submitHandler);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5)) {
            ExecuteEvents.Execute(event5.gameObject, pointer, ExecuteEvents.submitHandler);
        }
        //if (exploreEvent.alpha != 0 || fastforward) {
        //    this.GetComponent<BoxCollider2D>().enabled = false;
        //}
        //else {
        //    this.GetComponent<BoxCollider2D>().enabled = true;
        //}
    }

    void TalkToDude() {
        ClearEvents();
        exploreEvent.alpha = 1;
        int cost = rnd.Next(5, 10);

        twoline.text = "A person walks up to the caravan";

        event1.GetComponentInChildren<Text>().text = string.Format("1. Ask him to join. (-{0} Supply)", cost);
        event1.onClick.AddListener(() => Added(cost));
        if (DataScript.supply < cost || DataScript.population >= 3) {
            event1.interactable = false;
        }

        event2.GetComponentInChildren<Text>().text = "2. Turn him away";
        event2.onClick.AddListener(() => Confirmed());
    }

    void Added(int cost) {
        ClearEvents();
        for (int i = 0; i < 3; i++) {
            if (DataScript.Party[i].exists == false) {
                DataScript.Party[i] = new Person(true);
                break;
            }
        }

        DataScript.population += 1;

        data.GetComponent<DataScript>().member1 = GameObject.Find("Member1");
        data.GetComponent<DataScript>().member2 = GameObject.Find("Member2");
        data.GetComponent<DataScript>().member3 = GameObject.Find("Member3");
        data.GetComponent<DataScript>().UpdateStatusBars();
        data.GetComponent<DataScript>().UpdateMoveSets();

        DataScript.supply -= cost;
        Supply.text = (int.Parse(Supply.text) - cost).ToString();

        oneline.text = "He joined the party";

        event1.GetComponentInChildren<Text>().text = "1. Yay!";
        event1.onClick.AddListener(() => Destroy(gameObject));
        event1.onClick.AddListener(() => Confirmed());
    }

    new void TooFar() {
        ClearEvents();
        exploreEvent.alpha = 1;

        twoline.text = "There is a in the distance (move closer to talk).";

        event1.GetComponentInChildren<Text>().text = "1. Ok";
        event1.onClick.AddListener(() => Confirmed());
    }
}
