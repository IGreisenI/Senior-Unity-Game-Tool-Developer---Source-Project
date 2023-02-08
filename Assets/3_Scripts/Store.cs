using HomaGames.Internal.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : Singleton<Store>
{
    public List<StoreItem> StoreItems;
    public Action<StoreItem> OnItemSelected;

    public void SelectItem(StoreItem item)
    {
        OnItemSelected?.Invoke(item);
    }
}
