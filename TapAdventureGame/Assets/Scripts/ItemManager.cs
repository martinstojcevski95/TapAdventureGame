using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{

    [SerializeField] Text ItemInfo;
    [SerializeField] SilverPerClick _CurrencyPerClick;
    [SerializeField] Item item;
    [SerializeField]
    Image ItemImage;
    string ItemName;
    float cost;
    int count;
    float baseCost;
    float clickPower;
    [SerializeField] Color affordableColor;
    Slider tempSlider;
    private void Start()
    {
        baseCost = cost;
        var slider = this.gameObject.transform.GetChild(3).gameObject.GetComponentInChildren<Slider>();
        tempSlider = slider;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateInfo();
    }

    void UpdateInfo()
    {
        gameObject.name = ItemName + item._ItemName;
        ItemInfo.text = ItemName + item._ItemName + " \nCost: " + item._ItemCost + " \nClickPower :" + item._ClickPower;//" \nSilver " + tickValue + " /s";
        ItemImage.GetComponent<Image>().sprite = item.ItemImage;


        if (tempSlider != null)
        {
            tempSlider.value = _CurrencyPerClick.iron / item._ItemCost * 100;
            if (tempSlider.value >= 100)
            {
                this.gameObject.transform.GetChild(3).GetComponent<Button>().interactable = true;
                tempSlider.GetComponentInChildren<Image>().color = affordableColor;
            }
            else
                this.gameObject.transform.GetChild(3).GetComponent<Button>().interactable = false;
        }
   
    }

    public void PurchaseItem()
    {
        if (_CurrencyPerClick.iron >= item._ItemCost)
        {
            _CurrencyPerClick.iron -= item._ItemCost;
            count += 1;
            _CurrencyPerClick.ironPerClick += item._ClickPower;
            cost = Mathf.Round(baseCost * Mathf.Pow(1.15f, count));
            item._Damage += 0.5f;
            tempSlider.value = 0;
        }
       

    }
}
