using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityEquipButtonScript : MonoBehaviour
{
    private string abilityName, abilityId;
    private int abilityQty;
    private Sprite abilityImage;

    public AbilityEquipButtonScript SetData(string abilityName, string abilityId, int abilityQty, Sprite abilityImage)
    {
        this.abilityName = abilityName;
        this.abilityQty = abilityQty;
        this.abilityImage = abilityImage;
        this.abilityId = abilityId;

        return this;
    }

    public void Display()
    {
        transform.GetChild(0).GetComponent<Image>().sprite = abilityImage;
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = abilityName;
        transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = abilityQty.ToString();
    }

    public void EquipAbilty()
    {
        abilityQty = Shop.instance.ItemEquipped(abilityId);
        Debug.Log("Ability Qty: " + abilityQty);
        if (abilityQty < 0) return;
        Display();
        GameManager.instance.PlayerEquippedAbility(abilityId);  
    }
}
