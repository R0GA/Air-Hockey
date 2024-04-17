using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AiScript : MonoBehaviour
{

    [SerializeField]
    float maxMovementSpeed;

    [SerializeField]
    Rigidbody2D rb;

    Vector2 startingPosition;

    [SerializeField]
    Rigidbody2D puck;

    [SerializeField]
    Transform PlayerBoundaryHolder;

    Boundary playerBoundary;

    [SerializeField]
    Transform PuckBoundaryHolder;

    Boundary puckBoundary;

    Vector2 targetPosition;

    bool isFirstTimeInOpponentsHalf = true;

    float offsetYFromTarget;

    private void Start()
    {
        startingPosition = rb.position;

        playerBoundary = new Boundary(PlayerBoundaryHolder.GetChild(0).position.y,
                                      PlayerBoundaryHolder.GetChild(1).position.y,
                                      PlayerBoundaryHolder.GetChild(2).position.x,
                                      PlayerBoundaryHolder.GetChild(3).position.x);

        puckBoundary = new Boundary(PuckBoundaryHolder.GetChild(0).position.y,
                                    PuckBoundaryHolder.GetChild(1).position.y,
                                    PuckBoundaryHolder.GetChild(2).position.x,
                                    PuckBoundaryHolder.GetChild(3).position.x);

    }

    private void FixedUpdate()
    {

        if (!PuckScript.WasGoal)
        {

            float movementSpeed;

            if (puck.position.x < puckBoundary.Left)
            {

                if (isFirstTimeInOpponentsHalf)
                {
                    isFirstTimeInOpponentsHalf = false;
                    offsetYFromTarget = Random.Range(-1f, 1f);
                }

                movementSpeed = maxMovementSpeed * Random.Range(0.1f, 0.3f);

                targetPosition = new Vector2(startingPosition.x, Mathf.Clamp(puck.position.y + offsetYFromTarget, playerBoundary.Down, playerBoundary.Up));

            }
            else
            {

                isFirstTimeInOpponentsHalf = true;

                movementSpeed = Random.Range(maxMovementSpeed * 0.4f, maxMovementSpeed);

                targetPosition = new Vector2(Mathf.Clamp(puck.position.x, playerBoundary.Left, playerBoundary.Right), Mathf.Clamp(puck.position.y, playerBoundary.Down, playerBoundary.Up));

            }

            rb.MovePosition(Vector2.MoveTowards(rb.position, targetPosition, movementSpeed * Time.fixedDeltaTime));

        }
    }

    public void ResetPosition()
    {

        rb.position = startingPosition;

    }

}
