using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class StoreScript : ExploreScript {

    GameObject data;
   // CanvasGroup exploreEvent;

    void Start() {

    }

    void Awake()
    {
        data = GameObject.Find("PersistentData");
        //exploreEvent = GameObject.Find("ExploreEvent").GetComponent<CanvasGroup>();
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
        if (Math.Abs(pos - this.transform.position.x - 6f) < 4f && !fastforward)
        {
            ExploreStore();
        }
        else {
            TooFar();
        }
    }

    void ExploreStore() {
        exploreEvent.alpha = 1;
        ClearEvents();

        twoline.text = "There is a store in town, this merchant specializes in healing";

        event1.GetComponentInChildren<Text>().text = "1. Check it out.";
        event1.onClick.AddListener(() => Store());


        event2.GetComponentInChildren<Text>().text = "2. Can't really afford anything anyways.";
        event2.onClick.AddListener(() => Confirmed());
    }

    void Store() {
        ClearEvents();

        oneline.text = "You check out his wares";

        event1.GetComponentInChildren<Text>().text = "1. Heal Player 1 (+10 HP, -1 supp)";
        event1.onClick.AddListener(() => StorePurchase(1));
        if (DataScript.supply < 1)
            event1.interactable = false;

        event2.GetComponentInChildren<Text>().text = "2. Heal Player 2 (+10 HP, -1 supp)";
        event2.onClick.AddListener(() => StorePurchase(2));
        if (DataScript.supply < 1)
            event2.interactable = false;

        event3.GetComponentInChildren<Text>().text = "3. Heal Player 3 (+10 HP, -1 supp)";
        event3.onClick.AddListener(() => StorePurchase(3));
        if (DataScript.supply < 1)
            event3.interactable = false;

        event4.GetComponentInChildren<Text>().text = "4. Energy Bar! (+70 Energy, -10 supp)";
        event4.onClick.AddListener(() => StorePurchase(4));
        if (DataScript.supply < 10)
            event4.interactable = false;

        event5.GetComponentInChildren<Text>().text = "5. Leave the store";
        event5.onClick.AddListener(() => Confirmed());
    }

    void StorePurchase(int item) {
        ClearEvents();

        int sup = int.Parse(Supply.text);

        switch(item) {
            case 1:
                if (sup < 1) {
                    NotEnough();
                }
                else {
                    Supply.text = (int.Parse(Supply.text) - 1).ToString();
                    DataScript.supply -= 1;
                    DataScript.Party[0].cur_health += 10;
                    data.GetComponent<DataScript>().UpdateStatusBars();
                    Confirmed();
                }
                break;
            case 2:
                if (sup < 1) {
                    NotEnough();
                }
                else {
                    Supply.text = (int.Parse(Supply.text) - 1).ToString();
                    DataScript.supply -= 1;
                    DataScript.Party[1].cur_health += 10;
                    data.GetComponent<DataScript>().UpdateStatusBars();
                    Confirmed();
                }
                break;
            case 3:
                if (sup < 1) {
                    NotEnough();
                }
                else {
                    Supply.text = (int.Parse(Supply.text) - 1).ToString();
                    DataScript.supply -= 1;
                    DataScript.Party[2].cur_health += 10;
                    data.GetComponent<DataScript>().UpdateStatusBars();
                    Confirmed();
                }
                break;
            case 4:
                if (sup < 10) {
                    NotEnough();
                }
                else {
                    Supply.text = (int.Parse(Supply.text) - 10).ToString();
                    DataScript.supply -= 10;
                    DataScript.Party[0].cur_stamina += 70;
                    DataScript.Party[1].cur_stamina += 70;
                    DataScript.Party[2].cur_stamina += 70;
                    data.GetComponent<DataScript>().UpdateStatusBars();
                    Confirmed();
                }
                break;
        }
    }

    void NotEnough() {
        ClearEvents();
        event1.GetComponentInChildren<Text>().text = "You're too poor son";

        event5.GetComponentInChildren<Text>().text = "1. check other items out";
        event5.onClick.AddListener(() => Store());
    }
}
