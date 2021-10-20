using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "Shop Data", menuName = "Shop/Shop Data")]
public class ShopData : ScriptableObject
{
    [Serializable]
    public class ShopDataItem
    {
        public string _id;
        public int _qty = 0;
        public ShopDataItem(string id, int qty) => (_id, _qty) = (id, qty);
    }

    public List<ShopDataItem> items = new List<ShopDataItem>();

    public void AddItem(string id)
    {
        foreach (ShopDataItem s in items)
            if (s._id == id)
            {
                ++s._qty;
                return;
            }

        ShopDataItem item = new ShopDataItem(id, 1);
        items.Add(item);
    }

    public int GetItemQty(string id)
    {
        foreach (ShopDataItem s in items)
            if (s._id == id)
                return s._qty;
        
        return 0;
    }

    public List<ShopDataItem> GetAllBoughtItems() => items;
    
    public int ItemEquipped(string id)
    {
        List<ShopDataItem> shopDataItems = (from i in items where i._id == id select i).ToList();
        if (shopDataItems.Count == 0) return -1;
        --shopDataItems[0]._qty;
        if (shopDataItems[0]._qty < 0)
        {
            for (int i = 0; i < items.Count; ++i)
                if (items[i]._id == id)
                    items.RemoveAt(i);
            return shopDataItems[0]._qty;
        }
        return shopDataItems[0]._qty;
    }
}
