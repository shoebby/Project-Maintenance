using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectOverlayScript : MonoBehaviour
{
    [Header("Scripts")]
    public InventoryItemsScript itemViewer;
    public PauseMenuController pauseMenu;
    public KeyItemsScript keyItemData;
    public LoadoutScript loadoutScript;
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
    public SelfClockScript selfClock;
    [Space(5)]

    [Header("Other")]
    private string stockName = "Selection Name";
    private string stockDescription = "This is a description of the selected item. A description can be no longer than this amount.";

    public enum UseableItems
    {
        pillBox, vialPouch, firstAidKit, camera,pipeWrench, screwdriver, thumbDrive, unmarkedCD, masterKey, crowbar, pliers
    }

    public enum EquippableItems
    {
        rubberGloves, gasMask, ballerinaFlats, wellingtonBoots
    }

    void Start()
    {
        player = FindObjectOfType<PlayerScript>();

        popUpWindow.SetActive(false);
    }

    public void Use()
    {
        if (!keyItemData.activeKeyItem.useable)
            return;

        UseKeyItem(keyItemData.activeKeyItem.itemID);
    }

    public void Equip()
    {
        if (!keyItemData.activeKeyItem.equippable)
            return;

        EquipKeyItem(keyItemData.activeKeyItem.itemID);
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
        UseableItems usedItem = (UseableItems)itemID;
        Debug.Log("Used " + usedItem);

        switch (usedItem)
        {
            case UseableItems.pillBox:
                if (!player.canUseSelf) //working as intended
                {
                    PopUp("The tin is empty.");
                    return;
                }
                selfClock.ResetClock();
                PopUp("12 more hours left.");

                break;
            case UseableItems.vialPouch:
                if (player.canUse == PlayerScript.Useables.vialPouch)
                {
                    PopUp("There's no suitable phone nearby.");
                    return;
                }
                //interact with the nearby savepoint phone, bringing up the save screen where number of held vials is also displayed

                break;
            case UseableItems.firstAidKit:
                if (!player.canUseAid)
                {
                    PopUp("There's nothing to patch up.");
                    return;
                }
                UseFirstAidKit();
                //stick player in a healing animation, heal fully when animation is completed. Is interrupted by PlayerScript.DeductHealth(int damage)

                break;
            case UseableItems.camera:
                if (player.canUse != PlayerScript.Useables.camera)
                {
                    PopUp("I shouldn't waste the battery.");
                    return;
                }
                UseCamera();
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

    #region Useable Item Coroutines

    public void UseFirstAidKit()
    {
        pauseMenu.ToggleMenu(false, 1);

        player.isUsing = true;
        
        //ToDo: play anim until end, then heal up and toggle isUsing
        //anim is interruptable through PlayerScript.DeductHealth();
    }

    public void UseCamera()
    {
        pauseMenu.ToggleMenu(false, 1);

        player.isUsing = true;

        //set player position to volume's target position
        player.controller.enabled = false;
        player.transform.position = player.usePos.position;
        player.transform.rotation = player.usePos.rotation;
        player.controller.enabled = true;

        //ToDo: enter first person view, exiting first person view reenables canMove and disables isUsing
    }

    #endregion

    public void EquipKeyItem(int itemID)
    {
        EquippableItems equippedItem = (EquippableItems)itemID - 11;
        Debug.Log("Equipped " + equippedItem);

        switch (equippedItem)
        {
            case EquippableItems.rubberGloves:
                loadoutScript.activeHandsIcon.sprite = loadoutScript.hands_rubberGloves;
                player.handsSlot = PlayerScript.Equipped.rubberGloves;
                break;
            case EquippableItems.gasMask:
                loadoutScript.activeHeadIcon.sprite = loadoutScript.head_gasMask;
                player.headSlot = PlayerScript.Equipped.gasMask;
                break;
            case EquippableItems.ballerinaFlats:
                loadoutScript.activeFeetIcon.sprite = loadoutScript.feet_ballerinaFlats;
                player.feetSlot = PlayerScript.Equipped.ballerinaFlats;
                break;
            case EquippableItems.wellingtonBoots:
                loadoutScript.activeFeetIcon.sprite = loadoutScript.feet_wellingtonBoots;
                player.feetSlot = PlayerScript.Equipped.wellingtonBoots;
                break;
        }
    }

    public void PopUp(string description)
    {
        popUpWindow.SetActive(true);
        popUpDescription.text = description;
    }
}