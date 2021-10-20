using UnityEngine;

public class ShopItemBuyButtonScript : MonoBehaviour
{
    private string itemId;
    private Shop shop;
    private GameManager gameManager;
    private ShopContentScript scs;

    private void Start()
    {
        gameManager = GameManager.instance;
        shop = Shop.instance;
        scs = ShopContentScript.instance;
    }

    public void SetId(string id) => itemId = id;

    public void Buy()
    {
        ShopItem item = shop.GetItemDetails(itemId);
        if (gameManager.PlayerHasEnoughMoney(item.itemPrice))
            shop.ItemBought(itemId);
        else
            scs.PlayerIsPoor();  
    }
}
