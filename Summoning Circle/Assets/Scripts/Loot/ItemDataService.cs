using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataService : IItemDataService
{

    public IEnumerable<Item> AllItems()
    {
        return AllItemData.Instance.AllItems; ;
    }
    public IEnumerable<Item> AllItemsFromPool(eItemPools pool)
    {
        return AllItems().Where(item => item.ItemPools.HasFlag(pool)).Select(item => item);
    }

    public List<Item> AllUnlockedItemsFromPool(eItemPools pool)
    {
        int playerUnlocksInt = PlayerPrefs.GetInt("Unlocks");
        eUnlockCriteria unlocks = (eUnlockCriteria)playerUnlocksInt;
        var items = AllItemsFromPool(pool).Where(item => unlocks.HasFlag(item.UnlockCriteria)).Select(item => item);
        return items.ToList();
    }

    public List<Item> AllChallengeRoomItems()
    {
        int playerUnlocksInt = PlayerPrefs.GetInt("Unlocks");
        eUnlockCriteria unlocks = (eUnlockCriteria)playerUnlocksInt;
        var allItems = AllItemsFromPool(eItemPools.Challenge);
        var lockedChallenges = allItems.Where(item => !unlocks.HasFlag(item.UnlockCriteria)).Select(item => item);
        if (lockedChallenges.Count() == 0)
        {
            return allItems.ToList();
        }
        return lockedChallenges.ToList();
    }

}
