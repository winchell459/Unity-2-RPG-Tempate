using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject itemButton;
    public GameObject inventoryContent;
    public PlayerManager pm;

    //day 2 changed -----------------------------------------------------------
    public void Setup()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        int count = 0;
        foreach (InventorySlot square in inventoryContent.GetComponentsInChildren<InventorySlot>())
        {
            square.InitalizeSelf(count, pm);
            count++;
        }
    }

    public void UpdateAllSquares()
    {
        //day 2 added -----------------------------------------------------------
        Setup();

        foreach (InventorySlot square in inventoryContent.GetComponentsInChildren<InventorySlot>())
        {
            square.UpdateSelf(pm);
        }
    }
}
