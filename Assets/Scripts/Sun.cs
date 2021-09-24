using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public GameObject explosion;

    Rigidbody2D rb;
    Vector3 initialScale;
    Vector3 newScale;
    private float initialColliderRadius;
    PointEffector2D pe;
    // Start is called before the first frame update
    void Start()
    {
        pe = GetComponent<PointEffector2D>();
        initialScale = transform.localScale;
        newScale = initialScale;
    }


    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, newScale, 1f * Time.deltaTime);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {

        GameObject currentExplosion = Instantiate(explosion, other.gameObject.transform.position, Quaternion.identity);
        SoundController.playSunSound();

        if(!other.gameObject.CompareTag("Earth"))
        {
            Destroy(other.gameObject);

        } else
        {
            currentExplosion.GetComponent<PointEffector2D>().enabled = false;
        }

        if(other.gameObject.CompareTag("EarthPiece"))
        {
            currentExplosion.GetComponent<PointEffector2D>().enabled = false;
            newScale *= 1.01f;
            pe.forceMagnitude *= 1.01f;
        } else
        {
            newScale *= 1.1f;
            pe.forceMagnitude *= 1.3f;
        }
        
        Destroy(currentExplosion, currentExplosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

    }

}
