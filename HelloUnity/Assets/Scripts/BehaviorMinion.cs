using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTAI;

public class BehaviorMinion : MonoBehaviour
{
    [Header("Enemy Stats")] 
    [SerializeField]
    private float accel;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float attackRate;
    private float attackTimer;
    [SerializeField]
    private float wanderRate;
    private float timer;
    private float walkAnimThresh;
    private float followRange;

    [Header("Enviroment Stats")]
    [SerializeField]
    private float wanderRadius;
    [SerializeField]
    private Transform wanderStart;
    [SerializeField]
    private Transform playerSafe;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private UnityEngine.AI.NavMeshAgent agent;
    [SerializeField]
    private EnemyMotionController motionController;

    private Root btRoot = BT.Root();
    
    void Start()
    {
        /* basic RPG BT afaik
        attack if enemy (player) is in range
        if not in range, follow the player if they haven't reached the safe zone
        if they have reached the safe zone, flee (return) to start zone
        else if already at the start zone, wander around
        */
        BTNode attack = BT.RunCoroutine(Attack);
        BTNode follow = BT.RunCoroutine(Follow);
        BTNode flee = BT.RunCoroutine(Flee);
        BTNode wander = BT.RunCoroutine(Wander);

        Selector selector = BT.Selector();
        selector.OpenBranch(
            attack,
            follow,
            flee,
            wander
        );
        btRoot.OpenBranch(selector);
    }

    void Update()
    {
        if (agent.velocity.magnitude > walkAnimThresh)
        {
            motionController.walk();
        }
        else
        {
            motionController.stopWalk();
        }

        btRoot.Tick();
        attackTimer += Time.deltaTime;
    }

    IEnumerator<BTState> Attack()
    {
            if (Vector3.Distance(player.position, transform.position) < attackRange && attackTimer >= attackRate)
            {
                attackTimer = 0;
                motionController.attack();
                yield return BTState.Success;
            }
            else
            {
                yield return BTState.Failure;
            }
    }

    IEnumerator<BTState> Follow()
    {
        if (playerSafe.GetComponent<Collider>().bounds.Contains(player.position) == false)
        {
            UnityEngine.AI.NavMeshHit hit;
            if (UnityEngine.AI.NavMesh.SamplePosition(player.position, out hit, playerSafe.localScale.x, UnityEngine.AI.NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
                yield return BTState.Success;
            }
            else
            {
                yield return BTState.Failure;
            }
        }
        else
        {
            yield return BTState.Failure;
        }
    }


    IEnumerator<BTState> Flee()
    {
            if (Vector3.Distance(player.position, playerSafe.position) < playerSafe.localScale.x)
            {
                UnityEngine.AI.NavMeshHit hit;
                UnityEngine.AI.NavMesh.SamplePosition(wanderStart.position, out hit, followRange, UnityEngine.AI.NavMesh.AllAreas);
                agent.SetDestination(hit.position);
                yield return BTState.Success;
            }
            else
            {
                yield return BTState.Failure;
            }

    }
    IEnumerator<BTState> Wander()
    {
        timer += Time.deltaTime;
        if (timer >= wanderRate)
        {
            Vector3 newPos = RandomDestination(transform.position, wanderRadius);
            agent.SetDestination(newPos);
            timer = 0;
            yield return BTState.Success;
        }
        else
        {
            yield return BTState.Failure;
        }

    }
    private static Vector3 RandomDestination(Vector3 origin, float distance)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;
        UnityEngine.AI.NavMeshHit hit;
        UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, distance, UnityEngine.AI.NavMesh.AllAreas);
        return hit.position;
    }
}