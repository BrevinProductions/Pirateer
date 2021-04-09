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

        //a skeleton cannot be instantiated without a game object
        public Skelleton(GameObject obj) : base(obj)
        {
            Debug.Log("Skelleton Exists");
            gameObject = obj;

            EntityType = EntityType.enemy;
            //randomly generate the skeleton's items

            //yeah
            Items = new List<PItem>();

            Health = 10.0f;
        }

        //on hit of another entity, I'll be changing the player observer to an entity
        public override void Hit(Entity entity, HitData data)
        {
            entity.SelfHit(data);
        }

        //when hit
        public override void SelfHit(HitData hit)
        {
            Health -= hit.dmg;

            //you need to figure this out, effects will not work right now, 
            //I think entities need a list of active effects that will just
            //add the effects from the hit to the active effects that will 
            //with themselves from update
            foreach(PEffect effect in hit.effects)
            {
                effect.Apply();
            }
        }

        //execute on death
        public override void OnDeath()
        {
            //implement on death implementation
            throw new NotImplementedException();
        }
    }
}
