using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pirateer.Gameplay.Tools;
using UnityEngine;

namespace Pirateer.Gameplay.Environment
{
    public class PlayerCharacter : Entity
    {
        public PlayerCharacter(GameObject obj) : base(obj)
        {
            EntityType = EntityType.player;

            Health = 10.0f;

            Items = new List<PItem>();
        }

        public override void Hit(Entity entity, HitData hit)
        {
            //send hit info to entity that was hit
            entity.SelfHit(hit);
        }

        public override void OnDeath()
        {
            //nothing happens right now
            
            //Keep this in, I think it's funny >:-(
            Debug.Log("Player died lol");
        }

        public override void SelfHit(HitData hit)
        {
            this.Health -= hit.dmg;

            //implement effects

        }
    }
}
