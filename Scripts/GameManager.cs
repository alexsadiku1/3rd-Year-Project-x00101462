using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public GameObject restart;
    public GameObject ship;
    public GameObject GameOverGO;
    public GameObject replay;
    public int temps;
    public string tempn;
    public HighScoreManager hs;
    public GameScore gs;
    public GameObject nameinput;
    public InputField enterName;


    public enum GameManagerState{
        Gameplay,
        GameOver,

        }

    GameManagerState GMState;
	// Use this for initialization
	void Start () {

        StartGamePlay();

    }
	
	// Update is called once per frame
	void UpdateGameManagerState () {

        switch (GMState)
        {
            case GameManagerState.Gameplay:

                ship.GetComponent<Movement>().Init();

                break;
            case GameManagerState.GameOver:

                nameinput.SetActive(true);
                GameOverGO.SetActive(true);
                replay.SetActive(true);

                break;

        }
    }


    public void EnterName()
    {
        if (enterName.text != string.Empty)
        {

            hs.InsertScore(enterName.text, gs.Score);
            enterName.text = string.Empty;
            nameinput.SetActive(false);


        }
    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }
    public void StartGamePlay()
    {
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }
}
