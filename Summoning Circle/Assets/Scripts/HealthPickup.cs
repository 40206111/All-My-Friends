using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public Sprite Full;
    public Sprite Half;

    public int HealAmount = 0;

    private SpriteRenderer Sprite;

    private void Start()
    {
        HealAmount = Random.Range(1, 3);
        Sprite = GetComponentInChildren<SpriteRenderer>();
        if(HealAmount == 1)
        {
            Sprite.sprite = Half;
        }
        else
        {
            Sprite.sprite = Full;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D[] contacts = new ContactPoint2D[collision.contactCount];
            collision.GetContacts(contacts);
        foreach (ContactPoint2D c in contacts)
        {
            PlayerHub ph = c.collider.GetComponent<PlayerHub>();
            if(ph != null && ph.Health.CurrentHealth < ph.Health.MaxHealth)
            {
                ph.Health.Heal(HealAmount);
                Destroy(gameObject);
            }
        }
    }
}
