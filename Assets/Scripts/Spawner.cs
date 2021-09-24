using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //asteroid = 0, spacecapsule = 1
    public GameObject[] prefabList = new GameObject[2];
    private List<GameObject> asteroidsAndCapsules = new List<GameObject>();
    public static float radius = 12f;
    public float velocityScale = 1f;
    public float respawnTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(wave());
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetMouseButtonDown(0))
        //{
        //    SpawnRandomObject();
        //}

        //List<GameObject> temp = new List<GameObject>(asteroidsAndCapsules);
        //asteroidsAndCapsules.Clear();
        //while(temp.Count > 0)
        //{
        //    GameObject obj = temp[0];
        //    temp.RemoveAt(0);
        //     else
        //    {
        //        asteroidsAndCapsules.Add(obj);
        //    }
            
        //}
        
    }

 

    void SpawnRandomObject()
    {
        float randomAngle = Random.Range(0f, 2* Mathf.PI);
        //float shiftX = Random.Range(-6f, 2f);
        //shiftX = shiftX > -2f ? shiftX + 4f : shiftX;
        //float shiftY = Random.Range(-3f, 1f);
        //shiftY = shiftY > -1f ? shiftY + 2f : shiftY;
        float randomX = Mathf.Cos(randomAngle) * radius;
        float randomY = Mathf.Sin(randomAngle) * radius;
        Vector3 randomPos = new Vector3(randomX, randomY, 0);
        Vector3 randomDestination = Random.insideUnitCircle * 4f;

        GameObject instanciatedObject;
        float rand = Random.Range(0f, 1f);
        if(rand < 0.9f)
        {
            //asteroid
            instanciatedObject = Instantiate(prefabList[0], randomPos, Quaternion.identity);
            //instanciatedObject.transform.localScale *= Random.Range(0.5f, 1.5f);


        }
        else
        {
            //capsule
            instanciatedObject = Instantiate(prefabList[1], randomPos, Quaternion.identity);

        }
        //asteroidsAndCapsules.Add(instanciatedObject);
        Rigidbody2D rb = instanciatedObject.GetComponent<Rigidbody2D>();

        rb.angularVelocity = Random.Range(-45f, 45f);
        rb.velocity = (randomDestination - randomPos) / (randomDestination - randomPos).magnitude * (velocityScale + Random.Range(0f,1f));

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        Gizmos.DrawWireSphere(this.transform.position, radius);
    }

    IEnumerator wave()
    {
        while(true)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnRandomObject();
        }
    }
}
