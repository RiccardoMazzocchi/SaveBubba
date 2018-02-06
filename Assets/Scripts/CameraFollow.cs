using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    Transform target;

    public float horizontalOffset;
    public float verticalOffset;
    public float smoothingY, smoothingX;
    public float diagonalSmoothingY, diagonalSmoothingX;
    float zOffset = 30f;

    Vector3 targetPos;

    PlayerController playerScript;

	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = FindObjectOfType<PlayerController>();
        diagonalSmoothingY = smoothingY * 0.5f;
        diagonalSmoothingX = smoothingX * 0.5f;

        Invoke("FindPlayer", 1f);

    }

    void FindPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

	// Update is called once per frame
	void Update () {

        //sets the target position
        targetPos = new Vector3(target.position.x, target.position.y, target.position.z - zOffset);


        //if the player is moving right, camera offsets slightly to the left
        if (playerScript.moveH > 0)
        {
            targetPos = new Vector3(targetPos.x + horizontalOffset, targetPos.y, targetPos.z - zOffset);
        }
        //else if the player is moving left, camera offsets slightly to the right
        else if (playerScript.moveH < 0)
        {
            targetPos = new Vector3(targetPos.x - horizontalOffset, targetPos.y, targetPos.z - zOffset);
        }
        
        //if the player is moving up, camera offsets slightly to the bottom
        if (playerScript.moveV > 0)
        {
            targetPos = new Vector3(targetPos.x, targetPos.y + verticalOffset, targetPos.z - zOffset);
        }
        //else if the player is moving down, camera offsets slightly to the top
        else if (playerScript.moveV < 0)
        {
            targetPos = new Vector3(targetPos.x, targetPos.y - verticalOffset, targetPos.z - zOffset);
        }


        //if the player is moving either left or right, smooths the movement of the camera 
        if (Mathf.Abs(playerScript.moveH) > 0)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothingX * Time.deltaTime);
        }

        //if the player is moving up or down, smooths the movement of the camera
        if (Mathf.Abs(playerScript.moveV) > 0)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothingY * Time.deltaTime);
        }

        if (playerScript.diagonalMove)
        {
            smoothingX = diagonalSmoothingX;
            smoothingY = diagonalSmoothingY;
        }
        else
        {
            smoothingX = diagonalSmoothingX * 2f;
            smoothingY = diagonalSmoothingY * 2f;
            playerScript.diagonalMove = false;
        }
        

        //if the player is not moving, resets camera position smoothly to the center
        if (playerScript.moveH == 0 && playerScript.moveV == 0)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, 1.75f * Time.deltaTime);
        }
	}
}
