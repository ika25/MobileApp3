using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour
{
    // Veriables
    public PlayerController player;
    public GameObject PauseMenu;

    // Function that will pause the game
    public void PauseGame()
    {
        if (!player.IsDead)
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    // 1function to resume the game
    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        
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
}
