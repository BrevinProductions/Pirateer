using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using UnityEngine;

namespace Pirateer.Gameplay.Tools
{
    public class HitData
    {
        public float dmg { get; } = 0;
        public List<PEffect> effects { get; } = new List<PEffect>();

        //empty hit
        public HitData()
        {
            //do nothing, this hit does nothing
            Debug.Log("Effectless HIT");
        }

        //damage hits
        public HitData(float dmg)
        {
            this.dmg = dmg;
        }

        //used for hits that only affect, but do no damage
        public HitData(List<PEffect> effects)
        {
            this.effects = effects;
        }

        //used for combination hit
        public HitData(float dmg, List<PEffect> effects)
        {
            this.dmg = dmg;
            this.effects = effects;
        }
    }
}
