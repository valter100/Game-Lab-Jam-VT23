using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform focusTransform;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Wall wall;

    [Header("Input")]
    [SerializeField] KeyCode upKey;
    [SerializeField] KeyCode downKey;
    [SerializeField] KeyCode rightKey;
    [SerializeField] KeyCode leftKey;

    [Header("Values")]
    [SerializeField] float movementSpeed;
    [SerializeField] float timeBetweenProjectile;
    [SerializeField] bool activeOnLevel;
    float timeSinceLastProjectile;

    Vector3 currentDirection;

    void Start()
    {
        currentDirection = new Vector3(0, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.GameStarted || !activeOnLevel)
            return;

        Move();

        timeSinceLastProjectile += Time.deltaTime;

        if (timeSinceLastProjectile >= timeBetweenProjectile)
        {
            timeSinceLastProjectile = 0;
            Instantiate(projectile, spawnPoint.transform.position, transform.rotation);
        }
    }

    public void Move()
    {
        if (Input.GetKeyDown(upKey))
        {
            if (currentDirection == new Vector3(0, 1, 0))
                currentDirection = new Vector3(0, 1.5f, 0);
            else
                currentDirection = new Vector3(0, 1, 0);
        }
        else if (Input.GetKeyDown(downKey))
        {
            if (currentDirection == new Vector3(0, -1, 0))
                currentDirection = new Vector3(0, -1.5f, 0);
            else
                currentDirection = new Vector3(0, -1, 0);
        }

        else if (Input.GetKeyDown(rightKey))
        {
            if (currentDirection == new Vector3(1, 0, 0))
                currentDirection = new Vector3(1.5f, 0, 0);
            else
                currentDirection = new Vector3(1, 0, 0);
        }
        else if (Input.GetKeyDown(leftKey))
        {
            if (currentDirection == new Vector3(-1, 0, 0))
                currentDirection = new Vector3(-1.5f, 0, 0);
            else
                currentDirection = new Vector3(-1, 0, 0);
        }

        focusTransform.position += currentDirection * movementSpeed * Time.deltaTime;

        focusTransform.position = new Vector3(Mathf.Clamp(focusTransform.position.x, -wall.GetComponent<Collider>().bounds.size.x / 2, wall.GetComponent<Collider>().bounds.size.x / 2), Mathf.Clamp(focusTransform.position.y, 0, wall.GetComponent<Collider>().bounds.size.y), wall.transform.position.z);

        Vector3 lookDirection = (focusTransform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(lookDirection) * Quaternion.Euler(90, 0, 0);
    }
}
