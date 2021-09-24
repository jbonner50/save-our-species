using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > Spawner.radius * 1.5)
        {
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Sun") && !other.gameObject.CompareTag("Earth"))
        {
            GameObject currentExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
            SoundController.playImpactSound();
            Destroy(currentExplosion, currentExplosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);


            Destroy(other.gameObject);


        }
        
    }
}
