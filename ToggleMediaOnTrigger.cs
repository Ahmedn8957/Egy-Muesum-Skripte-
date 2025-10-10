using UnityEngine;
using UnityEngine.Video;

public class ToggleMediaOnTrigger : MonoBehaviour
{
    [Header("Assign GameObjects with Components")]
    public GameObject videoObject;     // GameObject with VideoPlayer
    public GameObject audioObject;     // GameObject with AudioSource

    private VideoPlayer videoPlayer;
    private AudioSource audioSource;

    void Start()
    {
        // Get components from assigned GameObjects
        videoPlayer = videoObject.GetComponent<VideoPlayer>();
        audioSource = audioObject.GetComponent<AudioSource>();

        // Initial state: audio on, video paused
        videoPlayer.Pause();
        audioSource.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Resume video playback
            videoPlayer.Play();

            // Stop and disable audio
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.enabled = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Pause video instead of disabling
            videoPlayer.Pause();

            // Enable and play audio
            audioSource.enabled = true;
            audioSource.Play();
        }
    }
}