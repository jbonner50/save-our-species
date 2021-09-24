using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : MonoBehaviour
{
    Rigidbody2D rb;
    CircleCollider2D cc;
    TrailRenderer tr;

    Vector3 initialScale;
    private float initialColliderRadius;
    private float initialTrailWidth;

    public GameObject explosion;
    public bool shrinking = false;

    public GameObject astronaut;

    // Start is called before the first frame update
    void Start()
    {
        shrinking = false;
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
        tr = GetComponent<TrailRenderer>();

        initialScale = transform.localScale;
        initialColliderRadius = cc.radius;
        initialTrailWidth = tr.startWidth;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > Spawner.radius * 1.5f || transform.localScale.magnitude / initialScale.magnitude < 0.1f)
        {
            Destroy(gameObject);
        }

        if(!shrinking)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, initialScale, Time.deltaTime * 2f);
            tr.startWidth = initialTrailWidth * transform.localScale.magnitude / initialScale.magnitude;

        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0, 0), Time.deltaTime * 1f);
            tr.startWidth = initialTrailWidth * transform.localScale.magnitude / initialScale.magnitude;

        }

        //if((Earth.currentPos - transform.position).magnitude < 2) {
        //    float percent = ((Earth.currentPos - transform.position).magnitude) / 2f;
        //    transform.localScale = initialScale * percent;
        //    tr.startWidth = initialTrailWidth * percent;
        //} else
        //{
        //    restore();
        //}
    }


    public void resize(float timer, float timeUntilDie)
    {
        float percent = timer / timeUntilDie;
        transform.localScale = initialScale * percent;

    }

    public void restore()
    {

        transform.localScale = initialScale;
        tr.startWidth = initialTrailWidth;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (!other.gameObject.CompareTag("Earth"))
        {
            GameObject currentExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
            SoundController.playImpactSound();
            Destroy(currentExplosion, currentExplosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            Destroy(gameObject);
        }
        
       
    }
}
