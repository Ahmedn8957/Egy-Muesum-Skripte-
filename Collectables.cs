using UnityEngine;

public class Collectables : MonoBehaviour
{
    public float speed;
    public AudioClip collectSound;
    public float Volume;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,speed, 0);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Play sound at the collectible's position
            AudioSource.PlayClipAtPoint(collectSound, transform.position, Volume);
            CollectibleCounter.instance.AddCollectible();
           //FindObjectOfType<CollectibleManager>().AddCollectible();
            // Destroy the collectible
            Destroy(gameObject);
        }
    }
}
