using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectibleCounter : MonoBehaviour
{
    public static CollectibleCounter instance;

    public TMP_Text counterText;
    public int finalCollected;
    private int collectedCount = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateCounterUI();
    }

    public void AddCollectible()
    {
        collectedCount++;
        UpdateCounterUI();

        if (collectedCount >= finalCollected)
        {
            SceneManager.LoadScene(2); // Loads the scene with build index 2
        }
    }

    private void UpdateCounterUI()
    {
        counterText.text = $"Collected: {collectedCount}";
    }
}