using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected GameObject item;
    [SerializeField] Inventory inventory;
    [SerializeField] protected bool isLocked;
    static InventoryDescription inventoryDescription;
    string itemDescription => item.GetComponent<IItemInfo>().GetItemInfo();
    protected Image slotImage;
    protected Color showItemColor = new Color(1, 1, 1, 1);
    protected Color hideItemColor = new Color(1, 1, 1, 0);
    protected Color selectedItemColor = new Color(1, 1, 1, 0.7f);

    protected virtual void Start()
    {
        if (inventoryDescription == null)
        {
            inventoryDescription = InventoryDescription.instance;
        }
        slotImage = transform.GetChild(0).GetComponent<Image>();


    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isLocked && item != null)
        {
            inventoryDescription.SetDescription(itemDescription);
            inventoryDescription.Show();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isLocked && item != null)
        {
            inventoryDescription.Hide();
        }
    }


    public GameObject GetItem()
    {
        return item;
    }


    public void UpdateItemSprite()
    {
        slotImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
    }

    public virtual void AddItem(GameObject newItem)
    {
        if (!isLocked)
        {
            item = newItem;
            if (item != null)
            {
                UpdateItemSprite();
                slotImage.color = showItemColor;
            }
            else
            {
                slotImage.color = hideItemColor;
            }
        }

    }

    public bool IsEmpty()
    {
        if (item != null || isLocked)
            return false;
        else
            return true;
    }

    public bool IsLocked()
    {
        return isLocked;
    }
    //utlis par OnClick() Unity
    public void SuspendItem()
    {
        if (!isLocked)
        {
            if (inventory.GetSuspendedItem() == null)
            {
                inventory.SuspendSlotItem(this);

            }
            else
            {
                inventory.CheckToSwitchItems(this);
            }
        }

    }

    public void SetSelectedmaterial()
    {
        if (!isLocked)
        {
            slotImage.color = selectedItemColor;
        }
    }

    public void SetUnselectedMaterial()
    {
        if (!isLocked && item != null)
        {
            slotImage.color = showItemColor;
        }
    }

    public virtual void RemoveItem()
    {
        item = null;
        slotImage.color = hideItemColor;
        slotImage.sprite = null;
    }

    public bool IsSlotCompatible(Slot slotToSwitch)
    {
        if (!CompareTag("InvSlot"))
        {
            if ((slotToSwitch.CompareTag(tag) || slotToSwitch.CompareTag("InvSlot")) && (slotToSwitch.IsEmpty() || slotToSwitch.GetItem().CompareTag(item.tag)))
            {
                return true;
            }

        }
        else if (slotToSwitch.CompareTag("InvSlot"))
        {
            return true;
        }
        else if (slotToSwitch.CompareTag(item.tag + "Slot"))
        {
            return true;
        }
        return false;
    }
}

