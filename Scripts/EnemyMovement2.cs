using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyMovement2 : MonoBehaviour
{

    public GameObject scoreUITextGO;
    public GameObject ExplosionGo;
    public GameObject nameinput;
    public InputField enterName;
    public HighScoreManager hs;
    public GameScore gs;

    const int MaxLives = 10;

    int lives;
    float speed;


    // Use this for initialization
    void Start()
    {
        lives = MaxLives;
        speed = 2f;
        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreText");
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 position = transform.position;

        position = new Vector2(position.x, position.y - speed * Time.deltaTime);

        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if (transform.position.y < min.y)
        {


            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        //Detect Collision
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag"))
        {
            EnemyExplosion();
        
            lives--;
            if (lives == 0)//if lives hit zero
            {
                //
               
                //gameObject.SetActive(false); //hides the ship
                scoreUITextGO.GetComponent<GameScore>().Score += 10000;
                nameinput.SetActive(true);

                Destroy(gameObject);//destroy ship
                
            }
            
        }
    }
    void EnemyExplosion()
    {
        GameObject exp = (GameObject)Instantiate(ExplosionGo);

        exp.transform.position = transform.position;

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
}
