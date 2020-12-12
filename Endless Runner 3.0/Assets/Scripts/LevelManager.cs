﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    private int _framePaused = 0;

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
    // Distance Veriables
    public int distance = 0;
    public int distanceFactor = 200;
    public Text txtDistance;
    public Animator distanceAnimator;
    public float AnimSpeed = 1f;
    //Data Storage veriables
    public GameObject GameOverUI;


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
        if (!isPaused && !player.GetComponent<PlayerController>().IsDead)
        {
            PlayerScore = (Time.frameCount - _framePaused) / 10f;
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
    }

    private IEnumerator ShowDistance()
    {
        distanceAnimator.SetTrigger("Show");
        yield return new WaitForSeconds(2);
        distanceAnimator.SetTrigger("Hide");
    }

     public void KillPlayer()
        {
            //Save highest score
            SaveScore();
            player.GetComponent<PlayerController>().IsDead = true;
            player.GetComponent<PlayerController>().movementSettings.forwardVelocity = 0;
            player.GetComponent<Animator>().SetTrigger("Die");
            //show game over window
            GameOverUI.GetComponent<Animator>().SetTrigger("Show");
            // Save Coins 
            SaveCoins();
    }

    private void SaveScore()
    {
        int HighestScore = PlayerPrefs.GetInt("HighestScore");// compare previous scores
        if (PlayerScore > HighestScore)
        {
            PlayerPrefs.SetInt("HighestScore", (int)PlayerScore);//see highest score
        }
    }

    private void SaveCoins()
    {
        int CollectedCoins = PlayerPrefs.GetInt("CollectedCoins");
        CollectedCoins = coinCount + CollectedCoins;
        PlayerPrefs.SetInt("CollectedCoins", CollectedCoins);
    }
}
