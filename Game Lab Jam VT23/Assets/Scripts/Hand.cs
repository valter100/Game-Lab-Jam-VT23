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
    [SerializeField] LineRenderer lineRenderer;

    [Header("Input")]
    [SerializeField] KeyCode upKey;
    [SerializeField] KeyCode downKey;
    [SerializeField] KeyCode rightKey;
    [SerializeField] KeyCode leftKey;

    [Header("Values")]
    [SerializeField] float movementSpeed;
    [SerializeField] float timeBetweenProjectile;
    [SerializeField] bool activeOnLevel;
    [SerializeField] bool isBeam;
    //[SerializeField] float TimeBetweenBeamTrigger;
    [SerializeField] LayerMask impassableLayer;
    float timeSinceLastProjectile;
    //float timeSinceLastBeamTrigger;

    Vector3 currentDirection;
    Vector3 oldPosition;

    void Start()
    {
        currentDirection = new Vector3(0, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.GameStarted || !activeOnLevel)
            return;

        oldPosition = transform.position;

        Move();
        timeSinceLastProjectile += Time.deltaTime;

        if (isBeam)
        {
            Vector3[] positions = { transform.position, focusTransform.transform.position };

            for (int i = 0; i < positions.Length; i++)
            {
                lineRenderer.SetPosition(i, positions[i]);
            }

            if (timeSinceLastProjectile >= timeBetweenProjectile)
            {
                timeSinceLastProjectile = 0;
                Instantiate(projectile, focusTransform.transform.position, transform.rotation * Quaternion.Euler(0, 0, 180));
            }
        }
        else
        {
            if (timeSinceLastProjectile >= timeBetweenProjectile)
            {
                timeSinceLastProjectile = 0;
                Instantiate(projectile, spawnPoint.transform.position, transform.rotation * Quaternion.Euler(0, 0, 180));
            }
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

        if (CheckPassable())
        {
            Debug.Log("GO!");
            focusTransform.position += currentDirection * movementSpeed * Time.deltaTime;
        }
        else
        {
            Debug.Log("DONT GO");
            currentDirection = new Vector3(0, 0, 0);
        }

        focusTransform.position = new Vector3(Mathf.Clamp(focusTransform.position.x, -wall.GetComponent<Collider>().bounds.size.x / 2, wall.GetComponent<Collider>().bounds.size.x / 2), Mathf.Clamp(focusTransform.position.y, 0, wall.GetComponent<Collider>().bounds.size.y), wall.transform.position.z);

        focusTransform.position = new Vector3(
            focusTransform.transform.position.x,
            focusTransform.transform.position.y,
            wall.transform.position.z - wall.GetComponent<Collider>().bounds.size.z/2);

        Vector3 lookDirection = (focusTransform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(lookDirection) * Quaternion.Euler(90, 0, 180);
    }

    public bool CheckPassable()
    {
        Vector3 direction = ((focusTransform.position + currentDirection * movementSpeed * Time.deltaTime) - transform.position).normalized;

        if (Physics.Raycast(transform.position, direction, Mathf.Infinity, impassableLayer))
        {
            return false;
        }

        return true;
    }

    public void Disable()
    {
        activeOnLevel = false;
    }
}
