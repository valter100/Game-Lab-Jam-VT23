using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] int nrOfObjects;
    [SerializeField] List<GameObject> objectToSpawn;
    [SerializeField] List<FaultyObject> unfixedObjects;
    Vector2 spawnBounds;
    [SerializeField] bool randomCubes;
    // Start is called before the first frame update
    void Start()
    {
        int listIndex = 0;
        if (randomCubes)
        {
            for (int i = 0; i < nrOfObjects; i++)
            {
                Vector3 spawnPoint = new Vector3(Random.Range(-GetComponent<Collider>().bounds.size.x / 2, GetComponent<Collider>().bounds.size.x/2), Random.Range(0, GetComponent<Collider>().bounds.size.y), transform.position.z -0.5f);
           
                GameObject spawnedObject = Instantiate(objectToSpawn[listIndex++], spawnPoint, Quaternion.identity);

                if(listIndex >= objectToSpawn.Count)
                {
                    listIndex = 0;
                }

                unfixedObjects.Add(spawnedObject.GetComponent<FaultyObject>());

                spawnedObject.GetComponent<FaultyObject>().SetWall(this);
            }
        }
        else
        {
            
            unfixedObjects = FindObjectsOfType<FaultyObject>().ToList();
            
        }

        FindObjectOfType<GameManager>().UpdateObjectCount();
    }

    public void removeFixedObject(GameObject fixedObject)
    {
        unfixedObjects.Remove(fixedObject.GetComponent<FaultyObject>());
    }

    public int UnfixedObjectCount() => unfixedObjects.Count;
}
