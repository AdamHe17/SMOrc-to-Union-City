using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class ExploreScript : MonoBehaviour {

    GameObject persistentDataObject;

    GameObject scrollingBg;
    protected float moveSpeed, pos, lastBuildingPos;
    protected int n_backgrounds, storeTimer;
    protected System.Random buildingRnd = new System.Random();

    Button endDayButton;
    Dictionary<string, Image> actionPoints = new Dictionary<string, Image>();
    protected static int actionCount;
    protected static int actionPointLimit = 5;

    protected static Text member1HP, Supply;

    protected CanvasGroup exploreEvent;
    protected Button event1, event2, event3, event4, event5;
    protected GameObject ev;
    public Text oneline, twoline, threeline;

    protected System.Random rnd = new System.Random();

    GameObject building, house, store;

    GameObject sun;
    GameObject sky;
    SpriteRenderer skycolor;
    float timewarp;
    bool fastforward = false;
    bool dayover = false;

    // Use this for initialization
    void Start() {
        // if (transform.position.x == 0 && transform.position.y == 0)
        // {
        persistentDataObject = GameObject.Find("PersistentData");
        DontDestroyOnLoad(persistentDataObject);

        scrollingBg = GameObject.Find("ScrollingBackground");
        moveSpeed = 10f;
        n_backgrounds = 1;
        lastBuildingPos = 4.9f;
        storeTimer = 3;

        building = GameObject.Find("Building");
        house = GameObject.Find("House");
        store = GameObject.Find("Store");

        sun = GameObject.Find("Sun");
        //Debug.Log(sun);
        sky = GameObject.Find("sky");

        skycolor = sky.transform.GetComponent<SpriteRenderer>();

        endDayButton = GameObject.Find("EndDayButton").GetComponent<Button>();
        endDayButton.onClick.AddListener(() => EndDay(2));

        //Image member1 = GameObject.Find("Member1").GetComponent<Image>();
        //member1HP = member1.GetComponentsInChildren<Text>()[3];

        //member1HP.text = Math.Ceiling(DataScript.Party[0].cur_health).ToString();
        //Debug.Log(DataScript.Party[0].cur_health);
        //Debug.Log(DataScript.p1hp);
        Supply = GameObject.Find("SupplyValue").GetComponent<Text>();
        Supply.text = DataScript.supply.ToString();


        for (int i = 1; i <= actionPointLimit; i++) {
            String tempName = String.Format("AP{0}", i.ToString());
            actionPoints.Add(tempName, GameObject.Find(tempName).GetComponent<Image>());
            actionPoints[tempName].color = Color.red;
        }
        actionCount = actionPointLimit;

        exploreEvent = GameObject.Find("ExploreEvent").GetComponent<CanvasGroup>();
        exploreEvent.alpha = 0;
        ev = GameObject.Find("Event");
        oneline = ev.transform.FindChild("1line").GetComponent<Text>();
        twoline = ev.transform.FindChild("2lines").GetComponent<Text>();
        threeline = ev.transform.FindChild("3lines").GetComponent<Text>();
        event1 = GameObject.Find("Event1").GetComponent<Button>();
        event2 = GameObject.Find("Event2").GetComponent<Button>();
        event3 = GameObject.Find("Event3").GetComponent<Button>();
        event4 = GameObject.Find("Event4").GetComponent<Button>();
        event5 = GameObject.Find("Event5").GetComponent<Button>();


        timewarp = 1f;
        sun = GameObject.Find("Sun");
        sky = GameObject.Find("sky");

        skycolor = sky.transform.GetComponent<SpriteRenderer>();
        // }
    }

    // Update is called once per frame
    void Update() {
        //Keyboard Inputs
        if (Input.GetKey(KeyCode.D)) {
            scrollingBg.transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);
            pos = -scrollingBg.transform.position.x;
            Debug.Log(pos);

            // Generate Scrolling Background
            if (Math.Abs((pos - (18.8f * n_backgrounds - 9.4f))) < 1 && n_backgrounds < 5) {
                GameObject temp = (GameObject)Instantiate(Resources.Load("ScrollingBackground"));
                temp.transform.position = new Vector2(pos + 18.8f, 0.28f);
                n_backgrounds += 1;
                temp.transform.parent = scrollingBg.transform;
            }

            // Generate Buildings
            if (pos > lastBuildingPos) {
                int check = buildingRnd.Next(0, 10);
                if (storeTimer == 0) {
                    GameObject temp = (GameObject)Instantiate(Resources.Load("Store"));
                    temp.transform.position = new Vector2(pos + 9.4f, 1.2f);
                    temp.transform.parent = scrollingBg.transform;
                    Debug.Log(temp.transform.position.x);
                    storeTimer = 3;
                }
                else if (check < 4) {
                    GameObject temp = (GameObject)Instantiate(Resources.Load("Building"));
                    temp.transform.position = new Vector2(pos + 9.4f, 1.2f);
                    temp.transform.parent = scrollingBg.transform;
                    Debug.Log(temp.transform.position.x);
                }
                else if (check > 3) {
                    GameObject temp = (GameObject)Instantiate(Resources.Load("House"));
                    temp.transform.position = new Vector2(pos + 9.4f, 1.2f);
                    temp.transform.parent = scrollingBg.transform;
                    Debug.Log(temp.transform.position.x);
                }
                storeTimer -= 1;
                lastBuildingPos = pos + 9.4f;
            }

        }
        else if (Input.GetKey(KeyCode.A) && pos > 0f) {
            scrollingBg.transform.Translate(Vector2.right * Time.deltaTime * moveSpeed);
            pos = -scrollingBg.transform.position.x;
        }

        // Moving the Sun
        if (!dayover) {
            //moving the sun up/down

            if (sun.transform.position.x <= -7.5) {
                CustomTransUp(sun, 0.1f * timewarp);
            }
            else if (sun.transform.position.x <= -7) {
                CustomTransUp(sun, 0.05f * timewarp);
            }
            else if (sun.transform.position.x <= -6.5) {
                CustomTransUp(sun, 0.03f * timewarp);
            }
            else if (sun.transform.position.x <= 0) {
                CustomTransUp(sun, 0.005f * timewarp);
            }
            else if (sun.transform.position.x <= 6.5) {
                CustomTransDown(sun, 0.005f * timewarp);
            }
            else if (sun.transform.position.x <= 7) {
                CustomTransDown(sun, 0.03f * timewarp);
            }
            else if (sun.transform.position.x <= 7.5) {
                CustomTransDown(sun, 0.05f * timewarp);
            }
            else {
                CustomTransDown(sun, 0.1f * timewarp);
            }

            if (sun.transform.position.x <= 3) {
                //Debug.Log("Before Adjust r: " + skycolor.color.r + " g: " + skycolor.color.g + " b: " + skycolor.color.b);
                skycolor.color = new Color(skycolor.color.r + Time.fixedDeltaTime * .005f * timewarp * -0.07059f, skycolor.color.g + Time.fixedDeltaTime * .005f * timewarp * .596078f, skycolor.color.b + Time.fixedDeltaTime * .005f * timewarp * 0.321569f);
                //Debug.Log("After Adjust r: " + skycolor.color.r + " g: " + skycolor.color.g + " b: " + skycolor.color.b);
            }
            else {
                //Color skycolor = sky.transform.GetComponent<SpriteRenderer>().color;
                skycolor.color = new Color(skycolor.color.r + Time.fixedDeltaTime * .01f * timewarp * 0.160784f, skycolor.color.g + Time.fixedDeltaTime * .01f * timewarp * -.86275f, skycolor.color.b + Time.fixedDeltaTime * .01f * timewarp * -0.61961f);
            }
        }
        //moving the sun left/right
        if (sun.transform.position.x <= 8.43) {
            sun.transform.Translate(Vector2.right * Time.fixedDeltaTime * 0.03f * timewarp);

        }
        else {
            if (!dayover) {
                dayover = true;
                if (fastforward) {
                    SceneManager.LoadScene("CombatScene");
                }
                else
                    EndDay(0);
            }
        }
        // }
    }

    //these are for moving the sun up and down
    void CustomTransUp(GameObject obj, float speed) {
        obj.transform.Translate(Vector2.up * Time.fixedDeltaTime * speed);
    }
    void CustomTransDown(GameObject obj, float speed) {
        obj.transform.Translate(Vector2.down * Time.fixedDeltaTime * speed);
    }

    void onAwake() {
        DontDestroyOnLoad(this);
    }

    protected void EndDay(int type) {
        Debug.Log(fastforward);
        // update persistent data
        //DataScript.p1hp = (int.Parse(member1HP.text
        DataScript.supply = (int.Parse(Supply.text));

        // 0 for timeout, 1 for no ap
        ClearEvents();
        if (type == 0) {
            exploreEvent.alpha = 1;
            oneline.text = "There is no more time in the day";

            event1.GetComponentInChildren<Text>().text = "1. Brave the night";
            event1.onClick.AddListener(() => SceneManager.LoadScene("CombatScene"));
            SceneManager.LoadScene("CombatScene");
        }
        else if (type == 1) {
            exploreEvent.alpha = 1;
            oneline.text = "You ran out of Action Points";
            timewarp = 250f;
            Debug.Log(timewarp);
            fastforward = true;
            SceneManager.LoadScene("CombatScene");
            //event1.GetComponentInChildren<Text>().text = "1. End the day";
            //event1.onClick.AddListener(() => SceneManager.LoadScene("CombatScene"));
        }
        else /*if(!fastforward)*/{
            exploreEvent.alpha = 1;
            oneline.text = "Prepare for the night";

            event2.GetComponentInChildren<Text>().text = "2. Continue exploring";
            event2.onClick.AddListener(() => Confirmed());

            event1.GetComponentInChildren<Text>().text = "1. Brave the night";
            event1.onClick.AddListener(() => timewarp = 250f);
            event1.onClick.AddListener(() => fastforward = true);
            event1.onClick.AddListener(() => Confirmed());
            event1.onClick.AddListener(() => SceneManager.LoadScene("CombatScene"));
        }
    }

    protected void LowerAP(int amount) {
        for (int i = 0; i < amount; i++) {
            if (actionCount > 0) {
                String tempName = String.Format("AP{0}", actionCount.ToString());
                actionPoints[tempName].color = Color.grey;
                actionCount--;
                //if (actionCount == 0) {
                //    building.GetComponent<Collider2D>().enabled = false;
                //    house.GetComponent<Collider2D>().enabled = false;
                //    store.GetComponent<Collider2D>().enabled = false;
                //}
            }
        }
    }

    protected void ClearEvents() {

        oneline.text = "";
        twoline.text = "";
        threeline.text = "";

        event1.GetComponentInChildren<Text>().text = "";
        event1.GetComponent<Button>().onClick.RemoveAllListeners();
        event1.interactable = true;
        //event1.

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
