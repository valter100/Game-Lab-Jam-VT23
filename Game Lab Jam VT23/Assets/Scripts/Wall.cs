using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] int nrOfObjects;
    [SerializeField] List<GameObject> objectToSpawn;
    [SerializeField] List<FaultyObject> unfixedObjects;
    [SerializeField] GameObject[] impassableWalls;
    Vector2 spawnBounds;
    [SerializeField] bool randomCubes;
    // Start is called before the first frame update
    void Start()
    {
        impassableWalls = GameObject.FindGameObjectsWithTag("Impassable");

        int listIndex = 0;
        if (randomCubes)
        {
            for (int i = 0; i < nrOfObjects; i++)
            {
                Vector3 spawnPoint = new Vector3(Random.Range(-GetComponent<Collider>().bounds.size.x / 2.2f, GetComponent<Collider>().bounds.size.x/2.2f), Random.Range(0.5f, GetComponent<Collider>().bounds.size.y - 0.5f), transform.position.z -0.5f);
           
                GameObject spawnedObject = Instantiate(objectToSpawn[listIndex++], spawnPoint, Quaternion.identity);
                bool destroyObject = false;
                foreach(FaultyObject go in unfixedObjects)
                {
                    if(spawnedObject.GetComponent<Collider>().bounds.Intersects(go.GetComponent<Collider>().bounds))
                    {
                        destroyObject = true;
                        break;
                    }
                }

                foreach(GameObject impassable in impassableWalls)
                {
                    if (destroyObject)
                        break;

                    if (spawnedObject.GetComponent<Collider>().bounds.Intersects(impassable.GetComponent<Collider>().bounds))
                    {
                        destroyObject = true;
                        break;
                    }
                }

                if (destroyObject)
                {
                    Destroy(spawnedObject.gameObject);
                    i--;
                    listIndex--;
                    continue;
                }

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
