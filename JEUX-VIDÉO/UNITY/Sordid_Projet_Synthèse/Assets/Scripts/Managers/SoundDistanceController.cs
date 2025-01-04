using UnityEngine;

public class SoundDistanceController : MonoBehaviour
{
    public Transform player;
    public float maxDistance = 10f;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            float volume = 0.5f - Mathf.Clamp01(distance / maxDistance); 
            audioSource.volume = volume; 
        }
    }
}
