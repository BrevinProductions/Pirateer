using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pirateer.Gameplay.Environment;

namespace Pirateer.Gameplay.Tools
{
    public interface EntityBehavior
    {
        public float HitDistance { get; set; }
        public EntityHandler handler {get; set;}

        Entity Entity { get; set; }

        void SetEntityHandler();

        void SetHandlerInactive();
    }
}
