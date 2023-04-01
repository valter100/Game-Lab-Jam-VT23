using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalProjectile : MonoBehaviour
{
    [SerializeField] GameObject poolObject;
    [SerializeField] float movementSpeed;
    [SerializeField] ParticleSystem SplashEffect;

    [SerializeField] List<string> oddTags;
    [SerializeField] string breakTag;
    [SerializeField] string fixTag;

    private void Update()
    {
        transform.position += transform.up * movementSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach(string tag in oddTags)
        {
            if (other.gameObject.tag == tag )
            {
                FindObjectOfType<GameManager>().GameOver();
                Destroy(gameObject);
            }
        }

        if (other.gameObject.tag == fixTag)
        {
            FaultyObject fixObject = other.GetComponent<FaultyObject>();
            if (!fixObject.Fixed())
                fixObject.Fix();
            Destroy(gameObject);
        }

        //if(other.gameObject.tag == breakTag)
        //{
        //    Vector3 spawnLocation = new Vector3(transform.position.x, transform.position.y, other.transform.position.z);

        //    GameObject pool = Instantiate(poolObject, spawnLocation, Quaternion.Euler(90, 0, 0));
        //    pool.transform.parent = GameObject.Find("Pools").transform;

        //    if (pool.transform.position == other.transform.position)
        //    {
        //        Debug.Log("Break Wall!");
        //        Destroy(pool.gameObject);
        //        Destroy(other.gameObject);
        //        Destroy(gameObject);
        //    }
        //}

        if (other.gameObject.tag == "Wall")
        {
            Vector3 spawnLocation = new Vector3(transform.position.x, transform.position.y, other.transform.position.z - 0.5f);

            GameObject pool = Instantiate(poolObject, spawnLocation, Quaternion.Euler(90, 0, 0));
            Instantiate(SplashEffect, spawnLocation, Quaternion.Euler(180, 0, 0));
            pool.transform.parent = GameObject.Find("Pools").transform;
            Destroy(gameObject);
        }
    }
}
