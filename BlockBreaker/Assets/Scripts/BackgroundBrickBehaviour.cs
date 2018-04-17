using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBrickBehaviour : MonoBehaviour {



    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == ("DestroyBackgroundBrickCollider"))

        {
            SpawningBackgroundBricks.backgroundBrickNumber--;
            Destroy(gameObject);
        }
    }

}
