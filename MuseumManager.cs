using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class MuseumManager : MonoBehaviour
{
    public static MuseumManager instance;
    public bool readyToShowText = false;

    public TMP_Text nameTxt;
    public TMP_Text historyTxt;
    public GameObject containerBox;
    public GameObject Interactiontext;

    public float typingSpeed = 0.05f;

    private Dictionary<string, ArtifactInfo> artifactData = new Dictionary<string, ArtifactInfo>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.LogWarning("More than one instance of MuseumManager");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        foreach (GameObject art in GameObject.FindGameObjectsWithTag("Art"))
        {
            ArtifactInfo info = art.GetComponentInChildren<ArtifactInfo>();
            if (info != null && !artifactData.ContainsKey(art.name))
            {
                artifactData.Add(art.name, info);
            }
        }
    }

    public IEnumerator ShowText(string fullText)
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < fullText.Length; i++)
        {
            builder.Append(fullText[i]);
            historyTxt.SetText(builder);
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void ShowArtifactInfo(string artifactName)
    {
        StopAllCoroutines();

        if (artifactData.TryGetValue(artifactName, out ArtifactInfo info))
        {
            nameTxt.text = info.artifactName;
            if (readyToShowText)
            {
                StartCoroutine(ShowText(info.artifactHistory));
                readyToShowText = false;
            }
        }
        else
        {
            nameTxt.text = artifactName;
            historyTxt.text = "No historical information available for this artifact.";
        }
    }
}