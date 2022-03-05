using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Range = 5f;
    public float Speed = 7f;

    public Vector2 Origin;
    public Vector2 Direction;

    public int Damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        Origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)Direction * Speed * Time.deltaTime;
        if (((Vector2)transform.position - Origin).sqrMagnitude > Range * Range)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EntityHub>().Health.Damage(Damage);
            Destroy(gameObject);
        }
    }
}
