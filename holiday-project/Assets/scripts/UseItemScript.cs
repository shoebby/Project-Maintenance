using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItemScript : MonoBehaviour
{
    public static PlayerScript player;
    public static GameObject playerObject;

    public Transform playerPos;

    public enum Item
    {
        vialPouch, camera, pipeWrench, screwdriver, thumbDrive, unmarkedCD, masterKey, crowbar, pliers
    }

    public Item useable;

    void Start()
    {
        player = FindObjectOfType<PlayerScript>();
        playerObject = player.gameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            player.usePos = playerPos;
            
            switch (useable)
            {
                case Item.vialPouch:
                    player.canUse = PlayerScript.Useables.vialPouch;
                    break;
                case Item.camera:
                    player.canUse = PlayerScript.Useables.camera;
                    break;
                case Item.pipeWrench:
                    player.canUse = PlayerScript.Useables.pipeWrench;
                    break;
                case Item.screwdriver:
                    player.canUse = PlayerScript.Useables.screwdriver;
                    break;
                case Item.thumbDrive:
                    player.canUse = PlayerScript.Useables.thumbDrive;
                    break;
                case Item.unmarkedCD:
                    player.canUse = PlayerScript.Useables.unmarkedCD;
                    break;
                case Item.masterKey:
                    player.canUse = PlayerScript.Useables.masterKey;
                    break;
                case Item.crowbar:
                    player.canUse = PlayerScript.Useables.crowbar;
                    break;
                case Item.pliers:
                    player.canUse = PlayerScript.Useables.pliers;
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player.canUse = PlayerScript.Useables.none;
        }
    }
}
