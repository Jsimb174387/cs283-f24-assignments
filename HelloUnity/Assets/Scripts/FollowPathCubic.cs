using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPathCubic : MonoBehaviour
{
    public Transform[] path;
    public float duration = 3.0f;
    private bool isLerping = false;
    public int cPosition = 0;
    public bool DeCasteljau = true;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.B) && !isLerping)
        {
            StartCoroutine(DoLerp(cPosition, cPosition + 1));
            cPosition++;
        }
        if (cPosition >= path.Length - 1)
        {
            cPosition = 0;
        }        
    }

    List<Vector3> GetPoints(int pos1, int pos2)
    {
        Vector3 b0 = path[pos1].position;
        Vector3 b3 = path[pos1+1].position;

        Vector3 b1;
        Vector3 b2;
        if (pos1 == 0){
            b1 = b0 + (1f/6f) * (b3-b0);
        } else {
            b1 = b0 + (1f/6f) * (b3 - path[pos1-1].position);
        }
        if (pos2 == path.Length - 1){
            b2 = b3 - (1f/6f) * (b3-b0);
        } else {
            b2 = b3 - (1f/6f) * (path[pos2+1].position - b0);
        }
        return new List<Vector3> {b0, b1, b2, b3};
    }

    IEnumerator DoLerp(int pos1, int pos2)
    {
        // Should be 4 points in total. b0, b1, b2, b3
        isLerping = true;
        List<Vector3> points = GetPoints(pos1, pos2);
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float t = timer / duration;
            Vector3 pos;
            if (DeCasteljau)
            {
                pos = DeCastel(points, t);
            } else 
            {
                pos = Polynomial(points, t);
            }

            transform.position = pos;
            // essentially making it face the direction of slighty infront of current
            Vector3 direction = (Polynomial(points, t + 0.01f) - pos).normalized;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }

            yield return null;
        }
        isLerping = false;
    }

    Vector3 DeCastel(List<Vector3> points, float t)
    {
        Vector3 b0 = points[0];
        Vector3 b1 = points[1];
        Vector3 b2 = points[2];
        Vector3 b3 = points[3];
        Vector3 A = Vector3.Lerp(b0, b1, t);
        Vector3 B = Vector3.Lerp(b1, b2, t);
        Vector3 C = Vector3.Lerp(b2, b3, t);
        Vector3 D = Vector3.Lerp(A, B, t);
        Vector3 E = Vector3.Lerp(B, C, t);
        Vector3 F = Vector3.Lerp(D, E, t);

        return F;
    }

    Vector3 Polynomial(List<Vector3> points, float t)
    {
        // p(t) = (1 - t)^3 * b0 + 3(1 - t)^2 * b1 + 3t^2(1 - t) * b2 + t^3 * b3
        Vector3 b0 = points[0];
        Vector3 b1 = points[1];
        Vector3 b2 = points[2];
        Vector3 b3 = points[3];
        float u = 1 - t;
        Vector3 p = u*u*u*b0 + 3*t*u*u*b1 + 3*t*t*u*b2 + t*t*t*b3;
        
        return p;
    }
}
