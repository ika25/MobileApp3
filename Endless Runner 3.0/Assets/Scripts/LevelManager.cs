﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    private int _framePaused = 0;
    private int _frameStart = 0;
    private CategoryButton activeButton;

    public GameObject player;
    public Text score;
    public float PlayerScore;
    public bool isPaused;
    public int coinCount = 0;
    public Text coin;
    public Image imgCoinProgress;
    public Text txtMultiplier;
    public int multiplier = 1;
    public float fillAmount = 10;
    public int heartCount = 0;
    public Text txtHeart;
    public Text txtContinueHeart;
    // Distance Veriables
    public int distance = 0;
    public int distanceFactor = 200;
    public Text txtDistance;
    public Animator distanceAnimator;
    public float AnimSpeed = 1f;
    //Data Storage veriables
    public GameObject GameOverUI;
    public ScrollRect achvScrollbar;
    public Animator AchievmentAnimator;

    public GameObject PlayerCanvas;

    public static LevelManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<LevelManager>();
            }
            return _instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        activeButton = GameObject.Find("CurrentPLayerButton").GetComponent<CategoryButton>();
        activeButton.Switch();
        PlayerScore = 0;
        isPaused = false;
}

    // Update is called once per frame
    void Update()
    {
        ScoreCalculation();// calculating score
        CoinCount();// calculating coin
        HeartCount();
    }

    private void HeartCount()
    {
        txtHeart.text = heartCount.ToString();
    }

    private void CoinCount()
    {
        coin.text = coinCount.ToString();
        txtMultiplier.text = multiplier.ToString();
        //Multiplier Progress 
        if (imgCoinProgress.fillAmount == 1)
        {
            imgCoinProgress.fillAmount = 0;
            multiplier++;
        }
        else
        {
            //fill amount determined by the current object count over the amount we want the player to collect in order to fill the meter
            imgCoinProgress.fillAmount = (coinCount - (fillAmount * (multiplier - 1))) / fillAmount;
        }
    }

    private void ScoreCalculation()
    {
        if (player.GetComponent<PlayerController>().LevelStart && !isPaused && !player.GetComponent<PlayerController>().IsDead)
        {
            PlayerScore = (Time.frameCount - _framePaused - _frameStart) / 10f;
            score.text = ((int)PlayerScore).ToString();
            // Distance calculation 
            if (PlayerScore % distanceFactor == 0)
            {
                distance += 100;// when player has crossed 100 meters speed will increese 
                txtDistance.text = distance + " m";
                StartCoroutine(ShowDistance());
                // Increasing the velocity 
                player.GetComponent<PlayerController>().fVelocity += 2;
                if (AnimSpeed < 2)
                    AnimSpeed += 0.1f;
                player.GetComponent<Animator>().SetFloat("AnimSpeed", AnimSpeed);

            }
        }
        else if (isPaused)
        {
            _framePaused++;
        }
        else if (player.GetComponent<PlayerController>().LevelStart == false)
        {
            _frameStart++;
        }
    }

    private IEnumerator ShowDistance()
    {
        distanceAnimator.SetTrigger("Show");
        yield return new WaitForSeconds(2);
        distanceAnimator.SetTrigger("Hide");
    }

     public void KillPlayer(GameObject enemy)
     {
            //Save highest score
            SaveScore();
            player.GetComponent<PlayerController>().IsDead = true;
            player.GetComponent<PlayerController>().movementSettings.forwardInput = 0;
            player.GetComponent<Animator>().SetTrigger("Die");
            // Hide Player Canvas 
            PlayerCanvas.SetActive(false);
            // Update the ContinueHearts
            if (countinueCount == 1)
            {
                txtContinueHeart.text = (countinueValue * 2).ToString();
            }
            //show game over window
            GameOverUI.GetComponent<Animator>().SetTrigger("Show");
            // Save Coins 
            SaveCoins();
            // Save Hearts 
            SaveHearts();
            // Save Highest Multiplier
            SaveMultiplier();
            _enemy = enemy;



    }

    private GameObject _enemy;
    private int countinueCount = 0;
    private int countinueValue = 1;

    //in this function we need to count how many times player has continued game.
    public void GameContinue()
    {
        if (countinueCount == 0)
        {
            countinueCount = 1;
        }
        else //if its not first time
        {
            countinueValue = countinueValue * 2; // 1*2=2, third try 2*2 =4, fourth 4*2=8 8 hearts will be required player to continue
        }

        if (heartCount >= countinueValue)
        {
            GameObject.Destroy(_enemy);
            player.GetComponent<PlayerController>().IsDead = false;
            player.GetComponent<PlayerController>().movementSettings.forwardInput = 1;
            player.GetComponent<Animator>().SetTrigger("Run");
            // Show Player Canvas 
            PlayerCanvas.SetActive(true);
            // Hide Game Over Window 
            GameOverUI.GetComponent<Animator>().SetTrigger("Hide");
            // Update the CollectedHearts
            heartCount = heartCount - countinueValue;
            
        }
        else //
        {
            Menus.instance.ShowAchievments();// calling WhowAchievement function from menus
        }
    }

    // save score function
    private void SaveScore()
    {
        int HighestScore = PlayerPrefs.GetInt("HighestScore");// compare previous scores
        if (PlayerScore > HighestScore)
        {
            PlayerPrefs.SetInt("HighestScore", (int)PlayerScore);//see highest score
        }
    }

    // Save hearts function
    private void SaveHearts()
    {
        int CollectedHearts = PlayerPrefs.GetInt("CollectedHearts");
        CollectedHearts = heartCount + CollectedHearts;
        PlayerPrefs.SetInt("CollectedHearts", CollectedHearts);
    }

    // save coins function
    private void SaveCoins()
    {
        int CollectedCoins = PlayerPrefs.GetInt("CollectedCoins");
        CollectedCoins = coinCount + CollectedCoins;
        PlayerPrefs.SetInt("CollectedCoins", CollectedCoins);
    }

    // Multiplier function
    private void SaveMultiplier()
    {
        int HighestMultiplier = PlayerPrefs.GetInt("HighestMultiplier");//saved
        if (multiplier > HighestMultiplier)//compare
        {
            PlayerPrefs.SetInt("HighestMultiplier", (int)multiplier);//save
        }
    }

    // this function will recieve game object
    public void CategorySwitch(CategoryButton btn)
    { // All Players
        btn.Switch();//
        activeButton.Switch();
        activeButton = btn;
        achvScrollbar.content = btn.achievementMenu.GetComponent<RectTransform>();

    }

    //This function will start level when clicked
    public void StartGame()
    {
        player.GetComponent<Animator>().SetTrigger("Run");
        player.GetComponent<PlayerController>().LevelStart = true;
        GameObject.Find("MainMenu").SetActive(false);//Deactivate main menu
        PlayerCanvas.SetActive(true);// activate player
    }
}
