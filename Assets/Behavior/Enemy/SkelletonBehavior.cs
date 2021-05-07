using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pirateer.Gameplay.Environment;
using Pirateer.Gameplay.Tools;

public class SkelletonBehavior : MonoBehaviour, EntityBehavior
{
    /*
     * this script is for implementing the state machine of the skelleton (I know
     * it's spelled wrong lol, I was tired, but it's fine) This is what I plan on
     * doing for each entity: they all have their state and behavior within 
     * separate objects to make the code more readable
     * 
     * So this script has exactly what the skelleton is doing at any point in time
     * 
     * There are a lot of features that havent been implemented yet - mostly 
     * randomness in generation of the skelletons
     * 
     * 
     * EACH GAMEOBJECT WITH AN ENTITY BEHAVIOR ALSO REQUIRES AND ENTITYHANDLER - IT JUST
     * DELIVERS INFO TO OTHER GAMEOBJECTS LMAO
     */

    //
    float swingTimer = 0;
    //

    CharacterController ctlr;

    public EntityHandler handler { get; set; }

    public Entity Entity { get; set; }
    HitData hitData;

    GameObject target = null;
    public float HitDistance { get; set; } = 3.0f;

    Sword sword;

    enum MoveState : byte
    {
        Idle,
        Move,
        Attack,
        IdleWalk
    }

    MoveState mState;

    // Start is called before the first frame update
    void Start()
    {
        //grab sword child
        sword = gameObject.transform.GetComponentInChildren<Sword>();

        //grab that controller
        ctlr = GetComponent<CharacterController>();

        //instantiate skelleton object
        Entity = new Skelleton(gameObject);

        //TODO: generate hitdata based on loot
        //for now:
        hitData = new HitData(1.0f);

        //reference local entity handler
        handler = gameObject.GetComponent<EntityHandler>();

        mState = MoveState.Idle;

        SetEntityHandler();
    }

    // Update is called once per frame
    void Update()
    {
        if(swingTimer < 0)
        {
            swingTimer = 0;
        }
        else
        {
            swingTimer -= Time.deltaTime;
        }

        Vector3 moveVector = new Vector3();

        //test health to see if the entity has died
        if(Entity.Health <= 0.0f)
        {
            //(get beasted)
            Entity.OnDeath(); //does a bunch of shit and then deletes this object
            SetHandlerInactive(); //kills the handler, freeing whatever has this GameObject as a target
        }

        //implement the state machine here
        //first test to see if target is active
        try
        {
            if (!target.GetComponent<EntityHandler>().Active)
            {
                target = null;
                mState = MoveState.Idle;
            }
            //do everything else (state machine)
            else
            {
                //main AI piece (for when there's a target)
                switch (mState)
                {
                    case MoveState.Idle:
                        if (target != null)
                            mState = MoveState.Attack;
                        break;

                    case MoveState.Move:
                        //move toward target
                        if (distanceToTarget() <= HitDistance)
                            mState = MoveState.Attack;
                        else
                        {
                            //move toward target

                            //angle to face the target
                            transform.LookAt(target.transform.position, Vector3.up);
                            moveVector = transform.rotation * Vector3.forward;

                            //move toward the target -- below to add gravity
                        }
                        break;

                    case MoveState.Attack:
                        if (distanceToTarget() > HitDistance)
                            mState = MoveState.Move;
                        else
                        {
                            //zero the move vector
                            moveVector = Vector3.zero;

                            //hit the damn target
                            //test the swing timer to stop swing from overlapping
                            if(swingTimer <= 0)
                            {
                                Swing(target);
                            }
                        }
                        break;

                    case MoveState.IdleWalk:
                        //something broke
                        Debug.Log("Skelleton is IdleWalking during an attack sequence, don't know how this happened");
                        break;

                    default:
                        Debug.Log("You've fucked something up in Skeleton AI (specifically when there's a target");
                        break; //I dont think there''s anything to do, there's been some sort of glitch if this happens
                }
            }
        }
        catch (NullReferenceException)
        {

        }
        //add gravity to the move vector


        //apply the move vector
        ctlr.Move(moveVector * Time.deltaTime);
    }

    void Swing(GameObject trgt)
    {
        swingTimer = 3.0f;

        Debug.Log(ToString() + " Register Swing at " + target.ToString());

        //just pass the game object to a sword child
        sword.Swing();

        //get the sword component from the child gameObjects

    }

    //returns the distance to the target
    public float distanceToTarget()
    {
        Vector3 tPos = target.transform.position;
        Vector3 sPos = gameObject.transform.position;

        return (Mathf.Sqrt(Mathf.Pow((tPos.x-sPos.x), 2) + Mathf.Pow((tPos.y - sPos.y), 2) + Mathf.Pow((tPos.z - sPos.z), 2)));
    }

    public void SetEntityHandler()
    {
        handler.GetEntity(Entity);
    }

    public void SetHandlerInactive()
    {
        handler.Active = false;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision happened");
        //check if updating the target is possible

        //check to see if the target is null
        try
        {
            target.Equals(null);
        }
        //target is able to be reset
        catch(NullReferenceException)
        {
            if (other.gameObject.tag.Equals("Entity"))
            {
                //just test to see if the collision has worked
                Debug.Log( ToString() + " Entity Collision");

                //get the entity handler
                EntityHandler hitHandler = other.gameObject.GetComponent<EntityHandler>();

                if (hitHandler.Entity.EntityType == EntityType.friendly || hitHandler.Entity.EntityType == EntityType.player)
                {
                    //finally set the target
                    target = hitHandler.Entity.gameObject;
                    //let the debugger know what's up
                    Debug.Log("(SKELETON!) target entity has been updated to " + hitHandler.Entity.EntityType);
                }
            }
        }
    }

}
