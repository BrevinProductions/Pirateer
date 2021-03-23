using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using UnityEngine;
using Pirateer.Gameplay.Tools;

namespace Pirateer.Gameplay.Environment
{
    public class Skelleton : Entity
    {
        //important stats
        public float Health { get; } = 10f;
        public List<PItem> items { get; } = new List<PItem>();

        //a skeleton cannot be instantiated without a game object
        public Skelleton(GameObject obj) : base(obj)
        {
            gameObject = obj;

            EntityType = EntityType.enemy;
            //randomly generate the skeleton's items


        }

        //on hit of another entity, I'll be changing the player observer to an entity
        public override void Hit(PlayerObserver player, HitData data)
        {
            player.SelfHit(data);
        }

        //when hit
        public override void SelfHit(HitData hit)
        {
            
        }

        //execute on death
        public override void OnDeath()
        {
            //implement on death implementation
            throw new NotImplementedException();
        }
    }
}
