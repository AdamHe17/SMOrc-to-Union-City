using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class StoreScript : ExploreScript {
    void OnMouseDown() {
        ExploreStore();
    }

    void ExploreStore() {
        exploreEvent.alpha = 1;
        ClearEvents();

        event1.GetComponentInChildren<Text>().text = "1. There is a store in town. Check it out.";
        event1.onClick.AddListener(() => Store());

        event2.GetComponentInChildren<Text>().text = "2. Can't really afford anything anyways";
        event2.onClick.AddListener(() => Confirmed());
    }

    void Store() {
        ClearEvents();

        event1.GetComponentInChildren<Text>().text = "1. Sword (-10 supp, +5 atk)";
        event1.onClick.AddListener(() => StorePurchase(1));

        event2.GetComponentInChildren<Text>().text = "2. Steel Sword (-30 supp, +20 atk)";
        event2.onClick.AddListener(() => StorePurchase(2));

        event3.GetComponentInChildren<Text>().text = "3. Generic Energy Bar (-4 supp, +3 AP)";
        event3.onClick.AddListener(() => StorePurchase(3));

        event4.GetComponentInChildren<Text>().text = "4. Poptart (-20 supp, +1 AP permanently)";
        event4.onClick.AddListener(() => StorePurchase(4));

        event5.GetComponentInChildren<Text>().text = "5. no money (-10 hope)";
        event5.onClick.AddListener(() => Confirmed());
    }

    void StorePurchase(int item) {
        ClearEvents();

        int sup = int.Parse(Supply.text);

        switch(item) {
            case 1:
                if (sup < 10) {
                    NotEnough();
                }
                else {
                    Supply.text = (int.Parse(Supply.text) - 10).ToString();
                    Confirmed();
                }
                break;
            case 2:
                if (sup < 30) {
                    NotEnough();
                }
                else {
                    Supply.text = (int.Parse(Supply.text) - 30).ToString();
                    Confirmed();
                }
                break;
            case 3:
                if (sup < 4) {
                    NotEnough();
                }
                else {
                    Supply.text = (int.Parse(Supply.text) - 4).ToString();
                    Confirmed();
                }
                break;
            case 4:
                if (sup < 20) {
                    NotEnough();
                }
                else {
                    Supply.text = (int.Parse(Supply.text) - 20).ToString();
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
