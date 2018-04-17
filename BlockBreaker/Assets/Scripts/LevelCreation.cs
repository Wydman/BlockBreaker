using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelCreation : MonoBehaviour {

    private Vector3[] brickPos = new[] { new Vector3(0, 0, 0), new Vector3(0, 2.24f, 0),new Vector3(0, 4.48f, 0),
                                         new Vector3(3.5f, 0, 0),new Vector3(3.5f, 2.24f, 0),new Vector3(3.5f, 4.48f, 0),
                                         new Vector3(7f, 0, 0),new Vector3(7f, 2.24f, 0),new Vector3(7f, 4.48f, 0),
                                         new Vector3(10.5f, 0, 0),new Vector3(10.5f, 2.24f, 0),new Vector3(10.5f, 4.48f, 0)};
    int fCount;
    int randomPattern;


   

    public void Start()
    {
        // je cherche le nombre de fichiers dans resources

        //////////////////////////// CETTE LIGNE NE FONCTIONNE PLUS APRES LE BUILD DE L'APP//////////////////////////////
        // il y a d'autre itération de se problème qui font que le jeu n'est pas buildable, genre dans Brick avec l'autre getfile :'(///////////////
        // POUR LINSTANT JE DESACTIVE TOUT ET MET LE NOMBRE DE FICHIER A LA PLACE DE fCount en dessous//
    //    fCount = (Directory.GetFiles("Assets\\Resources\\Prefabs\\Bricks\\BricksPattern", "*", SearchOption.TopDirectoryOnly).Length) / 2;
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
     //   Debug.Log("Nombre de Patterns : " + fCount);
        int n = 0;
        while (n <= 11)
        {
            randomPattern = Random.Range(0, 12/*fCount*/);
            // string.format permet d'incrémenter uen variable dans le string, 
            //il s'utilise en donnant la variable en fin (randompattern)
            // et en mettant un {0} a l'endroit ou on veut la faire apparaitre


            GameObject go = Instantiate(Resources.Load(string.Format("Prefabs\\Bricks\\BricksPattern\\prefabTest{0}", randomPattern))) as GameObject;
            go.transform.position = brickPos[n];
            go.name = ("B" + n);
            n++;
        }
    }




}
