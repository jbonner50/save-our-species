using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthPiece : MonoBehaviour
{
    public bool shrinking = false;
    Vector3 initialScale;

    // Start is called before the first frame update
    void Start()
    {
        shrinking = false;
        initialScale = transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {
        if (!shrinking)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, initialScale, Time.deltaTime * 2f);

        } else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0,0,0), Time.deltaTime * 1f);

        }


        if(transform.localScale.magnitude < 0.02f)
        {
            Destroy(gameObject);
        }
    }
}
