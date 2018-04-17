using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus2 : MonoBehaviour {
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

            // affiche le texte seulement lorsque shootBonus est false
            if (!Paddle.shootBonus)
            {
                // je load le prefab animé  puis le place en tant qu'enfant de Canvas pour qu'il soit effectif( vuq ue c'est unUI text, il doit etre dans le Canvas)
                // puis je le positionne localement
                GameObject go = Instantiate(Resources.Load("Prefabs\\TextShootBonus")) as GameObject;
                GameObject canvas = GameObject.Find("Canvas");
                go.transform.parent = canvas.transform;
                go.transform.localPosition = new Vector2(0, 0);
            }

            Paddle.shootBonus = true;
            Destroy(gameObject);
        }

    }

}
