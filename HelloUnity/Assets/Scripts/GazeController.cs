using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeController : MonoBehaviour
{
    public Transform target;
    public Transform headJoint;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - headJoint.position;
        if (direction != Vector3.zero)
        {
            headJoint.rotation = Quaternion.LookRotation(direction);
            Debug.DrawLine(headJoint.position, target.position, Color.red);
        }
    }
}
