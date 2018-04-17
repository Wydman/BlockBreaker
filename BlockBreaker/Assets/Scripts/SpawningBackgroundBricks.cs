using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningBackgroundBricks : MonoBehaviour {
    public static int backgroundBrickNumber = 0;
    private int maxBrick = 30;


    void Start () {
        StartCoroutine(DelaySpawn(5));

    }
	

	void Update () {
     //   StartCoroutine(DelaySpawn(1));


    }



    void SpawningBricks()
    {
       backgroundBrickNumber = GameObject.FindGameObjectsWithTag("BackgroundBrick").Length;

                GameObject go = Instantiate(Resources.Load("Prefabs\\BrickForBackground")) as GameObject;
                go.transform.position = new Vector3 ( Random.Range(-4,20),20,7);
                go.transform.Rotate(Vector3.forward, Random.Range(-300f, +300f));
                go.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-30f,+30f));
                go.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-60, 60), Random.Range(-20, -150)));
                backgroundBrickNumber++;
            


        
    }

    IEnumerator DelaySpawn(int wait)
    {
        while (backgroundBrickNumber <= maxBrick)
        {
      //      for (int i = 0; i <= maxBrick; i++)
      //      {
                SpawningBricks();
                yield return new WaitForSeconds(wait);
      //      }
        }

    }

}
