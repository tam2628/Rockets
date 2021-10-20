using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Shop : MonoBehaviour
{
    public static event Action<int> ShopItemBought;

    [SerializeField]
    private ShopData shopData;

    [Serializable]
    private class ShopCategories
    {
        public string category;
        public List<ShopItem> items;
    }

    [SerializeField]
    private List<ShopCategories> cats = new List<ShopCategories>();

    public static Shop instance;
    private List<ShopItem> allShopItems; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }            
        else if (instance != this)
            Destroy(gameObject);
    }


    private void Start()
    {
        allShopItems = new List<ShopItem>();
        List<string> cats = GetCategories();
        foreach (string s in cats)
            allShopItems.AddRange(GetCategoryItems(s));
    }

    public List<string> GetCategories()
    {
        List<string> categories = new List<string>(cats.Count);
        foreach (ShopCategories sc in cats)
            categories.Add(sc.category);
        return categories;
    }

    public ShopItem GetItemDetails(string id) => (from i in allShopItems where i.itemId == id select i).ToList()[0];
    
    public void ItemBought(string id)
    {
        shopData.AddItem(id);
        ShopItem item = GetItemDetails(id);
        ShopItemBought?.Invoke(item.itemPrice);
    }

    public int ItemEquipped(string id) => shopData.ItemEquipped(id);

    public List<ShopItem> GetCategoryItems(string category)
    {
        foreach (ShopCategories sc in cats)
            if (sc.category == category)
                return sc.items;

        return null;
    }
    
    public  List<ShopData.ShopDataItem> GetAllBoughtItems() => shopData.GetAllBoughtItems();
}
