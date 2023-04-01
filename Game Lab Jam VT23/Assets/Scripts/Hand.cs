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

    Vector3 currentDirection;

    void Start()
    {
        currentDirection = new Vector3(0, 1, 0);
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
        if(Input.GetKeyDown(upKey))
        {
            currentDirection = new Vector3(0, 1, 0);
        }
        else if (Input.GetKeyDown(downKey))
        {
            currentDirection = new Vector3(0, -1, 0);
        }

        else if(Input.GetKeyDown(rightKey))
        {
            currentDirection = new Vector3(1, 0, 0);
        }
        else if(Input.GetKeyDown(leftKey))
        {
            currentDirection = new Vector3(-1, 0, 0);
        }

        focusTransform.position += currentDirection * movementSpeed * Time.deltaTime;

        Vector3 lookDirection = (focusTransform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(lookDirection) * Quaternion.Euler(90, 0, 0);
    }
}
