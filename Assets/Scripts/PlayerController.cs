using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 desiredMove;
    [SerializeField] private float speed;

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

        desiredMove = context.ReadValue<Vector2>();
        Debug.Log(desiredMove);
        if (Mathf.Abs(desiredMove.x) > 0.1f && Mathf.Abs(desiredMove.y) >= 0.1f)
        {
            Debug.Log("DIAGONAL");
            //desiredMove = new Vector2(desiredMove.x * 1.7f, desiredMove.y);
            desiredMove = new Vector2(desiredMove.x* 1.4f, desiredMove.y * 0.80f);
        }
            
        desiredMove *= Time.fixedDeltaTime * speed;
        Debug.Log(desiredMove);
    }


    private void Update()
    {
        
        

    }

    private void FixedUpdate()
    {
        rb.MovePosition(new Vector2(transform.position.x + desiredMove.x, transform.position.y + desiredMove.y));


    }

}
