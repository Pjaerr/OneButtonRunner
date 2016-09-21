using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    //Floats and Ints
    float avgFrameRate;
    public static int attempts = 0;
    public static float volume = 0.8f;

    //Objects
    public GameObject pauseMenuUI;
    //public Text fpsCounter;
    public AudioSource geoplexSolarRain;
    public Text attemptsCounter;
    public Slider volumeSlider;
    public GameObject mainMenuUI;
    public GameObject optionsMenuUI;
    public GameObject instructionsMenuUI;
    public GameObject levelSelectMenuUI;

    void Awake()
    {
        Application.targetFrameRate = 240; //Limits the frame rate to 240 fps to save on power usage.
    }

	void Update ()
    {
        avgFrameRate = Time.frameCount / Time.time; //Assigns the average frame rate.
 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(true);
        }

        checkGameObjNotNull();
	}

    //Called in Update(): Checks to see if any game objects that are being referenced are not null before referencing them, to avoid the NullReferenceException error.
    void checkGameObjNotNull()
    {
        if (attemptsCounter != null)
        {
            attemptsCounter.text = "Attempts: " + attempts.ToString();
        }

        //Debug: FPS Counter rendered as UI.
        /*if (fpsCounter != null)
        {
            fpsCounter.text = "FPS = " + avgFrameRate.ToString();
        }*/

        if (geoplexSolarRain != null)
        {
            geoplexSolarRain.volume = volume;
        }

        if (volumeSlider != null)
        {
            volume = volumeSlider.value;
        }
    }

    //Called across scenes when going back back to the main menu. Loads the main menu scene and resets all non-scene independent variables.
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
        attempts = 0;
    }

    //Called via play button on level select: Loads level passed into button press call.
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

    //Opens the Option menu and closes the Main menu.
    public void Options()
    {
        mainMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    //Open the Instructions menu and closes the Main menu.
    public void Instructions()
    {
        mainMenuUI.SetActive(false);
        instructionsMenuUI.SetActive(true);
    }

    //Quits the game.
    public void QuitGame()
    {
        Application.Quit();
    }

    //Dependant upon what the Vsync dropdown menu is set to, changes the Vsync Count.
    public void VsyncCount(Dropdown dropDownMenu)
    {
        if (dropDownMenu.value == 0)
        {
            QualitySettings.vSyncCount = 0;
        }
        else if (dropDownMenu.value == 1)
        {
            QualitySettings.vSyncCount = 1;
        }
    }

    //Opens the Level Select menu and closes the Main menu.
    public void LevelSelect()
    {
        mainMenuUI.SetActive(false);
        levelSelectMenuUI.SetActive(true);
    }

    //Goes back from whichever menu's allow it, closing those menus and opening the Main menu.
    public void Back()
    {
        optionsMenuUI.SetActive(false);
        instructionsMenuUI.SetActive(false);
        levelSelectMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    //Changes the level on the level select screen.
    public void ChangeLevel(bool previousOrNext)
    {
        if (previousOrNext)
        {
            //Set next level ui active and current level ui inactive.
        }
        else if (previousOrNext == false)
        {
            //Set previous level ui active and current level ui inactive.
        }
    }

    //Pauses the game and presents the Pause Menu UI.
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
