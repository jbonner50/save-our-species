using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    public static int health;
    public float initialSpeed = 0.75f;
    public static Vector3 currentPos;
    Rigidbody2D rb;
    CircleCollider2D cc;
    public GameObject explosion;
    public GameObject astronaut;


    Vector3 initialScale;
    private float initialColliderRadius;
    public bool shrinking = false;

    // Start is called before the first frame update
    void Start()
    {
        shrinking = false;
        health = 3;
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(initialSpeed, 0);
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 5)
        {
            health = 5;
        }

        if (health <= 0)
        {
            health = 0;
            UI.stopTimer();
            gameObject.GetComponent<Explodable>().explode();
            GameManager.gameOver = true;


        }
        if(transform.localScale.magnitude / initialScale.magnitude < 0.1f) {
            health = 0;
            UI.stopTimer();
            GameManager.gameOver = true;
            Destroy(gameObject);

        }

        if (!shrinking)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, initialScale, Time.deltaTime * 2f);

        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0, 0), Time.deltaTime * 1f);

        }

        
        currentPos = transform.position;
    }


    public void resize(float timer, float timeUntilDie)
    {
        float percent = timer / timeUntilDie;
        transform.localScale = initialScale * percent;
    }

    public void restore()
    {

        transform.localScale = initialScale;
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Asteroid"))
        {

            health--;

            GameObject currentExplosion = Instantiate(explosion, other.gameObject.transform.position, Quaternion.identity);
            SoundController.playImpactSound();
            Destroy(currentExplosion, currentExplosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

            if (health > 0)
            {
                
                Destroy(other.gameObject);
            }

        
        } else if (other.gameObject.CompareTag("Sun"))
        {
            health = 0;
        }

        


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Asteroid") && health == 1)
        {

            health = 0;

            GameObject currentExplosion = Instantiate(explosion, other.gameObject.transform.position, Quaternion.identity);
            SoundController.playImpactSound();
            Destroy(currentExplosion, currentExplosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

        } else if (other.gameObject.CompareTag("Capsule"))
        {
            Destroy(other.gameObject);
            GameObject newAstronaut = Instantiate(astronaut, transform.position, Quaternion.identity);
            newAstronaut.transform.parent = gameObject.transform;
            SoundController.playCapsuleSound();
            Destroy(newAstronaut, newAstronaut.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

            Earth.health++;

        }

    }


    private void OnBecameInvisible()
    {

          GameManager.gameOver = true;
        
    }

    

}
