using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float groundDrag;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;
    private float crouchClamp;

    [Header("Keybinds")]
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool onGround;

    public Transform orientation;

    float hInput;
    float vInput;

    Vector3 moveDirection;

    Rigidbody playerRigid;

    public MovementState state;
    public enum MovementState
    { 
        walking,
        crouching,
        sprint
    }

    private void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
        playerRigid.freezeRotation = true;
        playerRigid.drag = groundDrag;
        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        onGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        crouchClamp = Mathf.Clamp(crouchClamp, crouchYScale, startYScale);

        PlayerInput();
        SpeedLimit();
        StateHandler();
        Crouching();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void PlayerInput()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");        

        if (Input.GetKey(crouchKey))
        {           
            crouchClamp -= 0.01f;
        }
        else crouchClamp += 0.01f;
    }

    private void StateHandler()
    {
        
        if (Input.GetKey(sprintKey) & !Input.GetKey(crouchKey))
        {
            state = MovementState.sprint;
            moveSpeed = sprintSpeed;
        }
        else if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }
        else 
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * vInput + orientation.right * hInput;

        playerRigid.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void Crouching()
    {
        transform.localScale = new Vector3(transform.localScale.x, crouchClamp, transform.localScale.z);
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
