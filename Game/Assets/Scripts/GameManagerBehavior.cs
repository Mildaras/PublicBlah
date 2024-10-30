using UnityEngine;
using UnityEngine.UI;

public class GameManagerBehavior : MonoBehaviour
{
    public Text goldLabel;
    public Text waveLabel;
    public Text healthLabel;
    public GameObject[] nextWaveLabels;
    public GameObject[] healthIndicator;
    public Animator tutorialAnimator;

    private CommandInvoker commandInvoker;
    private ICommand pauseGameCommand;
    private ICommand displayTutorialCommand;

    private int gold;
    private int wave = 0;
    private int health;
    public bool gameOver = false;

    public int Gold
    {
        get => gold;
        set
        {
            gold = value;
            goldLabel.text = "GOLD: " + gold;
        }
    }

    public int Wave
    {
        get => wave;
        set
        {
            wave = value;
            if (!gameOver)
            {
                foreach (GameObject nextWaveLabel in nextWaveLabels)
                {
                    nextWaveLabel.GetComponent<Animator>().SetTrigger("nextWave");
                }
            }
            waveLabel.text = "WAVE: " + (wave + 1);
        }
    }

    public int Health
    {
        get => health;
        set
        {
            if (value < health)
            {
                Camera.main.GetComponent<CameraShake>().Shake();
            }
            health = value;
            healthLabel.text = "HEALTH: " + health;

            if (health <= 0 && !gameOver)
            {
                gameOver = true;
                GameObject gameOverText = GameObject.FindGameObjectWithTag("GameOver");
                gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
            }

            for (int i = 0; i < healthIndicator.Length; i++)
            {
                healthIndicator[i].SetActive(i < health);
            }
        }
    }

    void Start()
    {
        commandInvoker = new CommandInvoker();
        pauseGameCommand = new PauseGameCommand();
        displayTutorialCommand = new DisplayTutorialCommand(tutorialAnimator);

        Gold = 1000;
        Health = 5;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            commandInvoker.ExecuteCommand(displayTutorialCommand);
        }

        if (Input.GetKeyDown(KeyCode.P)) 
        {
            commandInvoker.ExecuteCommand(pauseGameCommand);
        }

        if (Input.GetKeyDown(KeyCode.Z)) 
        {
            commandInvoker.UndoLastCommand();
        }
    }
}
