using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Lives : MonoBehaviour
{
    Text livesText;

    public int lives;

    public int Live
    
    {
        get
        {
            return this.lives;
        }
        set
        {
            this.lives = value;
            UpdateLivesTextUI();
        }
    }

    // Use this for initialization
    void Start()
    {
        livesText = GetComponent<Text>();
    }

    // Update is called once per frame
    void UpdateLivesTextUI()
    {
        string scoreStr = string.Format("{0:000000}", lives);
        livesText.text = scoreStr;
    }
}
