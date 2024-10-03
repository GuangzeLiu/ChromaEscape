using UnityEngine;
using UnityEngine.UI;

public class StartGameUI : MonoBehaviour
{
    public GameObject instructionPanel;
    public GameObject playerDeadUI;
    public GameObject levelCompleteUI;
    private bool gameStarted = false;
    private IncomingBlocksSpawner spawner;

    void Start()
    {
        instructionPanel.SetActive(true);
        if (playerDeadUI != null)
        {
            playerDeadUI.SetActive(false);
        }

        spawner = FindObjectOfType<IncomingBlocksSpawner>();
    }

    void Update()
    {
        if (!gameStarted && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) ||
                             Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) ||
                             Input.GetKeyDown(KeyCode.RightArrow)))
        {
            StartGame();
        }
    }

    void StartGame()
    {
        instructionPanel.SetActive(false);

        if (spawner != null)
        {
            spawner.StartGame();
        }

        gameStarted = true;
        Debug.Log("Game has started!");
    }

    public void ResetGame()
    {
        instructionPanel.SetActive(true);
        gameStarted = false;

        if (playerDeadUI != null)
        {
            playerDeadUI.SetActive(false);
        }

        if (levelCompleteUI != null)
        {
            levelCompleteUI.SetActive(false);
        }

        Debug.Log("Game reset, showing instructions again.");
    }
}