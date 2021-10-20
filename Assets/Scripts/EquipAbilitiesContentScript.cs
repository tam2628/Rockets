using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipAbilitiesContentScript : MonoBehaviour
{
    [SerializeField]
    private float topOffset = 10f;
    [SerializeField]
    private GameObject pickAbilityButtonPrefab;

    private Shop shop;
    private List<ShopData.ShopDataItem> items;
    private Vector3 one;

    private void Start()
    {
        one = new Vector3(1, 1, 1);
        shop = Shop.instance;
        items = shop.GetAllBoughtItems();
        for(int i = 0; i < items.Count; ++i)
        {
            ShopData.ShopDataItem boughtShopItem = items[i];
            ShopItem shopItem = shop.GetItemDetails(boughtShopItem._id);
            GameObject pickAbilityButton = Instantiate(pickAbilityButtonPrefab, transform);
            pickAbilityButton
                .GetComponent<AbilityEquipButtonScript>()
                .SetData(shopItem.itemName, shopItem.itemId, boughtShopItem._qty, shopItem.itemImage)
                .Display();

            float equipAbilityButtonHeight = pickAbilityButton.GetComponent<RectTransform>().rect.height;
            float yPos = equipAbilityButtonHeight * i + topOffset * (i + 1);
            RectTransform rt = pickAbilityButton.GetComponent<RectTransform>();
            rt.localScale = one;
            rt.anchoredPosition = new Vector2(0, -yPos);
        }
    }
}
