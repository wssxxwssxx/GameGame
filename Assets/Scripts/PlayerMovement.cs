using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 6f;
    private float movementDirection;
    private float doorTravelYOffset = .5f;
    [SerializeField] private float sprintMoveSpeed = 7f;
    [SerializeField] private float crouchSpeed = 2.5f;

    [SerializeField] private bool sprintFlag;
    [SerializeField] private bool crouchFlag;
    [SerializeField] private bool grounded;
    [SerializeField] private bool onDoor;
    [SerializeField] private bool goingRight;
    [SerializeField] private bool goingLeft;


    [SerializeField] private Collider standingCollider;
    [SerializeField] private Rigidbody rb;

    private Vector3 doorCoor;
    private Vector3 mousePosition;

    // F - MeleeAttack
    // Shift - Sprint
    // Ctrl - Crouch
    // Space - Jump


    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody>();
    }


    private void Update()
    {
        goingLeft = !goingRight;

        if(rb.velocity.x > 0)
        {
            goingRight = true;

        } else if(rb.velocity.x < 0)
        {
            goingRight = false;
        }

        if(goingRight)
        {
            gameObject.transform.FindChild("KnifeAttackPoint").transform.position =
                new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
        } else if (goingLeft)
        {
            gameObject.transform.FindChild("KnifeAttackPoint").transform.position =
                new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z);

        }

        movementDirection = Input.GetAxis("Horizontal");
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


    }

    private void FixedUpdate()
    {

        
            if(!crouchFlag && sprintFlag)
            {
                MovePlayer(sprintMoveSpeed, movementDirection);
            } else if (grounded && crouchFlag)
            {
                MovePlayer(crouchSpeed, movementDirection);
            }
            else
                MovePlayer(moveSpeed, movementDirection);

    }


    private void MovePlayer(float moveSpeed, float direction)
    {
        rb.velocity = new Vector3(moveSpeed * direction, rb.velocity.y, 0f);

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


    // Get Mouse Position
    public Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }

    public Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }

    public Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }




}
