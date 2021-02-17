using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pirateer;

public class PlayerObserver : MonoBehaviour
{
    //player health
    public float Health { get; set; }

    //health slider
    public Slider healthSlider;

    //Inventory Panel
    public GameObject inventoryPanel;

    // Start is called before the first frame update
    void Start()
    {
        //later I want to get the player's health from a save file,
        //but for now it's set to 10 at start

        Health = 10;

        healthSlider.maxValue = 10;

        //make sure user can't slide slider
        healthSlider.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthSlider.value != Health)
        {
            healthSlider.interactable = true;
            healthSlider.value = Health;
            healthSlider.interactable = false;
        }


        //test hit
        if (Input.GetKeyDown(KeyCode.H))
        {
            Hit(1.0f);
        }

        //toggle Inventory
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventoryPanel.SetActive(toggle(inventoryPanel.activeSelf));
        }
    }

    //hit w/ effects
    public void Hit(float dmg, List<PEffect> effects)
    {
        Health -= dmg;
    }

    //hit w/o effects
    public void Hit(float dmg)
    {
        Health -= dmg;
    }

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
