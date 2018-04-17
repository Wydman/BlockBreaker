using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus1 : MonoBehaviour {
    // non activé
    private int n = 10;

    void Start () {
		
	}
	
	
	void Update () {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -2));
        n = GameObject.Find("Paddle").GetComponent<Paddle>().ballNumberBonus;

    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag==("Paddle"))
        {
            ///////////////////////////////////IMPORTANT//////////////////////////////
            // pour ce text, j'utilsie un Text Mesh pour pouvoir le faire suivre un gameobject, sinon en UI text vu qu'il est lié au canvas,
            // impossible de lui faire suivre des coordonnées d'un gameobject ^^'
            // important, vu qu'il est animé et que les coordonnées de l'animation (pour justement le faire bouger) son fixe,
            // je suis obligé de le mettre dans un parent Empty pour pouvoir  bouger l'objet parent sans foutre en l'air les coordonées de l'animation !:)
           GameObject go1 = Instantiate(Resources.Load("Prefabs\\MeshTextBallBonus")) as GameObject;
           go1.transform.position = GameObject.FindWithTag("Ball").transform.position;



            while (n <= 10)
              {
                Debug.Log("+1ball");
                GameObject go = Instantiate(Resources.Load("Prefabs\\Ball")) as GameObject;
                go.transform.position = GameObject.FindWithTag("Ball").transform.position;
                go.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-300,300), 300));
                go.GetComponent<Ball>().launched = true;
                Destroy(gameObject);
                n++;
               }


        }

    }

}
