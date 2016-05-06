using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class HighScoreManager : MonoBehaviour {


    private List<HighScore> highscore = new List<HighScore>();
    // List<HighScore> hs = new List<HighScore>();
    public GameObject scorePrefab;
    public Transform scoreParent;
    private string connectionString;
    public int topRanks;
    public InputField enterName;
    public GameObject nameDialog;
    public GameScore gs;
	// Use this for initialization
	void Start () {
        connectionString = "URI=file:" + Application.dataPath + "/HighScoreDB.sqlite";
        CreateTable();
        //InsertScore("Alexaa", 10000);
        //GetScores();

        ShowScore();
        
	}
	
    private void GetScores()
    {
        highscore.Clear();

        using(IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using(IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "Select * From HighScores";

                dbCmd.CommandText = sqlQuery;
                

                using(IDataReader reader = dbCmd.ExecuteReader())
                {
                   
                    while (reader.Read())
                    {

                        highscore.Add(new HighScore(reader.GetInt32(0), reader.GetInt32(2), reader.GetString(1)));
                        
                    }
                    dbConnection.Close();
                    reader.Close();

                }
            }
        }
        highscore.Sort();

    }

    public void InsertScore(string name, int newScore)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format ("INSERT INTO HighScores(Name,Score) Values(\"{0}\",\" {1}\")",name,newScore);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();


            }
        }
    }
    //
    private void DeleteScore(int id)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("Delete From highscores where Rank =\"{0}\"", id);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();


            }
        }

    }

    private void ShowScore()
    {
        GetScores();

        foreach(GameObject score in GameObject.FindGameObjectsWithTag("Score"))
        {
            Destroy(score);
        }

        for (int i = 0; i < topRanks; i++)
        {
            if (1 <= highscore.Count - 1)
            {
                GameObject tmpObjec = Instantiate(scorePrefab);

                HighScore tmpScore = highscore[i];

                tmpObjec.GetComponent<HighScoreScript>().SetScore(tmpScore.Name, tmpScore.Score.ToString(), "#" + (i + 1).ToString());

                tmpObjec.transform.SetParent(scoreParent);
                tmpObjec.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }
        }
    }
    private void CreateTable()
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("CREATE TABLE if not exists HighScores (Rank INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL  UNIQUE , Name CHAR NOT NULL , Score INTEGER NOT NULL )");

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();


            }
        }

    }




    public void EnterName()
    {
        if(enterName.text != string.Empty)
        {
            
            InsertScore(enterName.text, gs.Score);
            enterName.text = string.Empty;
           
            ShowScore();
        }
    }

	// Update is called once per frame
    
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            nameDialog.SetActive(!nameDialog.activeSelf);
        }
	
    }
    
}
