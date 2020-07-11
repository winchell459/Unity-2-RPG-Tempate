using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryCheck", menuName = "CustomObject/Dialogue/InventoryResponse")]
public class InventoryResponse : DialogueResponse
{
    public List<int> InventoryRequired;
    public bool RemoveItemsAfterCheck = false;

    public DialogueBranch FailedCheckBranch;

    public bool CheckInventory()
    {
        bool inventoryChecked = true;
        List<inventorySlotProxy> inventory = FindObjectOfType<PlayerManager>().invetory;
        for (int i = 0; i < inventory.Count; i += 1)
        {
            if(InventoryRequired[i] > inventory[i].itemAmount)
            {
                inventoryChecked = false;
            }
        }
        if(RemoveItemsAfterCheck && inventoryChecked)
        {
            for (int i = 0; i < inventory.Count; i += 1)
            {
                inventory[i].itemAmount -= InventoryRequired[i];
            }
            FindObjectOfType<PlayerManager>().invetory = inventory;
        }
        return inventoryChecked;
    }
}
