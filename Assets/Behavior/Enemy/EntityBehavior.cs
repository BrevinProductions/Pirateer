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
        float HitDistance { get; set; }
        EntityHandler handler {get; set;}

        Entity Entity { get; set; }

        void SetEntityHandler();

        void SetHandlerInactive();
    }
}
