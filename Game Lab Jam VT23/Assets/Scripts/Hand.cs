using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform focusTransform;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform spawnPoint;

    [Header("Input")]
    [SerializeField] KeyCode upKey;
    [SerializeField] KeyCode downKey;
    [SerializeField] KeyCode rightKey;
    [SerializeField] KeyCode leftKey;

    [Header("Values")]
    [SerializeField] float movementSpeed;
    [SerializeField] float timeBetweenProjectile;
    float timeSinceLastProjectile;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        timeSinceLastProjectile += Time.deltaTime;

        if(timeSinceLastProjectile >= timeBetweenProjectile)
        {
            timeSinceLastProjectile = 0;
            Instantiate(projectile, spawnPoint.transform.position, transform.rotation);
        }
    }

    public void Move()
    {
        if(Input.GetKey(upKey))
        {
            focusTransform.position += new Vector3(0, 1, 0) * movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(downKey))
        {
            focusTransform.position += new Vector3(0, -1, 0) * movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(rightKey))
        {
            focusTransform.position += new Vector3(1, 0, 0) * movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(leftKey))
        {
            focusTransform.position += new Vector3(-1, 0, 0) * movementSpeed * Time.deltaTime;
        }

        Vector3 lookDirection = (focusTransform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(lookDirection) * Quaternion.Euler(90, 0, 0);
    }
}
