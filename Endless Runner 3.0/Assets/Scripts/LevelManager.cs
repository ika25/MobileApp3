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
