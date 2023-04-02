using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaultyObject : MonoBehaviour
{
    Wall wall;
    bool objectFixed;
    

    public delegate void ObjectFixed();
    public static ObjectFixed onObjectFixed;
    public bool Fixed() => objectFixed;

    private void Start()
    {
        wall = FindObjectOfType<Wall>();
    }

    public void SetWall(Wall newWall)
    {
        wall = newWall;
    }
    public void Fix()
    {
        GetComponentInChildren<ParticleSystem>().gameObject.SetActive(false);
        objectFixed = true;
        wall.removeFixedObject(gameObject);
        onObjectFixed.Invoke();
        GetComponent<Animator>().Play("Fix");
    }
}
