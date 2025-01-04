using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryDescription : MonoBehaviour
{
    public static InventoryDescription instance;
    TextMeshProUGUI text;
    Animator animator;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        animator = GetComponent<Animator>();
    }
    public void Show()
    {
        animator.SetTrigger("Show");
    }

    public void Hide()
    {
        animator.SetTrigger("Hide");
    }
    public void SetDescription(string description)
    {
        text.text = description;
    }

}
