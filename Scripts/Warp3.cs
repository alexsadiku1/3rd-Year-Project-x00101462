using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Warp3 : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "PlayerShipTag")
        {

            SceneManager.LoadScene(11);
        }
    }
}
