using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ExploreBuildingScript: ExploreScript {
    void OnMouseDown() {
        ExploreBuilding();
    }

    void ExploreBuilding() {
        exploreEvent.alpha = 1;
        ClearEvents();

        ev.GetComponentInChildren<Text>().text = "You see an abandonned building off in the distance. It looks like it might have once been a factory";
        

        event1.GetComponentInChildren<Text>().text = "1. Send a search party over (-3 AP)";
        event1.onClick.AddListener(() => InsideEvent(3));
        if (actionCount < 3)
            event1.interactable = false;

        event2.GetComponentInChildren<Text>().text = "2. Move the caravan there (-4 AP)";
        event2.onClick.AddListener(() => OutsideEvent(4));
        if (actionCount < 4)
            event1.interactable = false;

        event3.GetComponentInChildren<Text>().text = "3. Stay here";
        event3.onClick.AddListener(() => Confirmed());
    }

    void Inside() {
        ClearEvents();

        if (actionCount <= 0) {
            EndDay(1);
            return;
        }

        event1.GetComponentInChildren<Text>().text = "1. Venture further into the building. (-1 AP)";
        event1.onClick.AddListener(() => InsideEvent(1));

        event5.GetComponentInChildren<Text>().text = "2. Leave this building.";
        event5.onClick.AddListener(() => Confirmed());
    }
    
    void InsideEvent(int APCost) {
        LowerAP(APCost);
        ClearEvents();
        int check = rnd.Next(1, 101);

        if (check < 21) {
            member1HP.text = Math.Max((int.Parse(member1HP.text) - 5), 0).ToString();
            LowerAP(1);
            event1.GetComponentInChildren<Text>().text = "jumped on by a sleeper. (-5 HP, -1 AP)";
        }
        else if (check < 71) {
            Supply.text = (int.Parse(Supply.text) + 3).ToString();
            event1.GetComponentInChildren<Text>().text = "Found some supplies. (+3 Supplies)";
        }
        else {
            LowerAP(2);
            event1.GetComponentInChildren<Text>().text = "Ran away from a zombie. (-2 AP)";
        }

        event5.GetComponentInChildren<Text>().text = "1. Ok";
        event5.onClick.AddListener(() => Inside());
    }

    void Outside() {
        ClearEvents();

        if (actionCount <= 0) {
            EndDay(1);
            return;
        }

        event1.GetComponentInChildren<Text>().text = "1. Go inside and look for supplies. (-1 AP)";
        event1.onClick.AddListener(() => InsideEvent(1));

        event5.GetComponentInChildren<Text>().text = "2. Leave this area.";
        event5.onClick.AddListener(() => Confirmed());
    }

    void OutsideEvent(int APCost)
    {
        LowerAP(APCost);
        ClearEvents();
        int check = rnd.Next(1, 101);

        if (check < 21) {
            member1HP.text = Math.Max((int.Parse(member1HP.text) - 7), 0).ToString();
            LowerAP(1);
            event1.GetComponentInChildren<Text>().text = "Ran into a trap. (-7 HP)";
        }
        else if (check < 71) {
            Supply.text = (int.Parse(Supply.text) + 1).ToString();
            event1.GetComponentInChildren<Text>().text = "Found some bread. (+1 Supplies)";
        }
        else {
            LowerAP(1);
            event1.GetComponentInChildren<Text>().text = "Sneaked past a brute. (-1 AP)";
        }

        event5.GetComponentInChildren<Text>().text = "1. Ok";
        event5.onClick.AddListener(() => Outside());
    }
}
