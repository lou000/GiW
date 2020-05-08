using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryController : MonoBehaviour
{
    private CharacterController rController;
    private Animator anim;
    public int nextTarget;

    public GameObject [] path;
    [Range(0, 10)]
    public float rotationSpeed = 1.0f;
    [Range(0, 2)]
    public float switchTargetDistance = 1.0f;
    public float targetDistance;

    // Start is called before the first frame update
    void Start()
    {
        nextTarget = 0;
        rController = GetComponent<CharacterController>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Transform target = path[nextTarget].transform;
        // Determine which direction to rotate towards
        Vector3 targetDirection = target.position - transform.position;
        targetDirection = new Vector3(targetDirection.x, 0, targetDirection.z);
        targetDistance = targetDirection.magnitude;

        // The step size is equal to speed times frame time.
        float singleStep = rotationSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDirection);

        if(targetDirection.magnitude<switchTargetDistance)
            nextTarget++;
        if(nextTarget>path.Length-1)
            nextTarget = 0;
        
        anim.SetBool("Walk", true);
    }
}
