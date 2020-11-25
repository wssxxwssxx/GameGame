using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float cameraMovementSpeed = 10f;
    [SerializeField] private float cameraOffsetZ = -10f;
    [SerializeField] private float cameraOffsetY = 2f;
    [SerializeField] private GameObject player;
    private float playerPosX;
    private float playerPosY;




    private void Update()
    {
        playerPosX = player.transform.position.x;
        playerPosY = player.transform.position.y;

        transform.position = new Vector3(playerPosX, playerPosY + cameraOffsetY, cameraOffsetZ);

    }


}
