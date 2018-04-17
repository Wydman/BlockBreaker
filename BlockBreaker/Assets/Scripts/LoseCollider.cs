using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCollider : MonoBehaviour {


    // on crée une variable levelmanager qui sera son propre type (en l'occurence un script)
    // une autre solution serait de chercher le gameobject par TAG ou par nom, puis de l'ajouter a une variable
    // mais dans ce cas il faudrais utiliser le getcomponent<>() pour recuperer le script dans le gameobject
    // à chaque fois afin de lancer le script.
    /* Exemple :
     * private GameObject levelManager
     * /
     * levelManager = GameObject.FindObjectsWithTag("LevelManager"); ou par nom : GameObject.Find("Level Manager");
     * /
     * levelManager.getcomponent<LevelManager>().LoadLevel("Win");
    */
    private LevelManager levelManager;

    void Start () {
        // on va chercher le script LevelManager et on l'applique a la variable
		levelManager = GameObject.FindObjectOfType<LevelManager>();
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {

        if (GameObject.FindGameObjectsWithTag("Ball").Length == 1)
        {
            if (col.gameObject.tag == ("Ball"))
                
            {
                // on lane ensuite le script via la variable
                Paddle.shootBonus = false;
                levelManager.LoadLevel("Lose");
                
            }
        }
        
    }

      void Update () {
          //Victoire
          if (GameObject.FindGameObjectsWithTag("Brick").Length == 0)
          {
              GameObject.Find("LevelNumber").GetComponent<LevelNumber>().IncrementationLevelNumber();
              Paddle.shootBonus = false;
              levelManager.LoadLevel("win");
          }
      }
}
