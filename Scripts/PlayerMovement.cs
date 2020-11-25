using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float movementDirection = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movementDirection, 0, 0) * Time.deltaTime * moveSpeed;



    }

}
