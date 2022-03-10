using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllItemData : MonoBehaviour
{
    IItemDataService ItemDataService;

    public static AllItemData Instance { get; private set; }

    public List<Item> AllItems;

    [HideInInspector]
    public List<Item> Goblins;

    [HideInInspector]
    public List<Item> ChallengeItems;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            ItemDataService = new ItemDataService();
        }
        else
        {
            this.enabled = false;
        }
    }

    private void Start()
    {
        LoadData();
    }

    void LoadData()
    {
        Goblins = ItemDataService.AllUnlockedItemsFromPool(eItemPools.Goblin);
        Debug.Log($"Loaded {Goblins.Count} Goblins from {AllItems.Count} Items");

        ChallengeItems = ItemDataService.AllChallengeRoomItems();
        Debug.Log($"Loaded {ChallengeItems.Count} Challenge Items from {AllItems.Count} Items");
    }

}