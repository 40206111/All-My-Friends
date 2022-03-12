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

    Item GetItemFromAll()
    {

        int random = Random.Range(0, AllItems.Count);
        return AllItems[random];
    }

    public Item GetItemFromPool(eItemPools pool)
    {
        Item output = null;
        switch (pool)
        {
            case eItemPools.Goblin:
                if (Goblins.Count == 0)
                {
                    return GetItemFromAll();
                }
                int random = Random.Range(0, Goblins.Count);
                output = Goblins[random];
                Goblins.Remove(output);
                break;
            case eItemPools.Challenge:
                if (ChallengeItems.Count == 0)
                {
                    return GetItemFromAll();
                }
                random = Random.Range(0, ChallengeItems.Count);
                output = ChallengeItems[random];
                ChallengeItems.Remove(output);
                break;
            default:
                Debug.LogError("Can't get item from no pool!");
                break;
        }

        return output;
    }

    public eItemPools GetPoolFromRewardEnum()
    {
        var reward = WaveSpawner.NextReward;
        switch (reward)
        {
            case eRoundSigil.Goblin:
                return eItemPools.Goblin;
            case eRoundSigil.Skull:
                return eItemPools.Challenge;
            default:
                return eItemPools.None;
        }
    }

}
