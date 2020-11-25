using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private LayerMask groundLayerMask;
    private Rigidbody2D rb;


    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float movementDirection = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movementDirection, 0, 0) * Time.deltaTime * moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space) && transform.Find("GroundCheck").GetComponent<GroundCheck>().isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    

}
