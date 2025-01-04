using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class ItemChest : ChestBehavior
{ 
    //TODO : AJOUTER UN TIMER
    protected DropsUpgrade dropsUpgrade;
    [SerializeField] protected Sprite openSprite;
    CapsuleCollider2D capsuleCollider;
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        dropsUpgrade = gameObject.GetComponent<DropsUpgrade>();
    } 

    protected override void OnOpen()
    {
        isOpen = true;
        spriteRenderer.sprite = openSprite;
        GameObject loot = dropsUpgrade.getDroppedObject();
        loot.transform.position = gameObject.transform.position;
        Instantiate(loot);
        loot.GetComponent<Rigidbody2D>().AddForce(new Vector2(5, 5));
    }
}
