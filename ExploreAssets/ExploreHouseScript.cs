using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ExploreHouseScript : ExploreScript {
    void OnMouseDown() {
        int check = rnd.Next(1, 101);
        ExploreHouse(check);
    }

    void ExploreHouse(int check) {
        exploreEvent.alpha = 1;
        ClearEvents();

        event1.GetComponentInChildren<Text>().text = "1. There is a small house nearby. Explore the inside. (-1 AP)";
        event1.onClick.AddListener(() => InsideEvent());

        event5.GetComponentInChildren<Text>().text = "3. nah";
        event5.onClick.AddListener(() => Confirmed());
    }

    void Inside() {
        ClearEvents();

        if (actionCount <= 0) {
            EndDay(1);
            return;
        }

        event1.GetComponentInChildren<Text>().text = "1. Look inside the house. (-1 AP)";
        event1.onClick.AddListener(() => InsideEvent());

        event5.GetComponentInChildren<Text>().text = "2. Leave this house.";
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
            event1.GetComponentInChildren<Text>().text = "No one's here.";
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
            member1HP.text = Math.Max((int.Parse(member1HP.text) - 4), 0).ToString();
            LowerAP(1);
            event1.GetComponentInChildren<Text>().text = "Ran into a trap. (-4 HP)";
        }
        else if (check < 71) {
            Supply.text = (int.Parse(Supply.text) + 2).ToString();
            event1.GetComponentInChildren<Text>().text = "Found some scrap wood. (+2 Supplies)";
        }
        else {
            LowerAP(1);
            event1.GetComponentInChildren<Text>().text = "Sneaked past a brute. (-1 AP)";
        }

        event5.GetComponentInChildren<Text>().text = "1. Ok";
        event5.onClick.AddListener(() => Confirmed());
    }
}
