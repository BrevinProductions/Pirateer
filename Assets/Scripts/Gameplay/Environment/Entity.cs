using System;
using Unity;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pirateer.Gameplay.Tools;

namespace Pirateer.Gameplay.Environment
{
    public enum EntityType : byte
    {
        neutral,
        friendly,
        enemy,
        player
    }

    public abstract class Entity
    {
        public EntityType EntityType;

        public List<PItem> Items { get; set; }

        public float Health { get; set; }

        //link the game object to this on startup
        public GameObject gameObject { get; set; }

        //if the enemy hits the player, this happens
        public abstract void Hit(Entity entity, HitData hit);

        //if the enemy is hit, call this function
        public abstract void SelfHit(HitData hit);

        //enemies all need to execute this on death
        public abstract void OnDeath();

        //
        public virtual void DebugEntity()
        {
            Debug.Log("this entity is refferenced by an entity handler");
        }

        //no enemy can be instantiated without a gameobject
        public Entity(GameObject obj)
        {
            gameObject = obj;
        }
    }
}
