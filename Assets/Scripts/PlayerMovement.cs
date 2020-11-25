using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float sprintMoveSpeed = 7f;
    [SerializeField] private float crouchSpeed = 2.5f;
    [SerializeField] private Collider2D standingCollider;

    private bool sprintFlag;
    private bool crouchFlag;
    private Rigidbody2D rb;
    private bool grounded;

    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        grounded = transform.Find("GroundCheck").GetComponent<GroundCheck>().isGrounded;

        float movementDirection = Input.GetAxis("Horizontal");

        #region SprintFlag

        if (Input.GetKey(KeyCode.LeftShift) && grounded)
        {
            sprintFlag = true;
        } else
        {
            sprintFlag = false;
        }

        #endregion

        #region Jump
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jump(rb, jumpForce);
        }
        #endregion

        #region Crouch
        if (grounded && Input.GetKey(KeyCode.LeftControl))
        {
            crouchFlag = true;

        } else
        {
            crouchFlag = false;
        }
        standingCollider.enabled = !crouchFlag;
        #endregion


        if (!sprintFlag && !crouchFlag) 
            MovePlayer(movementDirection, moveSpeed);

       if (sprintFlag && !crouchFlag)
            MovePlayer(movementDirection, sprintMoveSpeed);

       if (crouchFlag)
            MovePlayer(movementDirection, crouchSpeed);
    }

    private void MovePlayer(float movementDirection, float moveSpeed)
    {

        transform.position += new Vector3(movementDirection, 0, 0) * Time.deltaTime * moveSpeed;

    }

    private void Jump(Rigidbody2D rb, float jumpForce)
    {
        rb.velocity = Vector2.up * jumpForce;
    }





}
