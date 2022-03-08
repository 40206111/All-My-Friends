using System.Collections.Generic;

public interface IItemDataService
{
    IEnumerable<Item> AllItems();

    List<Item> AllUnlockedItemsFromPool(eItemPools pool);
    IEnumerable<Item> AllItemsFromPool(eItemPools pool);


}
