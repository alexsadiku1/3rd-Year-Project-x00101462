using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Warp1 : MonoBehaviour {

    public int finalscore;
void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "PlayerShipTag")
        {
            PlayerPrefs.SetInt("ScoreText", finalscore);

            SceneManager.LoadScene(9);

            finalscore = PlayerPrefs.GetInt("ScoreText");
        }
    }
}
