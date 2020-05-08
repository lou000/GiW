using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class RobotRigidBodyController : MonoBehaviour
{
    [Range(0, 100)]
    public float baseSpeed = 50f;
    [Range(0,100)]
    public float maxSpeed = 10f;
    [Range(0, 100)]
    public float mouseSensitivity_Y = 15f;
    [Range(0, 10)]
    public float jumpForce = 0.4f;
    [Range(-2,2)]
    public float groundDistOffset = 0.98f;


    private Rigidbody rController;
    private Collider rCollider;
    private float rotY = 0.0f;
    private float moveHorizontal = 0.0f;
    private float moveVertical = 0.0f;
    private bool jump = false;
    private float distanceToGround;
    [HideInInspector]
    public bool controlled = false;
    private bool grounded = true;

    // Start is called before the first frame update
    void Start()
    {
        rController = GetComponent<Rigidbody>();
        rotY = transform.localRotation.eulerAngles.x;
        rCollider = GetComponent<Collider>();
        distanceToGround = rCollider.bounds.extents.y;

    }

    // Update is called once per frame
    void Update()
    {
                //Get inputs
        if(controlled)
        {
            if(Input.GetButtonDown("Fire1"))
                if(Cursor.lockState != CursorLockMode.Locked)
                    Cursor.lockState = CursorLockMode.Locked;
                else
                    Cursor.lockState = CursorLockMode.None;
            if((Cursor.lockState == CursorLockMode.Locked))
            {
                if(grounded)
                {
                    moveHorizontal += Input.GetAxis("Horizontal");
                    moveVertical += Input.GetAxis("Vertical");
                    if(Input.GetButtonDown("Jump"))
                        jump = true;
                }
                float mouseX = Input.GetAxis("Mouse X");
                rotY += mouseX * 10* mouseSensitivity_Y * Time.deltaTime;
            }

        }
    }
    void FixedUpdate() 
    {   if(controlled)
        {
            Quaternion localRotation = Quaternion.Euler(0.0f, rotY, 0.0f);
            rController.MoveRotation(localRotation);

            float up = 0.0f;
            if(jump)
            {
                up = jumpForce*10;
                jump = false;
            }
            
            Vector3 forward = rController.transform.forward;
            Vector3 right = rController.transform.right;
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();
            Vector3 move = forward * moveVertical + right * moveHorizontal + Vector3.up*up;
            rController.AddForce(move * baseSpeed);
            if(rController.velocity.magnitude>maxSpeed)
            {
                float x = rController.velocity.x;
                float z = rController.velocity.z;
                Vector3 horizontalVel = new Vector3(x,0,z).normalized * maxSpeed;
                rController.velocity = new Vector3(horizontalVel.x, rController.velocity.y, horizontalVel.z);
            }
            moveVertical = 0;
            moveHorizontal = 0;
            grounded = Grounded();
        }
    }

    bool Grounded()
    {
        return Physics.Raycast(rCollider.transform.position, -Vector3.up, distanceToGround-groundDistOffset);
    }
}
