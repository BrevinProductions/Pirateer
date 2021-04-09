using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pirateer.Gameplay.Tools;
using Pirateer.Gameplay.Environment;

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

    void Start()
    {

    }

    public void GetEntity(Entity entity)
    {
        Entity = entity;
        Entity.DebugEntity();

        return; //I can do what I want
    }

}
