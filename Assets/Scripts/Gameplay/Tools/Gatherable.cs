using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pirateer.Gameplay.Environment;
using Pirateer.Gameplay.Tools;

public class Gatherable : MonoBehaviour
{
    public PItem item { get; set; }
    bool addForce = false;

    // Start is called before the first frame update
    void Start()
    {
        item = new PItem(this);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = Quaternion.Euler(0.0f, transform.eulerAngles.y + (Time.deltaTime * 5.0f), 0.0f);

        if (addForce)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 2.5f);
        }
    }


    //detect trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Entity"))
        {
            if(other.gameObject.GetComponent<EntityHandler>().Entity.EntityType == EntityType.player)
            {
                other.gameObject.GetComponent<PlayerObserver>().Collect(this);
            }
        }

        else if (other.gameObject.tag.Equals("Ground"))
        {
            addForce = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Ground"))
        {
            addForce = false;
        }
    }
}
