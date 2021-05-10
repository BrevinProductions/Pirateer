using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pirateer;
using Pirateer.Gameplay.Tools;
using Pirateer.Gameplay.Environment;

public class PlayerObserver : MonoBehaviour, EntityBehavior
{
    public float HitDistance { get; set; } = 2.0f;

    public EntityHandler handler { get; set; }

    public void SetEntityHandler()
    {
        handler.GetEntity(Entity);
    }

    public void SetHandlerInactive()
    {
        handler.Active = false;
    }

    //health slider
    public Slider healthSlider;

    //Inventory Panel
    public GameObject inventoryPanel;

    //hit information
    HitData basicHit;
    public float hitStrength = 2.0f;
    HitData criticalHit;

    public Entity Entity { get; set; }

    bool deathMessage = false;


    // Start is called before the first frame update
    void Start()
    {
        handler = gameObject.GetComponent<EntityHandler>();

        basicHit = new HitData(hitStrength);
        criticalHit = new HitData(hitStrength * 2.0f);

        Entity = new PlayerCharacter(gameObject);


        //later I want to get the player's health from a save file,
        //but for now it's set to 10 at start

        Entity.Health = 10;

        healthSlider.maxValue = 10;

        //make sure user can't slide slider
        healthSlider.interactable = false;

        SetEntityHandler();
    }

    // Update is called once per frame
    void Update()
    {
        if(healthSlider.value != Entity.Health)
        {
            healthSlider.value = Entity.Health;
        }

        //toggle Inventory
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventoryPanel.SetActive(toggle(inventoryPanel.activeSelf));
        }

        //test for player death
        if((Entity.Health <= 0) && !deathMessage)
        {
            deathMessage = true;
            Entity.OnDeath();
        }
    }

    public bool Collect(Gatherable item)
    {
        Debug.Log("Gathered " + item.gameObject);
        item.gameObject.SetActive(false);
        Entity.Items.Add(item.item);

        return true;
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
