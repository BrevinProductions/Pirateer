using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pirateer;
using Pirateer.Gameplay.Tools;
using Pirateer.Gameplay.Environment;

public class PlayerObserver : MonoBehaviour
{

    //health slider
    public Slider healthSlider;

    //Inventory Panel
    public GameObject inventoryPanel;

    public PlayerCharacter player { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        player = new PlayerCharacter(gameObject);

        //later I want to get the player's health from a save file,
        //but for now it's set to 10 at start

        player.Health = 10;

        healthSlider.maxValue = 10;

        //make sure user can't slide slider
        healthSlider.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthSlider.value != player.Health)
        {
            //healthSlider.interactable = true;
            healthSlider.value = player.Health;
            //healthSlider.interactable = false;
        }

        //test hit
        if (Input.GetKeyDown(KeyCode.H))
        {
            player.SelfHit(new HitData(1.0f));
        }

        //toggle Inventory
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventoryPanel.SetActive(toggle(inventoryPanel.activeSelf));
        }

        //test for player death
        if(player.Health <= 0)
        {
            player.OnDeath();
        }
    }


    //little toggle function :) - I think this looks prettier
    bool toggle(bool val)
    {
        if (val)
            return false;
        else
        {
            return true;
        }
    }
}
