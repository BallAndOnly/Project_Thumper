using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("Sensitivity")]
    public float sensX;
    public float sensY;

    float xRotation;
    float yRotation;

    [Header("Ref thing")]
    public Transform orientation;
    public PlayerController playerController;

    public Transform CamPos;

    private void Start()
    {
        //Make cursor gone
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        transform.position = CamPos.position;

        //Look around
        float mouseX = Input.GetAxis("Mouse Y") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxis("Mouse X") * Time.deltaTime * sensY;

        xRotation -= mouseX;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //Cam clamp to make sure you don't spin 360 vertically
        yRotation += mouseY;

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);       
    }

}
