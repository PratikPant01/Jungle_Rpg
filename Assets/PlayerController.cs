using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float movespeed = 5f;

    public float collisionOffset = 0.1f;

    public ContactFilter2D movementFilter;

    Vector2 movementInput;
    Rigidbody2D rb;

    Animator animator;


    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            bool sucess=TryMove(movementInput);
            if(!sucess)
            {
                sucess=TryMove(new Vector2(movementInput.x, 0));
            
            if (!sucess)
            {
                TryMove(new Vector2(0, movementInput.y));
           } 
          }

            animator.SetBool("isMoving", sucess);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(movementInput, //x and y values between -1 and 1 that represent the direction
                movementFilter,//the setting that determine where a collsion can occur
                castCollisions,//the list of collisions that are detected
                movespeed * Time.fixedDeltaTime + collisionOffset);//AMOUNT OF DISTANCE TO CAST
        if (count == 0)
        {
            rb.MovePosition(rb.position + movementInput * movespeed * Time.fixedDeltaTime);
            return true;

        }
        else
        {
            return false;
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
    //private void OnAnimatorMove()
    //{
        
    //}
}
