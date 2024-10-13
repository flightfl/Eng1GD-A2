using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float boostModifier = 2f;
    Rigidbody2D rb;
    Vector2 inputDirection = Vector2.zero;

    // Dash
    [SerializeField] private int flashDistance = 5;

    // Double Jump
    private bool canJump = true;
    [SerializeField] private int jumpModifier = 5;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void OnMove(InputValue value)
    {
        Vector2 movementDir = value.Get<Vector2>();
        Debug.Log(movementDir);
        // rb.AddForce(movementDir * boostModifier, ForceMode2D.Impulse);
        inputDirection = movementDir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bounced");
        canJump = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        canJump = true; // every time leave the ground, can perform once double jump
    }

    private void Update()
    {
        rb.AddForce(inputDirection * boostModifier);
    }

    void OnFlash(InputValue value)
    {

        // Calculate the dash end position
        Vector2 dashEndPosition = rb.position + (inputDirection * flashDistance);

        // Move the ball to the dash end position
        rb.MovePosition(dashEndPosition);
    }

    void OnDoubleJump(InputValue value)
    {
        if (canJump)
        {
            rb.AddForce(Vector2.up * jumpModifier, ForceMode2D.Impulse);
            canJump = false; // once jumped, no second jump
        }
    }

}
