using UnityEngine;
/*
 * 
 * OLD
public class LavaDamage : MonoBehaviour
{
    [SerializeField] private int damagePerSecond;
    private bool isPlayerInside = false;
    private bool canDealDamage = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            InvokeRepeating(nameof(DealDamage), 1f, 1f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            CancelInvoke(nameof(DealDamage));
        }
    }

    private void DealDamage()
    {
        if (isPlayerInside && canDealDamage)
        {
            GameManager.Instance.PlayerTakeDamage(damagePerSecond);
            canDealDamage = false; 
            Invoke(nameof(ResetDamageFlag), 2f); 
        }
    }

    private void ResetDamageFlag()
    {
        canDealDamage = true;
    }
}

*/