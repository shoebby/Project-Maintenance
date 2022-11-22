using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [Header("Containers")]
    public GameObject menuContainer;
    public GameObject contentContainer;
    [Space(5)]

    [Header("Tab Buttons")]
    public Text tabInventory;
    public Text tabDiary;
    public Text tabSettings;
    [Space(5)]

    [Header("Contents")]
    public GameObject contentInventory;
    public GameObject contentDiary;
    public GameObject contentSettings;
    [Space(5)]

    [Header("Other")]
    public Image menuBackground;
    public bool inMenu;
    public Color selectedColor;

    public enum Menus
    {
        inventory, diary, settings
    }

    public Menus activeMenu;

    void Start()
    {
        menuBackground = GetComponent<Image>();

        SwitchContent(0);
        inMenu = false;

        ToggleMenu(false, 1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !inMenu)
            ToggleMenu(true, 0);
        else if (Input.GetKeyDown(KeyCode.Tab) && inMenu)
            ToggleMenu(false, 1);
    }

    public void ToggleMenu(bool toggle, int timeScale)
    {
        menuContainer.SetActive(toggle);
        contentContainer.SetActive(toggle);
        menuBackground.enabled = toggle;
        inMenu = toggle;
        Time.timeScale = timeScale;
    }

    public void SwitchContent(int menuID)
    {
        activeMenu = (Menus)menuID;

        contentInventory.SetActive(false);
        contentDiary.SetActive(false);
        contentSettings.SetActive(false);

        tabInventory.color = Color.white;
        tabDiary.color = Color.white;
        tabSettings.color = Color.white;

        switch (activeMenu)
        {   
            case Menus.inventory:
                contentInventory.SetActive(true);
                tabInventory.color = selectedColor;
                break;
            case Menus.diary:
                contentDiary.SetActive(true);
                tabDiary.color = selectedColor;
                break;
            case Menus.settings:
                contentSettings.SetActive(true);
                tabSettings.color = selectedColor;
                break;
        }
    }
}
