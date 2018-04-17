using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private GameObject Paddle;
    private Vector3 paddleToBallVector;
    private AudioSource audioSource;
  //  public AudioClip clip;
    public bool launched = false;

    Vector2 ballVelocity;
    float ballSpeed;



    void Start () {
        Paddle = GameObject.Find("Paddle");
        paddleToBallVector = transform.position - Paddle.transform.position;
          audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {

        KeepVelocity();


        if (launched != true)
        {
            transform.position = Paddle.transform.position + paddleToBallVector;
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (launched != true)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(200, 200));
                
            }


            launched = true;
            //GetComponent<Rigidbody2D>().velocity = new Vector2(5, 5);
            //GetComponent<Rigidbody2D>().AddRelativeForce( new Vector2(15,15));
        }
    }

    void KeepVelocity()
    {
        // gestion de la vélocité des balles 
        // il faut comprendre que ballVelocity est un vecteur, et qu'on peut calculer avec la vitesse (magnitude)
        // la magnitude ne peut etre modifée, mais on peut s'en servir comme condition
        ballVelocity = GetComponent<Rigidbody2D>().velocity;
        ballSpeed = ballVelocity.magnitude;
        //Debug.Log(ballSpeed);

        if (ballSpeed <= 5.9)
        {
            GetComponent<Rigidbody2D>().AddForce(ballVelocity);
        }
        if (ballSpeed >= 6.1)
        {
            GetComponent<Rigidbody2D>().AddForce(-ballVelocity);
        }


    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        // condition pour le son boing
        if (collision.gameObject.tag == ("Brick"))
        {
            if (launched) { audioSource.Play(); }
        }
        if (collision.gameObject.name == ("Paddle"))
        {
            if (launched) { audioSource.Play(); }
        }

        // empecher les boring loop (quand la balle a des trajectoires rectilignes)
        // en fait j'ajoute une force randoma  chaque bump, une petite pas trop visible dans le jeu
        // mais très utile lorsque la trajectoire est rectiligne entre 2 mur
        Vector2 tweak = new Vector2(Random.Range(-10f,10.2f), Random.Range(-10f, 10.2f));
        GetComponent<Rigidbody2D>().AddForce(tweak);

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == ("LoseCollider"))

        {
            Destroy(gameObject);
        }
    }





}
