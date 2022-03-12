using System;
using UnityEngine;

public class Pedastal : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer ItemSprite;

    Item ItemForPlayer;

    public static Action<Item> OnItemPickUp;


    private void Start()
    {
        gameObject.SetActive(false);
        WaveSpawner.OnPreBoss += SpawnItem;
    }

    public void SpawnItem()
    {
        var pool = AllItemData.Instance.GetPoolFromRewardEnum();
        if (pool == eItemPools.None)
        {
            return;
        }

        gameObject.SetActive(true);
        PutRandomItemOnPedastal(pool);
    }

    void PutRandomItemOnPedastal(eItemPools pool)
    {
        ItemForPlayer = AllItemData.Instance.GetItemFromPool(pool);
        ItemSprite.sprite = ItemForPlayer.PedastalIcon;
        ItemSprite.color = ItemForPlayer.Colour;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"{collision.collider.name}");
        PlayerHub player = collision.gameObject.GetComponent<PlayerHub>();
        if( player != null)
        {
            OnItemPickUp?.Invoke(ItemForPlayer);
            Instantiate(ItemForPlayer);
            gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        WaveSpawner.OnPreBoss -= SpawnItem;
    }
}
