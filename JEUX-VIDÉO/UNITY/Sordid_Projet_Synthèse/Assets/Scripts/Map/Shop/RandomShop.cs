using Harmony;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class RandomShop : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<SpellScrolls> scrolls;
    GameObject itemChosen;
    Inventory inventory;
    private void Start()
    {
        itemChosen = Instantiate(scrolls.Random(), transform.position, transform.rotation, transform).gameObject;
        itemChosen.transform.localScale = new Vector3(10, 10, 10);
        inventory = InventoryPersistence.instance.GetComponentInChildren<Inventory>();
        itemChosen.GetComponent<Rigidbody2D>().simulated = false;
        SpriteRenderer sprite = itemChosen.GetComponent<SpriteRenderer>();
        sprite.sortingLayerName = "Default";
        sprite.sortingOrder = 2;

    }



    public void Bought()
    {

        inventory.AddToInventoryEvent(itemChosen);
        itemChosen.SetActive(false);
        itemChosen.transform.parent = PlayerPersistence.instance.transform.Find("SpellCollected").transform;
    }

}
