using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    Transform boundaryHolder;

    Boundary playerBoundary;

    Vector2 startingPosition;

    public GameObject AiPlayer;

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {

        Vector3 pos = Camera.main.ScreenToWorldPoint(eventData.position);
        pos.z = 0;

        Vector2 clampedMousePos = new Vector2(Mathf.Clamp(pos.x, playerBoundary.Left,
                                                                 playerBoundary.Right),
                                              Mathf.Clamp(pos.y, playerBoundary.Down,
                                                                 playerBoundary.Up));

            rb.MovePosition(clampedMousePos);
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    void Start()
    {

        startingPosition = rb.position;

        playerBoundary = new Boundary(boundaryHolder.GetChild(0).position.y, 
                                      boundaryHolder.GetChild(1).position.y, 
                                      boundaryHolder.GetChild(2).position.x, 
                                      boundaryHolder.GetChild(3).position.x);

    }

    public void ResetPosition()
    {
           
        rb.position = startingPosition;

    }

}
