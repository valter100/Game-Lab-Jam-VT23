using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaultyObject : MonoBehaviour
{
    [SerializeField] Wall wall;
    bool objectFixed;

    public delegate void ObjectFixed();
    public static ObjectFixed onObjectFixed;
    public bool Fixed() => objectFixed;

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
    }
}
