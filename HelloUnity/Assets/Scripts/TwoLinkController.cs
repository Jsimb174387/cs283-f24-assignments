using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoLinkController : MonoBehaviour
{
    public Transform target;

    public Transform endEffector;
    public Transform middleJoint; // "elbow"
    public Transform baseJoint; // "shoulder"

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null || endEffector == null || middleJoint == null || baseJoint == null)
        {
            Debug.LogError("Missing a transform");
            return;
        }

        if (r > l1 + l2)
        {
            Debug.LogError("Target is too far");
        }
        
        float r = Vector3.Distance(target.position, baseJoint.position);
        float l1 = Vector3.Distance(middleJoint.position, baseJoint.position);
        float l2 = Vector3.Distance(endEffector.position, middleJoint.position);

        // r2 = l1^2 + l2^2 - 2(l1)(l2)cos(theta2)
        // below is rearranged, solving for cos(theta2)
        float cosTheta2 = (l1 * l1 + l2 * l2 - r * r) / (2 * l1 * l2);
        // elbow angle needed to reach target
        float theta2 = Mathf.Acos(cosTheta2);


        Vector3 directionToTarget = (target.position - baseJoint.position).normalized;
        // Calculate the position of the middle joint
        Vector3 middleJointPosition = baseJoint.position + directionToTarget * l1;

        // Calculate the direction from the middle joint to the end effector
        Vector3 directionToEndEffector = (target.position - middleJointPosition).normalized;


        baseJoint.rotation = Quaternion.LookRotation(directionToTarget);
        middleJoint.rotation = Quaternion.LookRotation(directionToEndEffector);

        middleJoint.position = middleJointPosition;
        endEffector.position = target.position;

        Debug.DrawLine(baseJoint.position, middleJoint.position, Color.red);
        Debug.DrawLine(middleJoint.position, endEffector.position, Color.red);         
    }
}
