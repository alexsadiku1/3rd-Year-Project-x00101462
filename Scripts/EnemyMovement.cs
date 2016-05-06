using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public GameObject scoreUITextGO;
    public GameObject ExplosionGo;
    float speed;
	// Use this for initialization
 



	void Start () {
	speed = 6f;
        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreText");
        DontDestroyOnLoad(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {

        Vector2 position = transform.position;

        position = new Vector2(position.x, position.y - speed * Time.deltaTime);

        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if(transform.position.y < min.y)
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
            scoreUITextGO.GetComponent<GameScore>().Score += 100;
            Destroy(gameObject);
        }
    }
    void EnemyExplosion()
    {
        GameObject exp = (GameObject)Instantiate(ExplosionGo);

        exp.transform.position = transform.position;
    }
}
