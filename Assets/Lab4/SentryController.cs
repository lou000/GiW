using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryController : MonoBehaviour
{
    private Animator anim;

    public GameObject[] path;
    [Range(0, 10)]
    public float rotationSpeed = 1.0f;
    [Range(0, 2)]
    public float switchTargetDistance = 1.0f;
    [Range(0,360)]
    public float moveAngle = 40f;

    private int nextTarget;
    public float targetDistance;

    // Start is called before the first frame update
    void Start()
    {
        nextTarget = 0;
        anim = gameObject.GetComponent<Animator>();

        for(int i=0; i<path.Length;i++)
        {
            if(i<path.Length-1)
                Debug.DrawLine(path[i].transform.position, path[i+1].transform.position,Color.white,Mathf.Infinity);
            else
                Debug.DrawLine(path[i].transform.position, path[0].transform.position,Color.white,Mathf.Infinity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Transform target = path[nextTarget].transform;

        Vector3 targetDirection = target.position - transform.position;
        targetDirection = new Vector3(targetDirection.x, 0, targetDirection.z);
        targetDistance = targetDirection.magnitude;

        float singleStep = rotationSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        if(targetDirection.magnitude<switchTargetDistance)
            nextTarget++;
        if(nextTarget>path.Length-1)
            nextTarget = 0;

        float angle = Vector3.Angle(targetDirection, transform.forward);
        if(angle<moveAngle)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }
}
