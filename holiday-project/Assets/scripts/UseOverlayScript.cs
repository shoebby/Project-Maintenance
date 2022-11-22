using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseOverlayScript : MonoBehaviour
{
    [Header("Scripts")]
    public InventoryItemsScript itemViewer;
    public PauseMenuController pauseMenu;
    public UseableItemsScript useableItemData;
    public PlayerScript player;
    [Space(5)]

    [Header("Overlay Text")]
    public Text nameText;
    public Text descriptionText;
    [Space(5)]

    [Header("Pop-up")]
    public GameObject popUpWindow;
    public Text popUpDescription;
    [Space(5)]

    [Header("Useable Item Utils")]
    public KeyItem setUsedItem;
    [Space(5)]

    [Header("Other")]
    private string stockName = "Selection Name";
    private string stockDescription = "This is a description of the selected item. A description can be no longer than this amount.";

    public enum UseableItems
    {
        padding1, vialPouch, padding2, camera, pipeWrench, screwdriver, thumbDrive, unmarkedCD, masterKey, crowbar, pliers
    }

    void Start()
    {
        player = FindObjectOfType<PlayerScript>();

        popUpWindow.SetActive(false);
    }

    public void Use()
    {
        UseKeyItem(useableItemData.activeKeyItem.itemID);
    }

    public void Back()
    {
        nameText.text = stockName;
        descriptionText.text = stockDescription;

        itemViewer.DeactivateKeyItem();
        gameObject.SetActive(false);
    }

    public void UseKeyItem(int itemID)
    {
        UseableItems usedItem = (UseableItems)useableItemData.activeKeyItem.itemID;
        Debug.Log("Used " + usedItem);

        switch (usedItem)
        {
            case UseableItems.vialPouch:
                if (usedItem != (UseableItems)useableItemData.useableItem.itemID)
                {
                    PopUp("There's no suitable phone nearby.");
                    return;
                }
                //interact with the nearby savepoint phone, bringing up the save screen where number of held vials is also displayed

                break;
            case UseableItems.camera:
                if (player.canUse != PlayerScript.Useables.camera)
                {
                    PopUp("I shouldn't waste the battery.");
                    return;
                }
                //take out the camera and enter a first person viewpoint with a camera overlay

                break;
            case UseableItems.pipeWrench:
                if (player.canUse != PlayerScript.Useables.pipeWrench)
                {
                    PopUp("There are no pipes to wrench.");
                    return;
                }
                //tighten or loosen a pipe, depending on the context

                break;
            case UseableItems.screwdriver:
                if (player.canUse != PlayerScript.Useables.screwdriver)
                {
                    PopUp("There's nothing to screw or unscrew.");
                    return;
                }
                //tighten or loosen screws, depending on the context

                break;
            case UseableItems.thumbDrive:
                if (player.canUse != PlayerScript.Useables.thumbDrive)
                {
                    PopUp("There's no slot to plug this into.");
                    return;
                }
                //insert the thumbdrive in the nearby PC and enter a PC view

                break;
            case UseableItems.unmarkedCD:
                if (player.canUse != PlayerScript.Useables.unmarkedCD)
                {
                    PopUp("There are no optical drives nearby.");
                    return;
                }
                //insert the CD in an optical drive and enter a viewing angle where the screen is clearly visible

                break;
            case UseableItems.masterKey:
                if (player.canUse != PlayerScript.Useables.masterKey)
                {
                    PopUp("There's no lock to open.");
                    return;
                }
                //insert the key in a door, after which it unlocks and opens

                break;
            case UseableItems.crowbar:
                if (player.canUse != PlayerScript.Useables.crowbar)
                {
                    PopUp("There's nothing to pry open.");
                    return;
                }
                //pry open a target

                break;
            case UseableItems.pliers:
                if (player.canUse != PlayerScript.Useables.pliers)
                {
                    PopUp("There's nothing to pry, snip, or grasp.");
                    return;
                }
                //pull out or snip a target

                break;
        }
    }

    public void PopUp(string description)
    {
        popUpWindow.SetActive(true);
        popUpDescription.text = description;
    }
}