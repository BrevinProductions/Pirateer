using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using UnityEngine;
using Pirateer.Gameplay.Environment;
using Pirateer.Gameplay.Tools;

public class EntityHandler : MonoBehaviour
{
    //I am actually going to shoot myself
    //I think I need to simplify this to tabs and have specifics of entity behavior loaded
    //from JSON or XML or some shit

    //edit: i did it   ___
    //                 |__|
    //                 |  |  
    //                 |  |      
    //              ___|  |__ __  
    //             /  |   |  |  \
    //            /|         |  | 
    //            [|  |     \ / |   
    //            ||          / |
    //            \|            |

    public Entity Entity { get; set; }
    public bool Active { get; set; } = true;

    public void GetEntityType<T>() where T : EntityBehavior
    {
        Entity = gameObject.GetComponent<T>().Entity;

        return; //I can do what I want
    }
}
