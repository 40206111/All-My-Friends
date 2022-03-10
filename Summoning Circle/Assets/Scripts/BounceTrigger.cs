using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceTrigger : MonoBehaviour
{
    BounceHub Hub;

    private void Start()
    {
        Hub = GetComponentInParent<BounceHub>();

        if(Hub != null)
        {
            Collider2D coll = Hub.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(coll, GetComponent<Collider2D>());
        }
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(Hub == null)
        {
            return;
        }
        Hub.OnTriggerEnter?.Invoke(collision);
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (Hub == null)
        {
            return;
        }
        Hub.OnTriggerExit?.Invoke(collision);
    }
}
