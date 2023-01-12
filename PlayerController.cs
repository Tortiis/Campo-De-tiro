using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Reference")]
    public Camera playercamera;   

    [Header ("General")]
    public float gravity = -20;
    
    [Header ("Movement")]
    public float WalkSpeed = 5f;
   
   [Header ("Rotation")]
   public float rotationSensibility = 10f;
   
    [Header ("Jump")]
     public float jumpHeight = 1.9f;

    private float CameraVerticalAngle;
    Vector3 moveInput = Vector3.zero;
    Vector3 rotationInput = Vector3.zero;
    CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>(); 
    }

    private void Update()
    {
      Look();
      Move();
    }

    private void Move()
    {
        

        if (characterController.isGrounded)
        {
            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveInput = transform.TransformDirection(moveInput);
              moveInput *= WalkSpeed;
            if (Input.GetButtonDown("Jump"))
            {
                moveInput.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
     moveInput.y += gravity * Time.deltaTime;
        characterController.Move(moveInput * Time.deltaTime);
    }

    private void Look()
    {
        rotationInput.x = Input.GetAxis("Mouse X") * rotationSensibility * Time.deltaTime;
        rotationInput.y = Input.GetAxis("Mouse Y") * rotationSensibility * Time.deltaTime;

        CameraVerticalAngle = CameraVerticalAngle + rotationInput.y;
        CameraVerticalAngle = Mathf.Clamp(CameraVerticalAngle, -70, 70);

        transform.Rotate(Vector3.up * rotationInput.x);
        playercamera.transform.localRotation = Quaternion.Euler(CameraVerticalAngle,0f , 0f);
    }
}
