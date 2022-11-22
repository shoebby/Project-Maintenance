using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemsScript : MonoBehaviour
{
    public GameObject[] keyItems;
    public GameObject activeKeyItem;
    public Camera renderCam;
    public float rotSpeed;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        
        activeKeyItem = null;

        for (int i = 0; i < keyItems.Length; i++)
            keyItems[i].SetActive(false);

        renderCam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if (activeKeyItem != null)
        {
            activeKeyItem.SetActive(true);

            Vector3 position = activeKeyItem.GetComponent<Renderer>().bounds.center;
            activeKeyItem.transform.Rotate(0f, rotSpeed * Time.unscaledDeltaTime, 0f,Space.World);
        }
    }

    //currently unused
    public void ActivateKeyItem(int itemID)
    {
        keyItems[itemID].SetActive(true);
        activeKeyItem = keyItems[itemID];
    }

    //only used in one place, does it need to be done like this?
    public void DeactivateKeyItem()
    {
        activeKeyItem.SetActive(false);
        activeKeyItem = null;
    }
}
