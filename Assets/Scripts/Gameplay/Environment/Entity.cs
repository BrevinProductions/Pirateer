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
    public abstract class Entity
    {
        public List<Tools.PItem> Items { get; set; }

        //
        public GameObject gameObject { get; set; }

        //if the enemy hits the player, this happens
        public abstract void Hit(PlayerObserver player, HitData hit);

        //if the enemy is hit, call this function
        public abstract void SelfHit(HitData hit);

        //enemies all need to execute this on death
        public abstract void OnDeath();

        //no enemy can be instantiated without a gameobject
        public Entity(GameObject obj)
        {

        }
    }
}
