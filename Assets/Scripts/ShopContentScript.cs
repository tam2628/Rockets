using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ShopContentScript : MonoBehaviour
{
    [SerializeField]
    private GameObject itemCardPrefab;
    private Shop shop;
    private Vector3 one;
    [SerializeField]
    private float offset;
    private Transform moneyBalanceUI;
    [SerializeField]
    private GamedataScriptableObject gameData;
    public static ShopContentScript instance = null;
    private RectTransform moneyBalanceUIRectTransform;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private IEnumerator WiggleMoneyBalanceUI()
    {
        float posX = moneyBalanceUIRectTransform.anchoredPosition.x;
        var seq = LeanTween.sequence();
        for(int i = 0; i < 3; ++i)
        {
            seq.append(LeanTween.moveX(moneyBalanceUIRectTransform, posX + 10, 0.05f));
            seq.append(LeanTween.moveX(moneyBalanceUIRectTransform, posX + -10, 0.05f));
            seq.append(LeanTween.moveX(moneyBalanceUIRectTransform, posX, 0.05f));
        }
        yield return null;
    }

    public void PlayerIsPoor() => StartCoroutine(WiggleMoneyBalanceUI());
    
    private void Start()
    {
        moneyBalanceUI = transform.GetChild(0);
        TextMeshProUGUI moneyBalanceUITM_text = moneyBalanceUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        moneyBalanceUITM_text.text = gameData.playerTotalCoins.ToString();

        one = new Vector3(1, 1, 1);
        shop = Shop.instance;
        List<string> categories = shop.GetCategories();
                   
        foreach (string cat in categories)
        {
            List<ShopItem> catItems = shop.GetCategoryItems(cat);
            foreach (ShopItem item in catItems)
            {
                GameObject itemCard = Instantiate(itemCardPrefab);
                itemCard.transform.GetChild(0).GetComponent<Image>().sprite =
                    item.itemImage;
                itemCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.itemName;
                itemCard.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = item.itemDescription;
                itemCard.transform.GetChild(3).GetComponent<ShopItemBuyButtonScript>().SetId(item.itemId);
                itemCard.transform
                    .GetChild(3)
                    .GetChild(0)
                    .GetChild(1)
                    .GetChild(0)
                    .GetComponent<TextMeshProUGUI>().text = item.itemPrice.ToString();
                itemCard.transform.parent = transform;
            }
        }


        moneyBalanceUIRectTransform = moneyBalanceUI.GetComponent<RectTransform>();
        float moneyBalanceUIHeight = moneyBalanceUIRectTransform.rect.height;

        // if somethingg goes wrong make the yPos negative
        float yPos = (offset + moneyBalanceUIHeight);
        for (int i = 1; i < transform.childCount; ++i)
        {
            Transform g = transform.GetChild(i);
            RectTransform rt = g.GetComponent<RectTransform>();
            rt.localScale = one;
            yPos += offset + (rt.rect.height * (i - 1));
            rt.anchoredPosition = new Vector3(0, -yPos, 0);
        }
        
        Shop.ShopItemBought += (int _) => moneyBalanceUITM_text.text = gameData.playerTotalCoins.ToString();
    }
}