

using UnityEngine;
using TMPro;

public class ScrapCollector : MonoBehaviour
{
    [Header("Scrap Collection")]
    [SerializeField] private int scrapScore = 10;
    [SerializeField] private int totalScrap = 0;

    [Header("UI")]
    [SerializeField] private TMP_Text scoreText;

    public int TotalScrap => totalScrap;

    private void Start()
    {
        UpdateScoreUI();
    }

    public void CollectScrap(int amount)
    {
        totalScrap += amount;

        UpdateScoreUI();

        Debug.Log("Scrap collected! Total score: " + totalScrap);
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Scrap Score: " + totalScrap;
        }
    }
}
