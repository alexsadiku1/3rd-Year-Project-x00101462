using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Movement : MonoBehaviour {
    public GameObject GameManagerGo;
	public GameObject BulletS;
	public GameObject Bullet1;
	public GameObject Bullet2;
    public GameObject ExplosionGo;
	public float speed;

    public Text LivesUIText;

    const int MaxLives = 3;
    int lives;

    public void Init()
    {
        lives = MaxLives;
        //update the lives UI Text
        LivesUIText.text = lives.ToString();

        gameObject.SetActive(true);
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            //play the laser sound
            gameObject.GetComponent<AudioSource>().Play();

            GameObject bullet1 = (GameObject)Instantiate(BulletS);
            bullet1.transform.position = Bullet1.transform.position;

            GameObject bullet2 = (GameObject)Instantiate(BulletS);
            bullet2.transform.position = Bullet2.transform.position;
        }

	float x = Input.GetAxisRaw("Horizontal");
	float y = Input.GetAxisRaw("Vertical");
	
	
	Vector2 direction = new Vector2(x,y).normalized;
	
	Move(direction);
	}
	void Move(Vector2 direction){
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2 (0,0));
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2 (1,1));
		
		max.x = max.x - 0.225f;
		min.x = min.x + 0.225f;
		
		max.y = max.y - 0.285f;
		min.y = min.y + 0.285f;
		
		Vector2 pos = transform.position;
		
		pos += direction * speed * Time.deltaTime;
		
		pos.x = Mathf.Clamp (pos.x, min.x, max.x);
		pos.y = Mathf.Clamp (pos.y, min.y, max.y);
		
		transform.position = pos;
		
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        //Detect Collision
        if ((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag"))
        {
            PlayerExplosion();

            lives--;//take a life away when hit
            LivesUIText.text = lives.ToString();

            if (lives == 0)//if lives hit zero
            {
                //
                GameManagerGo.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);

                //gameObject.SetActive(false); //hides the ship


                Destroy(gameObject);//destroy ship
            }
        }
    }

    //Inititate and explosion
    void PlayerExplosion()
    {
        GameObject exp = (GameObject)Instantiate(ExplosionGo);

        exp.transform.position = transform.position;
    }
}
