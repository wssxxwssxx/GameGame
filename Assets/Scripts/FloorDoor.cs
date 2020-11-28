using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDoor : MonoBehaviour
{
    public GameObject pairDoor;
    private MeshRenderer rend;
    [SerializeField] private Material colorToTurnTo;
    [SerializeField] private Material defaultColor;
 

    private void Start()
    {
        rend = GetComponent<MeshRenderer>();
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            rend.material = colorToTurnTo;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            rend.material = defaultColor;
        }
    }


}
