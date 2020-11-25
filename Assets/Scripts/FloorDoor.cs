using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDoor : MonoBehaviour
{
    public GameObject pairDoor;
    private SpriteRenderer rend;
    private Color colorToTurnTo = Color.green;
    private Color defaultColor = Color.yellow;


    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            rend.color = colorToTurnTo;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            rend.color = defaultColor;
        }
    }


}
