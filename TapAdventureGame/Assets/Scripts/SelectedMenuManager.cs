using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedMenuManager : MonoBehaviour {

    [SerializeField] Image[] MenuItems;
    [SerializeField] GameObject[] ActiveMenu;
    [SerializeField] Color unselectedColor;
    [SerializeField] Color selectedColor;
    GameObject tempSelected;
    GameObject lastActive;


    public bool CurrentActiveMenu()
    {
        foreach (var menu in ActiveMenu)
        {
            if (menu != null)
            {
                if (menu.activeInHierarchy && menu.activeSelf)
                {
                    tempSelected = menu;
                }
                else
                    lastActive = tempSelected;
            }
        }
        // TO CHANGE THIS if possible
              
        Debug.Log("active menu is " +tempSelected.name);
        return tempSelected;
    }

    public void ClickedMenu(Image menu)
    {
        foreach (var item in MenuItems)
        {
            unselectedColor = item.color;
            unselectedColor.a = 0.5f;
            item.color = unselectedColor;
        }

        selectedColor = menu.color;
        selectedColor.a = 1;
        menu.color = selectedColor;
        CurrentActiveMenu();
    }
}
