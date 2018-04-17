using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Brick : MonoBehaviour {


  //  public static int nombreBricks = 0;
    public int maxHits;

    private bool isBreakable;

    private int timeHit;
    private int bonusChance;
    // je crée un sprite renderer pour chanegr de sprite du gameobject
    private SpriteRenderer spriteR;
    // je créé une array de sprite pour stocker le spritesheet qui contient plusieurs sprites( il est donc en multiple dans unity)
    private Sprite[] Sprite;
    // pop des différents bonus
    int fCount;
    int randomBonus;


    void Start () {

        // TODO : essayer d'appliquer la méthode du tuto en réglant le problème supposé de collisions multiples
        //       chapitre tuto :  Statics To Detect Win Condition

        /////////////////////////////////////ENCORE PLUS IMPORTANT/////////////////////////////
        /*
        j'ai rencontré un soucis avec la façon "officielle de faire en comptant les brick au début et en les décrémentant avec la destruction
        En fait, quadn je rajoutais des balles le compte n'était plus du tout juste et était vite négatif
        je pense que c'est du aux collisions en meme temps avec plusieurs balles...

        cest pas opti du tout mais ça fera le taf, je met tout ça dans le LooseColider car
        si je le met dans Le levelManager( qui est présent dans toute les scènes) il va lancer la win  instant sur toute les scènes
        du coup le placer sur le loseCollider permet d'etre sur que l'on est en partie( tant que les bricks pop au start il n'y 
        à pas de soucis a part que ce soit dans l'update


        je vais donc utiliser tout simplement ça :

                //Victoire
        if (GameObject.FindGameObjectsWithTag("Brick").Length == 0)
        {
            levelManager.LoadLevel("win");
        }

        je vais lancer la méthode qui se trouvera 


        
        */
        ////////////////////////////////////IMPORTANT VICTOIRE//////////////////////////////////
        /*
        Pourquoi j'utilise cette méthode comme détection de victoire :
        
        à l'origine pour detecter la victoire, je prenais un objet quis erais toujours là,(le loose collider)
        et dans l'update je lui demandais de lancer la win si le nombre d'objet avec le TAG Brick était égal a 0
        ça marchais très bien, mais je pense que niveau opti,
        demander de vérifier une condition a chaque frame dans l'update est pas génial.
         
        Du coup en utilisant le start pour compter chaque objet avec le TAG brick on contourne le soucis,
        car au lieu de vérifier a chaque frame avec l'update le nombre de briques, je pourrais le faire a chaque collisions,
        ce qui est mille fois plus opti.
        Donc, il ne reste qu'a décrémenter de 1 a chaque destruction d'une brique dans le oncollision et de tout placer 
        dans uen condition(encapsulation) If qui lance la win si la derniere brique est détruite
         */
        // pour condition de victoire
        //  j'initialise isBreakable pour que si le tag de CET objet(celui ou le script est attaché)
        // a le tag Brick, il renvois True !
        // donc si le tag de l'objet est Brick il renvois true, sinon false


        //  isBreakable = (tag == "Brick");
        //  if (isBreakable)
        //  {
        // au start, j'incrémente de 1 a pour chacune des briques car chaque brique va lancer se 
        //      nombreBricks++;
        //  }

        //   Debug.Log(nombreBricks);


       


        timeHit = 0;
        // je définie spriteR  pour qu'il rapporte a la méthode SpriteRenderer
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        // j'ajoute dans l'array toute les sprites du spritesheet
        Sprite = Resources.LoadAll<Sprite>("Sprites\\SpriteSheet");

    }

    // perso j'ai mis le changement de sprite dans Update pour ne aps surcharger le oncollision, mais ç'est ptete mieux dedans.
    // ma méthode n'est pas opti mais est pratique car j'ai pas besoin de gerer les sprites dans l'inspector.
    // dans le tuto le mec ajotue les sprites dans l'exploreur unity avec une variable de sprites public
    // puis les apelle avec une methode en corrélation avec la variable timeHit( qui s'incrémente en meme temps que timeHit et
    // renvois le sprite a utiliser: voici en gros les lignes :

    /*
        //déclaration de l'array en public que l'on remplira des sprites dans l'inspector unity
        public Sprite[] hitSprites;


        // la méthode de changement de sprites
        void LoadSprites()
        {
            int spriteIndex = timeHit - 1; // -1 car l'array commence a 0
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }

        puis on apelle  loadSprites() dans le oncollision

     */



    private void OnCollisionExit2D(Collision2D collision)
    {
        hit();
        SpriteChange();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag ==("Bullet"))
        {
            Destroy(col.gameObject);
            hit();
            SpriteChange();
        }
    }





    void hit()
    {
        //////////////////////////// CETTE LIGNE NE FONCTIONNE PLUS APRES LE BUILD DE L'APP//////////////////////////////
        // il y a d'autre itération de se problème qui font que le jeu n'est pas buildable, genre dans Brick avec l'autre getfile :'(///////////////
        // POUR LINSTANT JE DESACTIVE TOUT ET MET LE NOMBRE DE FICHIER A LA PLACE DE fCount en dessous//
        //    fCount = (Directory.GetFiles("Assets\\resources\\Prefabs\\Bonus", "*", SearchOption.TopDirectoryOnly).Length) / 2;
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        timeHit++;

        if (timeHit >= maxHits)
        {
            LevelManager.autoBonus++;
          //  Debug.Log(LevelManager.autoBonus);
            bonusChance = Random.Range(0, 100);
            //Debug.Log(("luck : ") + bonusChance);
            if (bonusChance <= 5 || LevelManager.autoBonus == 25)
            {
                LevelManager.autoBonus = 0;
                randomBonus = Random.Range(0, 2/*fCount*/);
                // string.format permet d'incrémenter uen variable dans le string, 
                //il s'utilise en donnant la variable en fin (randompattern)
                // et en mettant un {0} a l'endroit ou on veut la faire apparaitre

                GameObject go = Instantiate(Resources.Load(string.Format("Prefabs\\Bonus\\Bonus{0}", randomBonus))) as GameObject;
                go.transform.position = this.transform.position;
            }
         //   nombreBricks--;
         //   Debug.Log(nombreBricks);
            Destroy(gameObject);
        }
    }


    void SpriteChange()
    {
        if (timeHit == 1)
        {
            // je remplace le sprite par l'und es sprites du spritesheet
            // j'ai tattoné pour savoir que le bon sprite était le num 1 de l'array
            // *TODO* il y a surement moyen de mieux gerer pour savoir exactement quel sprite du spritesheet on met.
            spriteR.sprite = Sprite[1];
        }
        if (timeHit == 2)
        {
            spriteR.sprite = Sprite[2];
        }
    }


}
