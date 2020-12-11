using System.Collections;
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
            PlayerScore = (Time.frameCount - _framePaused) / 10;
            score.text = PlayerScore.ToString();
        }
        else if (isPaused)
        {
            _framePaused++;
        }
    }
}
