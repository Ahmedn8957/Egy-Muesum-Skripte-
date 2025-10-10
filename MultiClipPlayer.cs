using UnityEngine;
using System.Collections;

public class MultiClipPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] clips;
    public float minDelay = 2f;
    public float maxDelay = 5f;

    void Start()
    {
        StartCoroutine(PlayRandomClip());
    }

    IEnumerator PlayRandomClip()
    {
        while (true)
        {
            float waitTime = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(waitTime);

            int index = Random.Range(0, clips.Length);
            audioSource.clip = clips[index];
            audioSource.Play();
        }
    }
}