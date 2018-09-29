using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject Char;       //Public variable to store a reference to the Char game object

    public bool facingRight = true;

    private Vector3 offset;         //Private variable to store the offset distance between the Char and camera

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the Char's position and camera's position.
        offset = transform.position - Char.transform.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the Char's, but offset by the calculated offset distance.

        float move = Input.GetAxis("Horizontal");

        if (move > 0 && facingRight)
            facingRight = !facingRight;
        else if (move < 0 && !facingRight)
            facingRight = !facingRight;

        if (!facingRight)
            transform.position = Char.transform.position + offset;
        else
            transform.position = new Vector3(Char.transform.position.x - offset.x, Char.transform.position.y + offset.y, Char.transform.position.z + offset.z);

    }
}
