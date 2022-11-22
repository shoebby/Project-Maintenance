using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItemScript : MonoBehaviour
{
    public static PlayerScript player;
    public static GameObject playerObject;
    public UseableItemsScript useItemPopup;

    public enum Item
    {
        vialPouch, camera, pipeWrench, screwdriver, thumbDrive, unmarkedCD, masterKey, crowbar, pliers
    }

    public Item useable;
    public KeyItem useableKeyItem;

    void Start()
    {
        player = FindObjectOfType<PlayerScript>();
        playerObject = player.gameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {            
            if (Input.GetKeyDown(KeyCode.E))
            {
                useItemPopup.Enable(this);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                useItemPopup.Disable();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player.canUse = PlayerScript.Useables.none;
            useItemPopup.Disable();
        }
    }
}
