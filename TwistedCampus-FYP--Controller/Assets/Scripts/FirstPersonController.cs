using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public bool CanMove { get; private set; } = true;
    private bool IsSprinting => canSprint && Input.GetKey(sprintKey);
    private bool ShouldJump => Input.GetKeyDown(jumpKey) && characterController.isGrounded;

    [Header("Functional Options")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    [Header("Movement Parameters")]
    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float sprintSpeed = 10.0f;

    [Header("Mouse Look Parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float upperlookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float lowerlookLimit = 80.0f;

    [Header("Mouse Look Parameters")]
    [SerializeField] private float jumpForce = 10.0f;
    [SerializeField] private float gravity = 24.0f;

    private Camera playerCamera;
    private CharacterController characterController;

    private Vector3 movDirection;
    private Vector2 currentInput;

    private float rotationX = 0;

    //Animation Components
    //[SerializeField]private Animator characterAnimator;           
    //[SerializeField]private string walkAnimationName = "Walking";
    //[SerializeField]private string idleAnimationName = "Idle";

    //private bool isWalking = false;                                                          
                
    void Awake()                                                                                                                                                                                     
    {
        playerCamera = GetComponentInChildren<Camera>();    
        characterController = GetComponent<CharacterController>();                                                                                                               
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;        
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            HandleMovementInput();
            HandleMouseLook();

            if(canJump)
                HandleJump();

            ApplyFinalMovements();

            //UpdateAnimations();
        }
    }

    private void HandleMovementInput()
    {
        currentInput = new Vector2((IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Vertical"), (IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Horizontal"));

        float movDirectionY = movDirection.y;
        movDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
        movDirection.y = movDirectionY;
    }

    private void HandleMouseLook()
    {
        //Up and Down Mouse Look
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upperlookLimit, lowerlookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        //Left and Right Mouse Look
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    }

    private void HandleJump()
    {
        if (ShouldJump)
            movDirection.y = jumpForce;
    }

    private void ApplyFinalMovements()
    {
        if (!characterController.isGrounded)
            movDirection.y -= gravity * Time.deltaTime;

        characterController.Move(movDirection * Time.deltaTime);
    }

    //private void UpdateAnimations()
    //{
    //    if (currentInput.magnitude > 0)
    //    {
    //        // Player is moving, play walk animation
    //        if (!isWalking)
    //        {
    //            characterAnimator.Play(walkAnimationName);
    //            isWalking = true;
    //        }
    //    }
    //    else
    //    {
    //        // Player is not moving, play idle animation
    //        if (isWalking)
    //        {
    //            characterAnimator.Play(idleAnimationName);
    //            isWalking = false;
    //        }
    //    }
    //}
}
