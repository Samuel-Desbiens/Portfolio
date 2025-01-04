
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootChest : ChestBehavior
{
    protected DropsBoots dropsUpgrade;
    [SerializeField] protected Sprite openSprite;
    CapsuleCollider2D capsuleCollider;
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        dropsUpgrade = gameObject.GetComponent<DropsBoots>();
    }

    protected override void OnOpen()
    {
        isOpen = true;
        spriteRenderer.sprite = openSprite;
        Boots loot = dropsUpgrade.getDroppedObject();
        loot.transform.position = gameObject.transform.position;
        Instantiate(loot);
        loot.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50, -50));
    }
}
