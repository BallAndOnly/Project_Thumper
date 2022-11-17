using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool onGround;

    public Transform orientation;

    float hInput;
    float vInput;

    Vector3 moveDirection;

    Rigidbody playerRigid;

    private void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
        playerRigid.freezeRotation = true;
        playerRigid.drag = groundDrag;
    }

    private void Update()
    {
        onGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        PlayerInput();
        SpeedLimit();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void PlayerInput()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * vInput + orientation.right * hInput;

        playerRigid.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedLimit()
    {
        Vector3 flatvel = new Vector3(playerRigid.velocity.x, 0f, playerRigid.velocity.z);

        if (flatvel.magnitude > moveSpeed) 
        {
            Vector3 limitedvel = flatvel.normalized * moveSpeed;
            playerRigid.velocity = new Vector3(limitedvel.x, playerRigid.velocity.y, limitedvel.z);
        }
    }
}
