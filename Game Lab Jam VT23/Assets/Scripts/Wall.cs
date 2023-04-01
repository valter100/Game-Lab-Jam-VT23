using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] int nrOfObjects;
    [SerializeField] List<GameObject> objectToSpawn;
    [SerializeField] List<GameObject> unfixedObjects;
    Vector2 spawnBounds;

    [SerializeField] GameObject[] impassableObjects;
    // Start is called before the first frame update
    void Start()
    {
        impassableObjects = GameObject.FindGameObjectsWithTag("Impassable");

        int listIndex = 0;

        for (int i = 0; i < nrOfObjects; i++)
        {
            Vector3 spawnPoint = new Vector3(Random.Range(-GetComponent<Collider>().bounds.size.x / 2, GetComponent<Collider>().bounds.size.x/2), Random.Range(0, GetComponent<Collider>().bounds.size.y), transform.position.z -0.5f);
           
            GameObject spawnedObject = Instantiate(objectToSpawn[listIndex++], spawnPoint, Quaternion.identity);

            if(listIndex >= objectToSpawn.Count)
            {
                listIndex = 0;
            }

            unfixedObjects.Add(spawnedObject);

            spawnedObject.GetComponent<FaultyObject>().SetWall(this);
        }

        FindObjectOfType<GameManager>().UpdateObjectCount();
    }

    public void removeFixedObject(GameObject fixedObject)
    {
        unfixedObjects.Remove(fixedObject);
    }

    public GameObject[] GetImpassableObjects() => impassableObjects;
    public int UnfixedObjectCount() => unfixedObjects.Count;
}
