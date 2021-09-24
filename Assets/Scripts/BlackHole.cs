using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{

    Vector3 mousePosition;
    public float moveSpeed = 0.1f;
    private float maxScale = 2f;
    private float minScale = 1f;
    [Range(1f, 2f)]
    public float scale = 1f;
    public float scrollScale = 0.05f;
    private COLLIDER_STATE earthState;
    private COLLIDER_STATE capsuleState;
    private enum COLLIDER_STATE {None, Outer, Inner};
    public float timeUntilDieEarth = 5f;
    public float timeUntilDieCapsule = 2f;
    private float earthTimer = 5f;
    private float capsuleTimer = 2f;
    private Vector3 initialScale;
    Rigidbody2D rb;
    PointEffector2D pointEffector;
    Vector2 position = new Vector2(0f, 0f);

    public bool earthShrink = false;
    public bool capsuleShrink = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialScale = transform.localScale;
        pointEffector = GetComponent<PointEffector2D>();
        earthState = COLLIDER_STATE.None;
        capsuleState = COLLIDER_STATE.None;
        rb.angularVelocity = 90f;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);

        scale += Input.mouseScrollDelta.y * scrollScale;
        if (scale < minScale)
        {
            scale = minScale;
        }
        else if (scale > maxScale)
        {
            scale = maxScale;
        }
        transform.localScale = initialScale * scale;
        pointEffector.forceMagnitude = -10f * scale;      



    }



    private void FixedUpdate()
    {
        rb.MovePosition(position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Earth") && !other.isTrigger) {

            if(earthState.Equals(COLLIDER_STATE.Outer)) {
                earthState = COLLIDER_STATE.Inner;
            } else
            {
                earthState = COLLIDER_STATE.Outer;
            }

        }
        if(other.CompareTag("Capsule"))
        {
            if (capsuleState.Equals(COLLIDER_STATE.Outer))
            {
                capsuleState = COLLIDER_STATE.Inner;
            }
            else
            {
                capsuleState = COLLIDER_STATE.Outer;
            }
        }
    }

    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Earth") && !other.isTrigger)
        {

            if (earthState.Equals(COLLIDER_STATE.Outer))
            {
                earthState = COLLIDER_STATE.None;
            }
            else
            {
                earthState = COLLIDER_STATE.Outer;

            }
            earthTimer = timeUntilDieEarth;


            other.gameObject.GetComponent<Earth>().shrinking = false;
            

        }
        if (other.CompareTag("Capsule"))
        {
            if (capsuleState.Equals(COLLIDER_STATE.Outer))
            {
                capsuleState = COLLIDER_STATE.None;

            }
            else
            {
                capsuleState = COLLIDER_STATE.Outer;

            }


            capsuleTimer = timeUntilDieCapsule;
            other.gameObject.GetComponent<Capsule>().shrinking = false;



        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("EarthPiece"))
        {
            other.gameObject.GetComponent<EarthPiece>().shrinking = true;
        }


        if (other.CompareTag("Earth") && earthState.Equals(COLLIDER_STATE.Inner) && !other.isTrigger)
        {
            other.gameObject.GetComponent<Earth>().shrinking = true;

        }

        if (other.CompareTag("Capsule") && capsuleState.Equals(COLLIDER_STATE.Inner))
        {
            other.gameObject.GetComponent<Capsule>().shrinking = true;

        }

    }

}
