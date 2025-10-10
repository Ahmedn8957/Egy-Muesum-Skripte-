using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class StartMenu : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource highlightAudio;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (highlightAudio != null)
        {
            StartCoroutine(PlayHalfClip());
        }
    }

    private IEnumerator PlayHalfClip()
    {
        highlightAudio.Play();
        yield return new WaitForSeconds(highlightAudio.clip.length / 2f);
        highlightAudio.Stop();
    }
}