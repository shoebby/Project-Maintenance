using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor;
using UnityEditor.Events;

public class KeyItemsScript : MonoBehaviour
{
    public GameObject selectOverlay;
    public Text selectOverlayName;
    public Text selectOverlayDescription;

    public InventoryItemsScript itemViewer;

    public GameObject slotPrefab;
    private Button slotTempButton;

    public KeyItem[] keyItems;
    public KeyItem activeKeyItem;

    public GameObject[] slots;

    void Start()
    {
        activeKeyItem = null;

        selectOverlay.SetActive(false);

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
    }

    public void SelectKeyItem(int itemID)
    {
        activeKeyItem = keyItems[itemID];

        selectOverlay.SetActive(true);

        //set up overlay texts and rendertexture
        selectOverlayName.text = activeKeyItem.itemName;
        selectOverlayDescription.text = activeKeyItem.description;
        itemViewer.activeKeyItem = itemViewer.keyItems[itemID];
    }
}
