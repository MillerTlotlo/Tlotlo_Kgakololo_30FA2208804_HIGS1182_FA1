
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Score Settings")]
    [SerializeField] private int score = 0;

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;

    private bool gameIsOver = false;

    private void Awake()
    {
        Time.timeScale = 1f;

        if (gameOverPanel == null)
        {
            CreateGameOverUI();
        }

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void AddScore(int points)
    {
        if (gameIsOver)
            return;

        score += points;
        UpdateScore();
    }

    public void GameOver()
    {
        if (gameIsOver)
            return;

        gameIsOver = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (gameOverText != null)
            gameOverText.text = "GAME OVER";

        UpdateScore();

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void UpdateScore()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();

        Debug.Log("Exit pressed. In the Unity Editor, stop Play mode manually.");
    }

    private void CreateGameOverUI()
    {
        GameObject canvasObject = new GameObject(
            "Generated Game UI",
            typeof(Canvas),
            typeof(CanvasScaler),
            typeof(GraphicRaycaster)
        );

        Canvas canvas = canvasObject.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        CanvasScaler scaler = canvasObject.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        gameOverPanel = new GameObject(
            "GameOverPanel",
            typeof(RectTransform),
            typeof(Image)
        );

        gameOverPanel.transform.SetParent(canvasObject.transform, false);

        RectTransform panelRect = gameOverPanel.GetComponent<RectTransform>();
        panelRect.anchorMin = Vector2.zero;
        panelRect.anchorMax = Vector2.one;
        panelRect.offsetMin = Vector2.zero;
        panelRect.offsetMax = Vector2.zero;

        gameOverPanel.GetComponent<Image>().color =
            new Color(0.03f, 0.04f, 0.09f, 0.95f);

        gameOverText = CreateText(
            "GameOverText", "GAME OVER", 64,
            new Vector2(0, 180), new Vector2(700, 100)
        );

        scoreText = CreateText(
            "ScoreText", "Score: 0", 38,
            new Vector2(0, 70), new Vector2(500, 70)
        );

        restartButton = CreateButton(
            "RestartButton", "RESTART",
            new Vector2(0, -50)
        );

        restartButton.onClick.AddListener(RestartGame);

        exitButton = CreateButton(
            "ExitButton", "EXIT",
            new Vector2(0, -150)
        );

        exitButton.onClick.AddListener(ExitGame);

        gameOverPanel.SetActive(false);
    }

    private TMP_Text CreateText(
        string objectName,
        string label,
        int fontSize,
        Vector2 position,
        Vector2 size)
    {
        GameObject textObject = new GameObject(
            objectName,
            typeof(RectTransform),
            typeof(TextMeshProUGUI)
        );

        textObject.transform.SetParent(gameOverPanel.transform, false);

        RectTransform rect = textObject.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = position;
        rect.sizeDelta = size;

        TMP_Text text = textObject.GetComponent<TMP_Text>();
        text.text = label;
        text.fontSize = fontSize;
        text.alignment = TextAlignmentOptions.Center;
        text.color = Color.white;
        text.raycastTarget = false;

        return text;
    }

    private Button CreateButton(
        string objectName,
        string label,
        Vector2 position)
    {
        GameObject buttonObject = new GameObject(
            objectName,
            typeof(RectTransform),
            typeof(Image),
            typeof(Button)
        );

        buttonObject.transform.SetParent(gameOverPanel.transform, false);

        RectTransform rect = buttonObject.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = position;
        rect.sizeDelta = new Vector2(300, 70);

        buttonObject.GetComponent<Image>().color =
            new Color(0.12f, 0.45f, 0.8f, 1f);

        Button button = buttonObject.GetComponent<Button>();

        ColorBlock colors = button.colors;
        colors.normalColor = new Color(0.12f, 0.45f, 0.8f);
        colors.highlightedColor = new Color(0.25f, 0.6f, 1f);
        colors.pressedColor = new Color(0.06f, 0.25f, 0.5f);
        button.colors = colors;

        TMP_Text buttonText = CreateText(
            label + "Label", label, 28,
            Vector2.zero, new Vector2(280, 60)
        );

        buttonText.transform.SetParent(buttonObject.transform, false);

        RectTransform textRect = buttonText.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        return button;
    }
}

