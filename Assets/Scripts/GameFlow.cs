
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameFlow : MonoBehaviour
{
    public static GameFlow Instance;

    [Header("UI")]
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject endScreen;

    private bool gameStarted = false;
    private bool gameEnded = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameStarted = false;
        gameEnded = false;

        startScreen.SetActive(true);
        endScreen.SetActive(false);

        Time.timeScale = 0f;
    }

    private void Update()
    {
        // ENTER = Start
        if (Keyboard.current != null &&
            Keyboard.current.enterKey.wasPressedThisFrame)
        {
            if (!gameStarted && !gameEnded)
            {
                StartGame();
            }
        }

        // E = End
        if (Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (gameStarted && !gameEnded)
            {
                EndGame();
            }
        }

        // R = Restart
        if (Keyboard.current != null &&
            Keyboard.current.rKey.wasPressedThisFrame)
        {
            if (gameEnded)
            {
                RestartGame();
            }
        }
    }

    public void StartGame()
    {
        gameStarted = true;
        gameEnded = false;

        startScreen.SetActive(false);
        endScreen.SetActive(false);

        Time.timeScale = 1f;

        Debug.Log("GAME STARTED");
    }

    public void EndGame()
    {
        gameStarted = false;
        gameEnded = true;

        endScreen.SetActive(true);

        Time.timeScale = 0f;

        Debug.Log("GAME ENDED");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}

