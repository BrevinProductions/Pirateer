using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public interface PItem
    {
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public bool equipped { get; set; }
    }
}
