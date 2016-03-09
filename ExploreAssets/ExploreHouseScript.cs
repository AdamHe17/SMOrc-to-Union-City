using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class ExploreHouseScript : ExploreScript {

    int exploreLimit = 4;
    GameObject data;

    void Start() {

    }

    void Awake() {
        data = GameObject.Find("PersistentData");
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
    }

    void OnMouseDown() {
        int check = rnd.Next(0, 10);
        if (Math.Abs(pos - this.transform.position.x - 6f) < 5f) {
            ExploreHouse(check);
        }
        else {
            TooFar();
        }
    }

    void ExploreHouse(int check) {
        exploreEvent.alpha = 1;
        ClearEvents();

        if (exploreLimit <= 0) {
            NothingLeft();
            return;
        }

        twoline.text = "There is small house nearby, all the windows are intact and there seems to be no zombies inside.";

        event1.GetComponentInChildren<Text>().text = "1. Explore it (-1 AP)";
        event1.onClick.AddListener(() => InsideEvent(1));
         if (actionCount < 1)
            event1.interactable = false;

        event2.GetComponentInChildren<Text>().text = "2. Ignore it";
        event2.onClick.AddListener(() => Confirmed());
    }

    void Inside() {
        ClearEvents();

        if (actionCount <= 0) {
            EndDay(1);
            return;
        }

        if (exploreLimit <= 0) {
            NothingLeft();
            return;
        }

        twoline.text = "You explored the house for a bit but there are still rooms inside";

        event1.GetComponentInChildren<Text>().text = "1. Search another room (-1 AP)";
        event1.onClick.AddListener(() => InsideEvent(1));
        if (actionCount < 1)
            event1.interactable = false;

        event2.GetComponentInChildren<Text>().text = "2. Leave the house.";
        event2.onClick.AddListener(() => Confirmed());
    }

    void InsideEvent(int APCost) {
        LowerAP(APCost);
        ClearEvents();
        exploreLimit -= 1;
        int check = rnd.Next(0, 10);

        if (check < 2) {
            oneline.text = "jumped on by a sleeper. (-5 HP)";
            ChangeRandomPartyHP(-5);
            data.GetComponent<DataScript>().UpdateStatusBars();
        }
        else if (check < 7) {
            oneline.text = "Found some supplies. (+2 Supplies)";
            DataScript.supply += 2;
            Supply.text = (int.Parse(Supply.text) + 2).ToString();
        }
        else {
            oneline.text = "No one is here.";
        }

        event1.GetComponentInChildren<Text>().text = "1. Ok";
        event1.onClick.AddListener(() => Inside());
    }

    void Outside() {
        ClearEvents();

        if (actionCount <= 0) {
            EndDay(1);
            return;
        }

        if (exploreLimit <= 0) {
            NothingLeft();
            return;
        }

        event1.GetComponentInChildren<Text>().text = "1. Go inside and look for supplies. (-1 AP)";
        event1.onClick.AddListener(() => InsideEvent(1));
        if (actionCount < 1)
            event1.interactable = false;

        event2.GetComponentInChildren<Text>().text = "2. Leave this area.";
        event2.onClick.AddListener(() => Confirmed());
    }

    void OutsideEvent() {
        LowerAP(1);
        ClearEvents();
        exploreLimit -= 1;
        int check = rnd.Next(0, 10);

        if (check < 2) {
            event1.GetComponentInChildren<Text>().text = "Ran into a trap. (-4 HP)";
            ChangeRandomPartyHP(-4);
            data.GetComponent<DataScript>().UpdateStatusBars();
        }
        else if (check < 7) {
            event1.GetComponentInChildren<Text>().text = "Found some scrap wood. (+2 Supplies)";
            DataScript.supply += 2;
            Supply.text = (int.Parse(Supply.text) + 2).ToString();
        }
        else {
            LowerAP(1);
            event1.GetComponentInChildren<Text>().text = "Sneaked past a brute. (-1 AP)";
        }

        event5.GetComponentInChildren<Text>().text = "1. Ok";
        event5.onClick.AddListener(() => Confirmed());
    }

    void NothingLeft() {
        ClearEvents();

        twoline.text = "There is nothing left in this building.";

        event1.GetComponentInChildren<Text>().text = "1. Leave";
        event1.onClick.AddListener(() => Confirmed());
    }
}
