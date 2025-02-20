using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelInteractable : MonoBehaviour
{
    public enum InteractableType
    {
        KEY,
        UNLOCKABLE_DOOR
    }

    public InteractableType interactableType;
    public string requiredItem;
    public string itemName;

    private void Start()
    {
        switch(interactableType)
        {
            case InteractableType.KEY:
                if(Game.Instance.HasItemInInventory(itemName))
                {
                    AcquireItem();
                }
                
                break;

            case InteractableType.UNLOCKABLE_DOOR:
                if(Game.Instance.HasUnlockedDoor(itemName)) {
                    UnlockDoor();
                }

                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Hamster"))
        {
            switch(interactableType)
            {
                case InteractableType.KEY:
                    AcquireItem();
                    break;
            }
        }
    }

    private void AcquireItem()
    {
        Game.Instance.AddItemToInventory(itemName);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Hamster"))
        {
            switch(interactableType)
            {
                case InteractableType.UNLOCKABLE_DOOR:
                    if(Game.Instance.HasItemInInventory(requiredItem))
                    {
                        UnlockDoor();
                    }

                    break;
            }
        }
    }

    private void UnlockDoor()
    {
        Game.Instance.AddDoorToUnlocked(itemName);
        gameObject.layer = LayerMask.NameToLayer("Unlocked Doors");
        gameObject.GetComponent<Tilemap>().color = new Color(1f, 1f, 1f, 0.5f);
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
}
