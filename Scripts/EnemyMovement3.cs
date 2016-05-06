using UnityEngine;
using System.Collections;

public class EnemyMovement3 : MonoBehaviour
{

    public GameObject scoreUITextGO;
    public GameObject ExplosionGo;

    const int MaxLives = 5;

    int lives;
    float speed;


    // Use this for initialization
    void Start()
    {
        lives = MaxLives;
        speed = 10f;
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
                scoreUITextGO.GetComponent<GameScore>().Score += 500;


                Destroy(gameObject);//destroy ship
            }

        }
    }
    void EnemyExplosion()
    {
        GameObject exp = (GameObject)Instantiate(ExplosionGo);

        exp.transform.position = transform.position;
    }
}
