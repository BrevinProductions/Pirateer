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

    public EntityHandler handler { get; set; }

    public Entity Entity { get; set; }
    HitData hitData;

    GameObject target = null;
    public float HitDistance { get; set; } = 1.0f;

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
        //instantiate skelleton object
        Entity = new Skelleton(gameObject);

        //TODO: generate hitdata based on loot
        //for now:
        hitData = new HitData(1.0f);

        //reference local entity handler
        handler = gameObject.GetComponent<EntityHandler>();

        mState = MoveState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        //test health to see if the entity has died
        if(Entity.Health <= 0.0f)
        {
            //(get beasted)
            Entity.OnDeath(); //does a bunch of shit and then deletes this object
            SetHandlerInactive(); //kills the handler, freeing whatever has this GameObject as a target
        }

        //implement the state machine here
        //first test to see if target is active
        if (target != null)
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
                            //move toward the damn target (I've got to implement a character controller)
                        }
                        break;

                    case MoveState.Attack:
                        if (distanceToTarget() > HitDistance)
                            mState = MoveState.Move;
                        else
                        {
                            //hit the damn target
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
    }

    bool Swing()
    {
        //return true if the swing hit, just test to see if the sword made contact with the target
        return false;
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
        handler.GetEntityType<SkelletonBehavior>();
    }

    public void SetHandlerInactive()
    {
        handler.Active = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        //check if updating the target is possible
        if(target == null && collision.gameObject.tag.Equals("Entity"))
        {
            //get the entity handler
            EntityHandler hitHandler = collision.gameObject.GetComponent<EntityHandler>();

            if(hitHandler.Entity.EntityType == EntityType.friendly || hitHandler.Entity.EntityType == EntityType.player)
            {
                //finally set the target
                target = hitHandler.Entity.gameObject;
                //let th debugger know what's up
                Debug.Log("(SKELETON!) Entity has been updated to " + hitHandler.Entity.EntityType);
            }
        }
    }
}
