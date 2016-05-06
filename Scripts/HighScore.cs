using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;


    
   class HighScore : IComparable<HighScore>
    {
   public int Score { get; set; }
    public string Name { get; set; }
    public int Id { get; set; }
    public int HighScores { get; private set; }

    public HighScore(int id, int score, string name)
    {
        this.Score = score;
        this.Name = name;
        this.Id = id;
    }

        public int CompareTo(HighScore other)
        {
            if (other.Score < this.Score)
            {
                return -1;
            }
            else if (other.Score > this.Score)
            {
                return 1;
            }
            return 0;
        }
        

 
}

