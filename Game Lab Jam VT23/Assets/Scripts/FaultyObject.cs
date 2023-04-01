using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaultyObject : MonoBehaviour
{
    bool objectFixed;

    public bool Fixed() => objectFixed;

    public void Fix()
    {
        GetComponentInChildren<ParticleSystem>().gameObject.SetActive(false);
    }
}
