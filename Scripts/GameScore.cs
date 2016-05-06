using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GameScore : MonoBehaviour,IComparable<GameScore>
{
    Text scoreTextUI;

    static public int score;
    public int highscore;
    private int v1;
    private string v2;

    public string name { get; set; }
    public int ID { get; set; }

    
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            UpdateScoreTextUI();
           
        }
    }

    public int HighScore
    {
        get
        {
            return highscore;
        }
        set
        {
            highscore = value;

        }
    }



    public GameScore(int id, int _score, string name)
    {
        score =_score;
        highscore = score;
        this.name = name;
        this.ID = id;
    }

    public GameScore(int v1, string v2)
    {
        this.v1 = v1;
        this.v2 = v2;
    }




    // Use this for initialization
    void Start()
    {

        

        scoreTextUI = GetComponent<Text>();
    }

    // Update is called once per frame
    void UpdateScoreTextUI()
    {
        string scoreStr = string.Format("{0:000000}", score);

        scoreTextUI.text = score.ToString();
    }

    public int CompareTo(GameScore other)
    {if(other.HighScore < this.HighScore)
        {
            return -1;
        }
    else if (other.HighScore > this.HighScore)
        {
            return 1;
        }
        return 0;
    }
}
