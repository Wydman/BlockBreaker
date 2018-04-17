using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public static int levelNumber = 1;
    public static int autoBonus = 0;


    public void LoadLevel(string name)
    {
        if (name == "Start")
        {
            // on remet le compte des level a 0 (1)
            LevelManager.levelNumber = 1;
            Debug.Log("RESET SCORE");
            Debug.Log("Level load requested name" + name);
            // a utiliser pour changer de scènes, mais il faut ajouter "using UnityEngine.SceneManagement;" en haut
            SceneManager.LoadScene(name);
        }

        // condition pour reset le score après la scène loose pour pouvoiur l'afficher 
        else{
            Debug.Log("Level load requested name" + name);
            // a utiliser pour changer de scènes, mais il faut ajouter "using UnityEngine.SceneManagement;" en haut
            SceneManager.LoadScene(name);
        }

    }

    public void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }

}
