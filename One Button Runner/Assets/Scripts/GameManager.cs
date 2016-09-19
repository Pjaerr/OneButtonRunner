using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject pauseMenuUI;
    public Text fpsCounter;
    float avgFrameRate;
    public AudioSource geoplexSolarRain;
    public Text attemptsCounter;
    public static int attempts = 0;
    public Slider volumeSlider;
    public static float volume = 0.8f;
    public GameObject mainMenuUI;
    public GameObject optionsMenuUI;
    public GameObject instructionsMenuUI;

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(true);
        }

        if(attemptsCounter != null)
        {
            attemptsCounter.text = "Attempts: " + attempts.ToString();
        }

        if (geoplexSolarRain != null)
        {
            geoplexSolarRain.volume = volume;
        }

        if (volumeSlider != null)
        {
            volume = volumeSlider.value;
        }

        //Debug Information
        avgFrameRate = Time.frameCount / Time.time;
        if (fpsCounter != null)
        {
            fpsCounter.text = "FPS = " + avgFrameRate.ToString();
        }
        

	}

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void LoadLevel(int levelNumber)
    {
        switch (levelNumber)
        {
            case 1:
                {
                    SceneManager.LoadScene("Level_1");
                    break;
                }
                
        }
    }
    public void Options()
    {
        mainMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }
    public void Instructions()
    {
        mainMenuUI.SetActive(false);
        instructionsMenuUI.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }


    public void VsyncCount(Dropdown dropDownMenu)
    {
        if (dropDownMenu.value == 0)
        {
            QualitySettings.vSyncCount = 0;
            Debug.Log("Vsync Off");
        }
        else if (dropDownMenu.value == 1)
        {
            QualitySettings.vSyncCount = 1;
            Debug.Log("Vsync On");
        }
    }
    public void Back()
    {
        optionsMenuUI.SetActive(false);
        instructionsMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void PauseGame(bool trueFalse)
    {
        if (trueFalse)
        {
            Time.timeScale = 0;
            pauseMenuUI.SetActive(true);
            geoplexSolarRain.Pause();
        }
        if (trueFalse == false)
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1;
            geoplexSolarRain.UnPause();
        }
    }
}
