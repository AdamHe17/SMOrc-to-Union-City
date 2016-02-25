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

        event1.GetComponentInChildren<Text>().text = "1. There is a building far away. Go inside and look for supplies. (-1 AP)";
        event1.onClick.AddListener(() => InsideEvent());

        event2.GetComponentInChildren<Text>().text = "2. Check the surrounding first. Have to be careful. (-1 AP)";
        event2.onClick.AddListener(() => OutsideEvent());

        event5.GetComponentInChildren<Text>().text = "3. nah";
        event5.onClick.AddListener(() => Confirmed());
    }

    void Inside() {
        ClearEvents();

        if (actionCount <= 0) {
            EndDay(1);
            return;
        }

        event1.GetComponentInChildren<Text>().text = "1. Venture further into the building. (-1 AP)";
        event1.onClick.AddListener(() => InsideEvent());

        event5.GetComponentInChildren<Text>().text = "2. Leave this building.";
        event5.onClick.AddListener(() => Confirmed());
    }
    
    void InsideEvent() {
        LowerAP(1);
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
        event1.onClick.AddListener(() => InsideEvent());

        event5.GetComponentInChildren<Text>().text = "2. Leave this area.";
        event5.onClick.AddListener(() => Confirmed());
    }

    void OutsideEvent() {
        LowerAP(1);
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
