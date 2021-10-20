using UnityEngine;

public class ShopItem : ScriptableObject
{
    public string itemId;
    public int itemPrice;
    public string itemName;
    public string itemDescription;
    public bool itemOnlyBoughtOnce;
    public Sprite itemImage;
    public GameObject itemPrefab;
}