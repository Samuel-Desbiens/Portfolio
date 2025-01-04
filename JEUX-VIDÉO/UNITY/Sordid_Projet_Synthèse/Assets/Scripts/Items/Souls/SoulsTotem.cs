using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SoulsTotem : MonoBehaviour
{
    [SerializeField] bool firstUnlocked =false;
    Animator animator;
    Souls soul;
    private PlayerInputActions inputs;
    private InputAction interact;
    Inventory inventory;

    bool col;

    private void Awake()
    {
        inputs = new PlayerInputActions();
        interact = inputs.Player.Interact;
    }

    private void OnEnable()
    {
        interact.Enable();
    }

    private void OnDisable()
    {
        interact.Disable();
    }
    void Start()
    {
        inventory = InventoryPersistence.instance.GetComponentInChildren<Inventory>();
        animator = GetComponent<Animator>();
        soul = GetComponentInChildren<Souls>();
        if (!firstUnlocked)
        {
            soul.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(interact.triggered && col && soul.gameObject.activeSelf)
        {
            inventory.SetSoul(soul.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            col = true;
            animator.SetTrigger("Start");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            col = false;
            animator.SetTrigger("End");
        }
    }
}
