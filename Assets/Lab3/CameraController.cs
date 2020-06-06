using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject [] targets;
    public GameObject currentTarget;
    private CharacterController targetCon;
    private Rigidbody targetRB;
    private bool rb = false;

    [Range(0, 100)]
    public float mouseSensitivity_Y=10f;
    [Range(0, 100)]
    public float clampAngle = 80.0f;
    [Range(0.001f,1)]
    public float smooth_time = 0.03f;
    [Range(0,3)]
    public float followDistance = 1f;
    [Range(0,5)]
    public float cameraHeight = 2f;
    [Range(0,3)]
    public float cameraLROffset = 0f;

    private float rotX = 0.0f;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 localRot = transform.localRotation.eulerAngles;
        rotX = localRot.y;
        if(targets.Length>0)
        {
            currentTarget = targets[0];
            InitTarget();
        }
    }

    void InitTarget()
    {
        if(currentTarget.TryGetComponent<CharacterController>(out targetCon))
        {
            rb = false;
            targetCon.GetComponent<RobotController>().controlled = true;
            targetCon.tag = "Player";
        }
        else if(currentTarget.TryGetComponent<Rigidbody>(out targetRB))
        {
            rb = true;
            targetRB.GetComponent<RobotRigidBodyController>().controlled = true;
            targetRB.tag = "Player";
        }
    }

    void DropControl()
    {
        if(rb)
        {
            targetRB.GetComponent<RobotRigidBodyController>().controlled = false;
            targetRB.tag = "Untagged";
        }
        else
        {
            targetCon.GetComponent<RobotController>().controlled = false;
            targetCon.tag = "Untagged";
        }
    }

    // Update is called once per frame
    void Update()
    {  
        float mouseY = Input.GetAxis("Mouse Y");
        rotX -= mouseY * 10 * mouseSensitivity_Y * Time.deltaTime;
        rotX = Mathf.Clamp(rotX, -90f, 90f);
        if(followDistance<0.1f)
            this.GetComponent<Camera>().nearClipPlane = 1.5f;
        else
            this.GetComponent<Camera>().nearClipPlane = 0.3f;
        
        if(Input.GetButtonDown("1"))
        {
            if(targets.Length>0)
            {
                DropControl();
                currentTarget = targets[0];
                InitTarget();
            }
        }
        if(Input.GetButtonDown("2"))
        {
            if(targets.Length>1)
            {
                DropControl();
                currentTarget = targets[1];
                InitTarget();
            }
        }
    }
    void FixedUpdate() 
    {
        //Camera position to target position
        Transform targetTransform;
        if(rb)
            targetTransform = targetRB.transform;
        else
            targetTransform = targetCon.transform;

        Vector3 targetPos = new Vector3(targetTransform.position.x, targetTransform.position.y,targetTransform.position.z)
                                        - followDistance * targetTransform.forward 
                                        + cameraLROffset * targetTransform.right 
                                        + cameraHeight   * targetTransform.up;
        this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPos, ref velocity, smooth_time);

        //Camera rotation to target rotation
        this.transform.forward = Vector3.SmoothDamp(this.transform.forward, targetTransform.forward, ref velocity, smooth_time);
        
        //Camera Y rotation
        Vector3 targetRot = targetTransform.rotation.eulerAngles;
        Quaternion localRotation = Quaternion.Euler(rotX, targetRot.y, targetRot.z);
        if(Cursor.lockState==CursorLockMode.Locked)
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, localRotation, 10f*Time.fixedDeltaTime);


    }
}
