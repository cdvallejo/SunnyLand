using System;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [Range(-10,10)] public float cameraZoom;
    public Transform playerPosition;


    private void Update()
    {
        if (playerPosition != null)
        {
            // Set the position of the camera to the player's position
            //transform.position = new Vector3(playerPosition.position.x, playerPosition.position.y, transform.position.z);
            transform.position = playerPosition.position + new Vector3(0, 0, cameraZoom);
        }
    }
}
