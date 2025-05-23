using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeeMovement : MonoBehaviour
{
    [SerializeField] GameObject[] patrolPoints;
    [SerializeField] float moveSpeed = 1f;
    public int patrolDest = 0;

    //private Vector3 lastPosition;

    //void Start()
    //{
    //    lastPosition = transform.position;
    //}

    void Update()
    {
        Vector3 targetPosition = patrolPoints[patrolDest].transform.position;

        // Flip sprite depending on direction
        //checks whether the targetPosition is to the right of the bee's current position along the X-axis.
        if (targetPosition.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (targetPosition.x < transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // Move towards the target
        //current, target
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        // Check if reached target
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            patrolDest++;
            if (patrolDest >= patrolPoints.Length)
            {
                patrolDest = 0;
            }
        }
    }
}

