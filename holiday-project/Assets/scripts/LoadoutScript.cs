using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutScript : MonoBehaviour
{
    public PlayerScript player;
    
    public Image activeHeadIcon;
    public Image activeHandsIcon;
    public Image activeFeetIcon;

    public Sprite head_empty;
    public Sprite hands_empty;
    public Sprite feet_empty;

    public Sprite head_gasMask;
    public Sprite hands_rubberGloves;
    public Sprite feet_ballerinaFlats;
    public Sprite feet_wellingtonBoots;

    void Start()
    {
        player = FindObjectOfType<PlayerScript>();

        activeHeadIcon.sprite = head_empty;
        activeHandsIcon.sprite = hands_empty;
        activeFeetIcon.sprite = feet_empty;
    }

    public void FlushHead()
    {
        activeHeadIcon.sprite = head_empty;
        player.headSlot = PlayerScript.Equipped.none;
    }

    public void FlushHands()
    {
        activeHandsIcon.sprite = hands_empty;
        player.handsSlot = PlayerScript.Equipped.none;
    }

    public void FlushFeet()
    {
        activeFeetIcon.sprite = feet_empty;
        player.feetSlot = PlayerScript.Equipped.none;
    }
}
