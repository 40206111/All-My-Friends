using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackTest : MonoBehaviour
{
    public PlayerHub phub;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 knockback = phub.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            phub.Mover.Knockback(knockback.normalized * 5f);
        }
    }
}
