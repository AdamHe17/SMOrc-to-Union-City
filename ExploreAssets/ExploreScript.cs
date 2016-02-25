using UnityEngine;
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

    protected System.Random rnd = new System.Random();

    GameObject building, house, store;

    GameObject sun;
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
        event1 = GameObject.Find("Event1").GetComponent<Button>();
        event2 = GameObject.Find("Event2").GetComponent<Button>();
        event3 = GameObject.Find("Event3").GetComponent<Button>();
        event4 = GameObject.Find("Event4").GetComponent<Button>();
        event5 = GameObject.Find("Event5").GetComponent<Button>();

        sun = GameObject.Find("Sun");
    }

    // Update is called once per frame
    void Update() {
        if (sun.transform.position.x <= 7.68) {
            sun.transform.Translate(Vector2.right * Time.fixedDeltaTime * 0.03f);
        }
        else {
            if (!dayover) {
                dayover = true;
                EndDay(0);
            }
        }
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

        event2.GetComponentInChildren<Text>().text = "";
        event2.GetComponent<Button>().onClick.RemoveAllListeners();

        event3.GetComponentInChildren<Text>().text = "";
        event3.GetComponent<Button>().onClick.RemoveAllListeners();

        event4.GetComponentInChildren<Text>().text = "";
        event4.GetComponent<Button>().onClick.RemoveAllListeners();

        event5.GetComponentInChildren<Text>().text = "";
        event5.GetComponent<Button>().onClick.RemoveAllListeners();
    }

    protected void Confirmed() {
        exploreEvent.alpha = 0;
    }
}
