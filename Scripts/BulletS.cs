using UnityEngine;
using System.Collections;

public class BulletS : MonoBehaviour
{
	float speed;
	
	
	void Start(){
		speed = 40;
	}
	
	void Update(){
		Vector2 position = transform.position;
		
		position = new Vector2(position.x,position.y + speed * Time.deltaTime);
		
		transform.position = position;
		
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));
		
		if(transform.position.y > max.y){
			Destroy(gameObject);
		}
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        //Detect Collision
        if (col.tag == "EnemyShipTag")
        {
            Destroy(gameObject);
        }
    }
}