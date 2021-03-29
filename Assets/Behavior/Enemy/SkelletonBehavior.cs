using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pirateer.Gameplay.Environment;
using Pirateer.Gameplay.Tools;

public class SkelletonBehavior : MonoBehaviour, EntityBehavior
{
    /*
     * this script is for implementing the state machine of the skelleton (I know
     * it's spelled wrong lol, I was tired, but its fine) This is what I plan on
     * doing for each entity: they all have their state and behavior within 
     * separate objects to make the code more readable
     * 
     * So this script has exactly what the skelleton is doing at any point in time
     * 
     * There are a lot of features that havent been implemented yet - mostly 
     * randomness in generation of the skelletons
     * 
     */

    public Entity Entity { get; set; }
    HitData hitData;

    // Start is called before the first frame update
    void Start()
    {
        //instantiate skelleton object
        Entity = new Skelleton(gameObject);

        //TODO: generate hitdata based on loot
        //for now:
        hitData = new HitData(1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
