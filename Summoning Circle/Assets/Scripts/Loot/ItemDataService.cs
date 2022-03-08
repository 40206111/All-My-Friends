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
        return (List<Item>)AllItems().Where(item => item.ItemPools.HasFlag(pool)).Select(item => item);
    }

    public List<Item> AllUnlockedItemsFromPool(eItemPools pool)
    {
        int playerUnlocksInt = PlayerPrefs.GetInt("Unlocks");
        eUnlockCriteria unlocks = (eUnlockCriteria)playerUnlocksInt;
        return (List<Item>)AllItemsFromPool(pool).Where(item => unlocks.HasFlag(item.UnlockCriteria)).Select(item => item);
    }

}
