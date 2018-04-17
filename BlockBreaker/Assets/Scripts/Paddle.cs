using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {
    // positioné a 8 car c'est la moitié de l'écran en game unit
    float mousePosInBlocks;
    private GameObject ball;
    public int ballNumberBonus = 10; // je le met la car je le veux accessible tout le temps(il renvois la variable a bonus1)
    int random = 0;
    float random2 = 0;
    float[] paddlePos= new float[] { -0.5f, 0f, 0.5f};
    static bool autoPlay = false;
    public static bool shootBonus = false;
    public static bool laserBonus = false;
    // je dois placer ma couroutine dans une variable pour pouvoir la stopper
    private IEnumerator co;





    void Start () {
        

    }
	
	
	void Update () {
        if(shootBonus){StartCoroutine("ShootBonus");}
        else{StopCoroutine("ShootBonus");}
 

        ActivationAutoPlay();
        ActivationCheat();
        if (!autoPlay)
        {
            MoveWithMouse();
        }
        else
        {
            AutoPlay();
        }
    }



    void MoveWithMouse()
    {
        ///////////////////////////IMPORTANT//////////////////////////////
        // on place une limite de mouvement a notre variable (Mathf.Clamp(valeur,min,max)
        // cette valeur que l'on définie par la position x (en pixel)(pour le defilement horizontal evidemment)
        // et que l'on divise par la taille de l'écran ( en pixel) (Screen.width = 800 car du 800/600)
        // ce qui nous donne une valeur entre 0 et 1
        // ensuite on multiplie par 16, d'ou viens ce 16 :
        // lorsque l'on a créé notre background , donc notre aire de jeu, on a décidé de le définir a 50 pixel per unit
        //(vérifiable sur les caractéristique du sprite background(dans les assets))
        // du coup on multiplie par 16 car 800/50 = 16
        // en gros on a découpé la surface en 16 et maintenant on veut placer la souris sur toute sa longueur !
        // si on ne multiplie pas par 16, la souris ne peut se déplacer que dans le premier bloc de notre aire de jeu
        // voir explication en fin de code *
        mousePosInBlocks = Mathf.Clamp(Input.mousePosition.x / Screen.width * 16, 0.5f, 15.5f);
        transform.position = new Vector3(mousePosInBlocks, 0.5f, 0);
    }

    void AutoPlay()
    {
        if (GameObject.FindWithTag("Ball").GetComponent<Ball>().launched != true)
        {
            GameObject.FindWithTag("Ball").GetComponent<Rigidbody2D>().AddForce(new Vector2(200, 200));
            GameObject.FindWithTag("Ball").GetComponent<Ball>().launched = true;

        }

            transform.position = new Vector3(ball.transform.position.x + random2, 0.5f, 0);
    }

    void ActivationAutoPlay()
    {
        //  activation du autoPlay avec la touche C
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (autoPlay == false)
            { autoPlay = true; }
            else { autoPlay = false; }
        }
    }
    void ActivationCheat()
    {
        // si il n'y avais qu'une seule balle je pourrais mettre "ball = ------" dans le start, mais
        // j'ai besoin que meme si la premeire ball sorte du jeu que le cheat puisse en trouver une nouvelle
        ball = GameObject.FindWithTag("Ball");
        if (Input.GetKeyDown(KeyCode.L))
        {
            ballNumberBonus = 1;
        }
    }



    //   petit truc pourf aire en sorte que le paddle switch aléatoriement entre plusieurs positions en autoplay
    // j'utilise une array avec 3 positions précises, et j'utilsie un Random.range pour choisir l'une d'entre elle a chaque collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        random = Random.Range(0, 3);
        random2 = paddlePos[random];
    }
    //  



    IEnumerator ShootBonus()
    {

        if (Input.GetMouseButtonDown(0))
            {
            Debug.Log("pan !");


            // OK très utile IMPORTANT 
            // au lieu d'instancier un objet à 0,0,0 puis de le déplacer a l'endroit ou on veut le mettre, on peut faire ça :
            //Instantiate(Resources.Load("Prefabs\\Bullet"), new Vector3(this.transform.position.x - 0.3f, 0.6f, 0), Quaternion.identity) as GameObject;
            // on est obligé de mettre un quaternion si on donne un vector 3, c'est comme ça.
            // par contre on peut juste mettre un transform si l'on veut, j'aurais faire ça :
            //Instantiate(Resources.Load("Prefabs\\Bullet"), this.transform) as GameObject;
            // ce qui aurait pour effet de faire pop le truc a l'endroit du paddle
            // ça peut etre très utile car les Bullet instanciées ont un TrailRenderer, qui meme désactivé a cause de sa durée, se souvient de l'endroit ou il a été créé
            // et fais donc un trait droit avant de changer de place
            // le Quaternion.identity veut dire que l'objet est parfaitement aligné avec le monde ou les parents de l'objet COOL !
            GameObject go0 = Instantiate(Resources.Load("Prefabs\\Bullet"), new Vector3(this.transform.position.x - 0.3f, 0.6f, 0), Quaternion.identity) as GameObject;
            go0.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 200));
            GameObject go1 = Instantiate(Resources.Load("Prefabs\\Bullet"), new Vector3(this.transform.position.x + 0.3f, 0.6f, 0), Quaternion.identity) as GameObject;
            go1.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,200));



        }
            yield return new WaitForSeconds(10);
            shootBonus = false;
            Debug.Log("shootbonus false");
          //  StopCoroutine(coroutine);
    }

    IEnumerator LaserBonus()
    {
        
            Debug.Log("LASERRR");
            GameObject go0 = Instantiate(Resources.Load("Prefabs\\Bullet"), new Vector3(this.transform.position.x - 0, 0.6f, 0), Quaternion.identity) as GameObject;
            go0.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 200));



        
        yield return new WaitForSeconds(10);
        shootBonus = false;
        Debug.Log("shootbonus false");
        //  StopCoroutine(coroutine);
    }

    /*   
     
    * = We defined the World Space to be 16 WU wide.
        See the settings of the background image( voir le sprite dans les assets): the PPU(pixel per unit).
        Regard the screen as ranging from 0 to 1 in width. 1 is the far right edge when it comes to the width.

        Input.mousePostion.x returns the x value of a pixel coordinate. If you divide that by the Screen.width,
        you get a percentage ranging from 0 to 1. On the right side of your screen, the x position of your mouse would be 800,
        just like the width of your screen. 800/800 = 1.

        As aforementioned, the segment of the World Space we see through our camera is 16 WU wide,
        not 1 WU. Therefore we multiply the value by 16. Remove the 16,
        and you'll notice that the paddle only moves in about 1/16th of your screen.

        Does that make sense? If so, please mark a top answer and add [Solved] in front of your title.
        This really saves us a lot of time, as the issue won't appear on our list when we do our daily support.

    */
}
