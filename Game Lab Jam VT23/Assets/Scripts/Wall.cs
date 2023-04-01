using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] int nrOfObjects;
    [SerializeField] List<GameObject> objectToSpawn;
    Vector2 spawnBounds;
    // Start is called before the first frame update
    void Start()
    {
        int listIndex = 0;

        for (int i = 0; i < nrOfObjects; i++)
        {
            Vector3 spawnPoint = new Vector3(Random.Range(-GetComponent<Collider>().bounds.size.x / 2, GetComponent<Collider>().bounds.size.x/2), Random.Range(0, GetComponent<Collider>().bounds.size.y), transform.position.z -0.5f);
           
            Instantiate(objectToSpawn[listIndex++], spawnPoint, Quaternion.identity);

            if(listIndex >= objectToSpawn.Count)
            {
                listIndex = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
