using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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

    [Header("FlashLight")]
    public Transform flashLight;
    private bool flashLightState;

    [Header("Keybinds")]
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public KeyCode flashLightToggle = KeyCode.F;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool onGround;

    [Header("Interactions")]
    public Transform playerCamera;
    public float interactRange;
    public ActionUIController ActionUIComponent;

    [Header("UI")]
    public Image sprintBar;

    [Header("Refs")]
    public Transform orientation;

    float hInput;
    float vInput;

    public Vector3 moveDirection;

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

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, playerCamera.TransformDirection(Vector3.forward), out hit, interactRange))
        { 
            Debug.DrawRay(playerCamera.position, playerCamera.TransformDirection(Vector3.forward).normalized * interactRange, Color.green);
            if (hit.collider.tag == "Interactable")
            {
                IntInteractable interactable = hit.collider.GetComponent<IntInteractable>();
                ActionUIComponent.ShowActionText(interactable.ShowActionText());
                ActionUIComponent.HandPos(interactable.HandPos());
                ActionUIComponent.ShowHand(interactable.ShowHand());

                if (Input.GetKeyDown(KeyCode.E)) interactable.Interact();
            }
            else
            {
                ActionUIComponent.ShowCanvas(false);
            }
        }
        else 
        { 
            Debug.DrawRay(playerCamera.position, playerCamera.TransformDirection(Vector3.forward).normalized * interactRange, Color.yellow);
            ActionUIComponent.ShowCanvas(false);
        }

        PlayerInput();
        SpeedLimit();
        StateHandler();
        Crouching();
        FlashLight();

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

    private void FlashLight()
    {
        if (Input.GetKeyDown(flashLightToggle)) 
        {
            flashLightState = !flashLightState;
            flashLight.GetComponent<Light>().enabled = flashLightState;
        }
    }
}
