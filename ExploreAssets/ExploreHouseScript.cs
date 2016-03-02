﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ExploreHouseScript : ExploreScript {
    void OnMouseDown() {
        int check = rnd.Next(0, 10);
        ExploreHouse(check);
    }

    void ExploreHouse(int check) {
        exploreEvent.alpha = 1;
        ClearEvents();

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
        int check = rnd.Next(0, 10);

        if (check < 2) {
            member1HP.text = Math.Max((int.Parse(member1HP.text) - 5), 0).ToString();
            LowerAP(1);
            oneline.text = "jumped on by a sleeper. (-5 HP, -1 AP)";
        }
        else if (check < 7) {
            Supply.text = (int.Parse(Supply.text) + 3).ToString();
            oneline.text = "Found some supplies. (+3 Supplies)";
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
        int check = rnd.Next(0, 10);

        if (check < 2) {
            member1HP.text = Math.Max((int.Parse(member1HP.text) - 4), 0).ToString();
            LowerAP(1);
            event1.GetComponentInChildren<Text>().text = "Ran into a trap. (-4 HP)";
        }
        else if (check < 7) {
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
