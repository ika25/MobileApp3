using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menus : MonoBehaviour
{
    // Veriables
    public PlayerController player;
    public GameObject PauseMenu;
    public Animator AchievmentAnimator;
    public Animator GameOverAnimator;
    public Text CurrentCoins, AllCoins, CurrentHearts, AllHearts, CurrentScore, HighestScore, CurrentMultiplier, HighestMultiplier;// ahcievment data
    private static Menus _instance;


    // access this class directly by the name of the class
    public static Menus instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Menus>();
            }
            return _instance;
        }
    }


    // Function that will pause the game
    public void PauseGame()
    {
        if (!player.IsDead)
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
            LevelManager.instance.isPaused = true;
        }
    }

    // 1function to resume the game
    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        LevelManager.instance.isPaused = false;

    }

    // Function when clicked on main menu to reload the current scene
    public void RestartGame()
    {
        Time.timeScale = 1;
        Application.LoadLevel(Application.loadedLevel);

    }

    // Function to quit
    public void Exit()
    {
        Application.Quit();
    }

    // Function to controll sound On/Off
    public void AudioOnOff()
    {
        AudioIcon icon = GameObject.Find("AudioButton").GetComponent<AudioIcon>();
        icon.SpriteSwitch();
        
    }

    public void ShowAchievments()
    {
        GameOverAnimator.SetTrigger("Hide");
        // Get Achievment data
        CurrentCoins.text = LevelManager.instance.coinCount.ToString();
        CurrentHearts.text = LevelManager.instance.heartCount.ToString();
        CurrentMultiplier.text = LevelManager.instance.multiplier.ToString();
        CurrentScore.text = ((int)LevelManager.instance.PlayerScore).ToString();
        //get all stored data
        AllCoins.text = PlayerPrefs.GetInt("CollectedCoins").ToString();
        AllHearts.text = PlayerPrefs.GetInt("CollectedHearts").ToString();
        HighestMultiplier.text = PlayerPrefs.GetInt("HighestMultiplier").ToString();
        HighestScore.text = PlayerPrefs.GetInt("HighestScore").ToString();

        AchievmentAnimator.SetTrigger("Show");
    }
}
