using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 desiredMove;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        

    }

    public void PrintStuff()
    {
        Debug.Log("PRASDFADS");
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log(desiredMove);
        desiredMove = context.ReadValue<Vector2>();
        
    }


    private void Update()
    {
        
        

    }

    private void FixedUpdate()
    {
        rb.MovePosition(new Vector2(transform.position.x + desiredMove.x, transform.position.y + desiredMove.y));


    }

}
