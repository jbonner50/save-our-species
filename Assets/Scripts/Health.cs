using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public GameObject[] lives = new GameObject[5];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < lives.Length; i++)
        {

            SpriteRenderer sr = lives[i].GetComponent<SpriteRenderer>();
            Animator anim = lives[i].GetComponent<Animator>();
            anim.enabled = false;

            Color c = sr.color;
            if(i < Earth.health)
            {
                c.a = 1;
                anim.enabled = true;
                anim.Play("astronaut_noloop");
            } else
            {
                anim.Play("astronaut_noloop", 0, 0f);
                c.a = 0.5f;
            }
            sr.color = c;

        }
    }
}
