using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RobotController : MonoBehaviour
{
    private CharacterController rController;
    private Animator animController;
    [Range(0, 100)]
    public float baseSpeed = 3f;
    [Range(0, 100)]
    public float sprintSpeed = 5f;
    [Range(0, 100)]
    public float mouseSensitivity_Y = 10f;
    [Range(0, 1)]
    public float gravity = 0.5f;
    [Range(0, 10)]
    public float jumpForce = 0.6f;

    private Vector3 targetPostion;
    private float rotY = 0.0f;
    private float horizontalAxis = 0.0f;
    private float verticalAxis = 0.0f;
    private bool jump = false;
    private bool sprint = false;
    private Vector3 up = Vector3.zero;
    private Vector3 lastMove = Vector3.zero;
    [HideInInspector]
    public bool controlled = false;
    // Start is called before the first frame update
    void Start()
    {
        rController = GetComponent<CharacterController>();
        animController = GetComponent<Animator>();
        rotY = transform.localRotation.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(controlled)
        {
            //Get inputs
            if(Input.GetButtonDown("Fire1"))
            {   
                Cursor.lockState = CursorLockMode.Locked;
            }
            if(Input.GetButtonDown("Cancel"))
            {
                Cursor.lockState = CursorLockMode.None;
            }
            if(rController.isGrounded)
            {
                horizontalAxis += Input.GetAxis("Horizontal");
                verticalAxis += Input.GetAxis("Vertical");
                if(Input.GetButtonDown("Jump"))
                    jump = true;
                if(Input.GetButton("Sprint"))
                    sprint = true;
                else
                    sprint = false;
            }
            
            float mouseX = Input.GetAxis("Mouse X");
            rotY += mouseX * 10* mouseSensitivity_Y * Time.deltaTime;
        }
    }
    void LateUpdate()
    {
        if(controlled)
        {
            Vector3 forward = rController.transform.forward;
            Vector3 right = rController.transform.right;
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();
            
            if(rController.isGrounded)
                up.y = -1;
            else
                up.y -= gravity * Time.deltaTime;

            if(jump)
            {
                animController.SetTrigger("Jump");
                up.y = jumpForce;
                jump = false;
            }
            Vector3 move = forward * verticalAxis + right * horizontalAxis + up;
            if(verticalAxis>0)
            {
                if(sprint)
                {
                    animController.SetBool("Walk", false);
                    animController.SetBool("Run", true);
                }
                else
                {
                    animController.SetBool("Run", false);
                    animController.SetBool("Walk", true);
                }
                animController.SetBool("Idle", false);
            }
            else
            {
                animController.SetBool("Run", false);
                animController.SetBool("Walk", false);
                animController.SetBool("Idle", true);
            }

            if(!rController.isGrounded)
                move = new Vector3(lastMove.x, move.y, lastMove.z);
            else
                lastMove = move;
            if(sprint)
                rController.Move(move * Time.deltaTime * sprintSpeed);
            else
                rController.Move(move * Time.deltaTime * baseSpeed);

            Quaternion localRotation = Quaternion.Euler(0.0f, rotY, 0.0f);
            if(Cursor.lockState==CursorLockMode.Locked)
                rController.transform.rotation = localRotation;
            
            horizontalAxis = 0;
            verticalAxis = 0;
        }
    }   
}
