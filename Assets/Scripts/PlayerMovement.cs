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

    [SerializeField] private bool sprintFlag;
    [SerializeField] private bool crouchFlag;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool grounded;
    [SerializeField] private bool onDoor;
    [SerializeField] private Vector3 doorCoor;

    private float doorTravelYOffset = .5f;

    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

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

    private void Jump(Rigidbody2D rb, float jumpForce)
    {
        rb.velocity = Vector2.up * jumpForce;
    }

    private void MoveBetweenDoors(Vector3 position)
    {
        if(grounded)
        transform.position = new Vector3(position.x, position.y - doorTravelYOffset, 0f);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Door")
        {
            onDoor = false;
            doorCoor = Vector3.zero;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Door")
        {
            onDoor = true;
            doorCoor = collision.gameObject.GetComponent<FloorDoor>().pairDoor.transform.position;
        }
    }



}
