using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor;
using UnityEditor.Events;

public class UseableItemsScript : MonoBehaviour
{
    public GameObject container;
    public Image popupBG;
    
    public GameObject selectOverlay;
    public Text selectOverlayName;
    public Text selectOverlayDescription;

    public InventoryItemsScript itemViewer;

    public GameObject slotPrefab;
    private Button slotTempButton;

    public KeyItem[] keyItems;
    public KeyItem activeKeyItem;

    public GameObject[] slots;

    private UseItemScript useVolume;
    public KeyItem useableItem;

    void Start()
    {
        slots = new GameObject[keyItems.Length];

        for (int i = 0; i < keyItems.Length; i++)
        {
            //instantiate slot
            slots[i] = (Instantiate(slotPrefab, Vector3.zero, transform.rotation, transform));

            //randomized rotation between -4 and 4 degrees
            slots[i].GetComponent<RectTransform>().Rotate(0f,0f,Random.Range(-4f,4f));

            //set slot icon according to key item's icon
            GameObject slotTempImage = slots[i].transform.GetChild(0).gameObject;
            slotTempImage.GetComponent<Image>().sprite = keyItems[i].icon;

            //set up the button function (HUGE PAIN IN THE ASS)
            slotTempButton = slots[i].GetComponent<Button>();
            UnityAction<int> action = new UnityAction<int>(SelectKeyItem);
            UnityEventTools.AddIntPersistentListener(slotTempButton.onClick, action, i);
        }

        Disable();
    }

    public void SelectKeyItem(int itemID)
    {
        activeKeyItem = keyItems[itemID];

        selectOverlay.SetActive(true);

        //set up overlay texts and rendertexture
        selectOverlay.GetComponent<UseOverlayScript>().setUsedItem = activeKeyItem;
        selectOverlayName.text = activeKeyItem.itemName;
        selectOverlayDescription.text = activeKeyItem.description;
        itemViewer.activeKeyItem = itemViewer.keyItems[activeKeyItem.itemID];
    }

    public void Disable()
    {
        container.SetActive(false);
        popupBG.color = new Color(0f,0f,0f,0f);
        activeKeyItem = null;
        selectOverlay.SetActive(false);

        useableItem = null;
    }

    public void Enable(UseItemScript useVolume)
    {
        container.SetActive(true);
        popupBG.color = new Color(0f, 0f, 0f, .75f);

        useableItem = useVolume.useableKeyItem;
    }
}
