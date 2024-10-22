using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPathLinear : MonoBehaviour
{
    public Transform[] path;
    public float duration = 3.0f;
    private bool isLerping = false;
    public int cPosition = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !isLerping)
        {
            StartCoroutine(DoLerp(cPosition, cPosition + 1));
            cPosition++;
            if (cPosition >= path.Length - 1)
            {
                cPosition = 0;
            }
        }
    }

    IEnumerator DoLerp(int pos1, int pos2)
    {
        isLerping = true;
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float t = timer / duration;
            transform.position = Vector3.Lerp(path[pos1].position, path[pos2].position, t);
            transform.rotation = Quaternion.Slerp(path[pos1].rotation, path[pos2].rotation, t);
            Vector3 direction = (path[pos2].position - path[pos1].position).normalized;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
            yield return null;


        }
        isLerping = false; 
    }
}
