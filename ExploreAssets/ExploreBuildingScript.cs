using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class ExploreBuildingScript: ExploreScript {

    int exploreLimit = 6;
    GameObject data;

    void Start() {

    }

    void Awake() {
        data = GameObject.Find("PersistentData");
    }

    void OnMouseDown() {
        if (Math.Abs(pos - this.transform.position.x - 6f) < 4f) {
            ExploreBuilding();
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
        else if(Input.GetKeyDown(KeyCode.Alpha3)) {
            ExecuteEvents.Execute(event3.gameObject, pointer, ExecuteEvents.submitHandler);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            ExecuteEvents.Execute(event4.gameObject, pointer, ExecuteEvents.submitHandler);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha5)) {
            ExecuteEvents.Execute(event5.gameObject, pointer, ExecuteEvents.submitHandler);
        }
    }

    void ExploreBuilding() {
        exploreEvent.alpha = 1;
        ClearEvents();

        if (exploreLimit <= 0) {
            NothingLeft();
            return;
        }

        threeline.text = "You see a building off in the distance. It seems to have once been a factory.";
        

        event1.GetComponentInChildren<Text>().text = "1. Search the building (-2 AP)";
        event1.onClick.AddListener(() => InsideEvent(2));
        if (actionCount < 2)
            event1.interactable = false;

        event2.GetComponentInChildren<Text>().text = "2. Scout the surrounding area (-3 AP)";
        event2.onClick.AddListener(() => OutsideEvent(3));
        if (actionCount < 3)
            event2.interactable = false;

        event3.GetComponentInChildren<Text>().text = "3. Stay here";
        event3.onClick.AddListener(() => Confirmed());
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

        twoline.text = "You hear noises inside the building";

        event1.GetComponentInChildren<Text>().text = "1. Venture further into the building. (-1 AP)";
        event1.onClick.AddListener(() => InsideEvent(1));
        if (actionCount < 1)
            event1.interactable = false;

        event2.GetComponentInChildren<Text>().text = "2. Leave this building.";
        event2.onClick.AddListener(() => Confirmed());
    }
    
    void InsideEvent(int APCost) {
        LowerAP(APCost);
        ClearEvents();
        exploreLimit -= 1;
        int check = rnd.Next(0, 10);

        if (check < 2) {
            check = rnd.Next(0, 3);
            oneline.text = "jumped on by a sleeper. (-5 HP)";
            DataScript.Party[check].cur_health -= 5;
            data.GetComponent<DataScript>().UpdateStatusBars();
        }
        else if (check < 7) {
            oneline.text = "Found some supplies. (+3 Supplies)";
            Supply.text = (int.Parse(Supply.text) + 3).ToString();
            DataScript.supply += 3;
        }
        else {
            LowerAP(1);
            oneline.text = "Ran away from a zombie. (-1 AP)";
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

    void OutsideEvent(int APCost) {
        LowerAP(APCost);
        ClearEvents();
        exploreLimit -= 1;
        int check = rnd.Next(0, 10);

        if (check < 2) {
            check = rnd.Next(0, 3);
            oneline.text = "Ran into a trap. (-7 HP)";
            DataScript.Party[check].cur_health -= 7;
            data.GetComponent<DataScript>().UpdateStatusBars();
        }
        else if (check < 7) {
            oneline.text = "Found some bread. (+1 Supplies)";
            Supply.text = (int.Parse(Supply.text) + 1).ToString();
            DataScript.supply += 3;
        }
        else {
            LowerAP(1);
            oneline.text = "Sneaked past a brute. (-1 AP)";
        }

        event1.GetComponentInChildren<Text>().text = "1. Ok";
        event1.onClick.AddListener(() => Outside());
    }

    void NothingLeft() {
        ClearEvents();

        twoline.text = "There is nothing left in this building.";

        event1.GetComponentInChildren<Text>().text = "1. Leave";
        event1.onClick.AddListener(() => Confirmed());
    }
}
