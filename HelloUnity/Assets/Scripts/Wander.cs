using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    public float wanderRadius = 1f;
    public float wanderRate = 50f;
    public NavMeshAgent agent;
    public Animator NPCAnimator;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = wanderRate;        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= wanderRate)
        {
            Vector3 newPos = RandomDestination(transform.position, wanderRadius);
            agent.SetDestination(newPos);
            timer = 0;
        }

        if(!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            Debug.Log("Reached destination");
            NPCAnimator.SetBool("isWalking", false);
        }
        else
        {
            Debug.Log("Walking");
            NPCAnimator.SetBool("isWalking", true);
        }
    }

    public static Vector3 RandomDestination(Vector3 origin, float distance)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, distance, NavMesh.AllAreas);
        return hit.position;
    }
}
