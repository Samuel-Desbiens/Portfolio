using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpell : ChestBehavior
{
    protected DropsSpell dropsSpell;
    [SerializeField] protected Sprite openSprite;
    CapsuleCollider2D capsuleCollider;
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        dropsSpell = gameObject.GetComponent<DropsSpell>();
    }

    protected override void OnOpen()
    {
        isOpen = true;
        spriteRenderer.sprite = openSprite;
        SpellScrolls loot = dropsSpell.getDroppedObject();
        loot.transform.position = gameObject.transform.position;
        Instantiate(loot);
        loot.GetComponent<Rigidbody2D>().AddForce(new Vector2(5, 5));
    }
}
