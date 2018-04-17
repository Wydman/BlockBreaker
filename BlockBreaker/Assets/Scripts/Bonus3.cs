using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus3 : MonoBehaviour {
    // non activé
    // private int n = 10;

    void Start () {
		
	}
	
	
	void Update () {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -2));
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag==("Paddle"))
        {
            Paddle.laserBonus = true;
            Destroy(gameObject);
        }

    }

}
