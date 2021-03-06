﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float sprintMoveSpeed = 7f;
    [SerializeField] private float crouchSpeed = 2.5f;
    [SerializeField] private Collider standingCollider;
    [SerializeField] private bool sprintFlag;
    [SerializeField] private bool crouchFlag;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private bool grounded;
    [SerializeField] private bool onDoor;
    [SerializeField] private FieldOfView fieldOfView;

    private Vector3 doorCoor;
    private Vector3 mousePosition;
    private float doorTravelYOffset = .5f;

    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody>();
    }


    private void Update()
    {
        mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 mouseDirection = new Vector3(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y,
            0f
            );

        grounded = transform.Find("GroundCheck").GetComponent<GroundCheck>().isGrounded;

        #region Flags
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
        #endregion

        if(onDoor && Input.GetKeyDown(KeyCode.E))
        {
            MoveBetweenDoors(doorCoor);
        }



        fieldOfView.SetOrigin(transform.position);
        fieldOfView.SetDirection(mouseDirection);

    }

    private void FixedUpdate()
    {

        if(Input.GetKey(KeyCode.D))
        {
            if(!crouchFlag && sprintFlag)
            {
                MovePlayer(sprintMoveSpeed);
            } else if (grounded && crouchFlag)
            {
                MovePlayer(crouchSpeed);
            }
            else
                MovePlayer(moveSpeed);



        } else if (Input.GetKey(KeyCode.A))
        {
            if (!crouchFlag && sprintFlag)
            {
                MovePlayer(-sprintMoveSpeed);
            }
            else if (grounded && crouchFlag)
            {
                MovePlayer(-crouchSpeed);
            }
            else
                MovePlayer(-moveSpeed);
        } else
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);

  
    }

    private void MovePlayer(float moveSpeed)
    {
        rb.velocity = new Vector3(moveSpeed, rb.velocity.y, 0f);

    }

    private void Jump(Rigidbody rb, float jumpForce)
    {
        rb.velocity = Vector2.up * jumpForce;
    }

    private void MoveBetweenDoors(Vector3 position)
    {
        if(grounded)
        transform.position = new Vector3(position.x, position.y - doorTravelYOffset, 0f);
    }


    private void OnTriggerExit(Collider collision)
    {
        if (collision.transform.tag == "Door")
        {
            onDoor = false;
            doorCoor = Vector3.zero;
        }
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.transform.tag == "Door")
        {
            onDoor = true;
            doorCoor = collision.gameObject.GetComponent<FloorDoor>().pairDoor.transform.position;
        }
    }


}
