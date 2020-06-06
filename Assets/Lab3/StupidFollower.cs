using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class StupidFollower : MonoBehaviour
{
    [Range(0, 100)]
    public float baseSpeed = 20f;
    [Range(0,100)]
    public float maxSpeed = 10f;
    [Range(0,2)]
    public float rotationSpeed = 0.5f;
    [Range(0,360)]
    public float visionAngle = 45f;
    [Range(0,360)]
    public float moveAngle = 5f;
    public Collider target = null;
    Animator anim;

    private Rigidbody rController;
    private Collider rCollider;
    private Vector3 targetDir;
    private float angle;
    // Start is called before the first frame update
    void Start()
    {
        rCollider = GetComponent<Collider>();
        rController = GetComponent<Rigidbody>();
        rController.freezeRotation = true;
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("Open_Anim", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(target!=null)
        {
            targetDir = target.transform.position - rCollider.transform.position;
            angle = Vector3.Angle(targetDir, transform.forward);
        }
    }

    void OnDrawGizmos() 
    {
        if(angle<visionAngle && target!=null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, target.transform.position);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(target == null && other.tag == "Player")
            target = other;
    }
    private void OnTriggerExit(Collider other) {
        if(target != null && other.tag == "Player")
            target = null;
    }

    void FixedUpdate()
    {
        if(angle<visionAngle && target!=null)
        {
            targetDir.y = 0;
            float singleStep = rotationSpeed * Time.fixedDeltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDir, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);


            if(angle<moveAngle && targetDir.magnitude>2f)
            {
                rController.AddForce(transform.forward * baseSpeed);
                anim.SetBool("Walk_Anim", true);
            }
            else
            {
                anim.SetBool("Walk_Anim", false);
            }

            if(targetDir.magnitude<2f)
            {
                rController.velocity = Vector3.zero;
                anim.SetBool("Roll_Anim", false);
            }
            if(rController.velocity.magnitude>maxSpeed)
            {
                anim.SetBool("Walk_Anim", false);
                anim.SetBool("Roll_Anim", true);
                rController.velocity = rController.velocity.normalized*maxSpeed;
            }
        }
    }
}
