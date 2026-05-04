using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;

public class GameManager : MonoBehaviour
{
    private int currentEnergy;
    [SerializeField] private int energyThreshold = 30;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject enemySpawn;
    private bool bossSpawned = false;
    [SerializeField] private Image energyBar;
    [SerializeField] GameObject gameUi;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private CinemachineCamera cam;
    [SerializeField] private GameObject red;
    void Start()
    {
        currentEnergy = 0;
        UpdateEnergyBar();
        boss.SetActive(false);
        MainMenu();
        audioManager.StopAudioGame();
        cam.Lens.OrthographicSize = 5f;
        red.SetActive(false);
    }

    public void AddEnergy()
    {
        if (bossSpawned)
        {
            return;
        }
        currentEnergy += 1;
        UpdateEnergyBar();
        if (currentEnergy == energyThreshold) 
        { 
            CallBoss();
        }
    }

    private void CallBoss() 
    {
        bossSpawned = true;
        boss.SetActive(true);
        enemySpawn.SetActive(false);
        gameUi.SetActive(false);
        audioManager.PlayBossAudio();
        cam.Lens.OrthographicSize = 10f;
        red.SetActive(true);
    }

    private void UpdateEnergyBar() 
    {
        if (energyBar != null) 
        {
            float fillAmount = Mathf.Clamp01((float)currentEnergy / (float)energyThreshold);
            energyBar.fillAmount = fillAmount;
        }
    }

    public void MainMenu() 
    {
        mainMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void GameOver() 
    {
        gameOverMenu.SetActive(true);
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        Time.timeScale = 0f;
        audioManager.StopAudioGame();
    }

    public void PauseGame() 
    {
        pauseMenu.SetActive(true);
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        winMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void StartGame() 
    {
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        winMenu.SetActive(false);
        Time.timeScale = 1f;
        audioManager.PlayDefaultAudio();
    }

    public void ResumeGame() 
    {
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        winMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void WinGame() 
    {
        winMenu.SetActive(true);
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        Time.timeScale = 0f;
    }
}