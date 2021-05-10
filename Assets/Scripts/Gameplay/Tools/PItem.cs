using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Pirateer.Gameplay.Tools
{
    public enum ItemType : byte
    {
        Weapon,
        Armour,
        Food,
        Potion,
        //add more as seen fit
    }

    public class PItem
    {
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public bool equipped { get; set; }
        public GameObject gameObject { get; set; }

        Gatherable gatherable { get; }

        public PItem(Gatherable gatherable)
        {
            this.gatherable = gatherable;
            this.gameObject = gatherable.gameObject;
        }
    }
}
