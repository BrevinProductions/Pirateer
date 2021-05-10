using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pirateer.Gameplay.Tools;


public class Sword : MonoBehaviour
{
    public string Name { get; set; }
    public ItemType Type { get; set; }
    public bool equipped { get; set; }

    public Animation swordSwing;

    public Sword()
    {
        Type = ItemType.Weapon;
    }

    public void Swing()
    {
        swordSwing.Play("SwordSwing2");
    }

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Entity"))
        {
            Debug.Log(this + " swordswing at " + other.gameObject);
            other.gameObject.GetComponent<EntityHandler>().Entity.SelfHit(new HitData(2.0f));
        }
    }
}