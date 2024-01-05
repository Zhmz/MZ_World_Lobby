using System.Collections;
using UnityEngine;


public class CameraControl : MonoBehaviour
{
    public bool moveCamera = true;                      // Whether the camera should be moved by this script.    
    public float smoothing = 7f;                        // Smoothing applied during Slerp, higher is smoother but slower.
    public Vector3 offset = new Vector3(0f, 1.5f, 0f); // The offset from the player's position that the camera aims at.
    public Transform playerPosition;                    // Reference to the player's Transform to aim at.


    private IEnumerator Start()
    {
        playerPosition = GameObject.FindWithTag("Player")?.transform;
        // If the camera shouldn't move, do nothing.
        if (!moveCamera || playerPosition == null)
            yield break;

        // Wait a single frame to ensure all other Starts are called first.
        yield return null;

        // Set the rotation of the camera to look at the player's position with a given offset.
        //transform.rotation = Quaternion.LookRotation(playerPosition.position - transform.position + offset);



        transform.position = playerPosition.position + offset;



    }


    // LateUpdate is used so that all position updates have happened before the camera aims.
    private void LateUpdate()
    {


        playerPosition = GameObject.FindWithTag("Player")?.transform;

        // If the camer shouldn't move, do nothing.
        if (!moveCamera || playerPosition == null)
            return;

        // Find a new rotation aimed at the player's position with a given offset.
        //Quaternion newRotation = Quaternion.LookRotation (playerPosition.position - transform.position + offset);

        //// Spherically interpolate between the camera's current rotation and the new rotation.
        //transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime * smoothing);

        transform.position = playerPosition.position + offset;
    }
}
