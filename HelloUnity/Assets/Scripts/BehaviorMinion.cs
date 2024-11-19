using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTAI;

public class BehaviorMinion : MonoBehaviour
{
    [Header("Enemy Stats")] 
    [SerializeField] private float health = 1;
    [SerializeField] private float attackRange = 1;
    [SerializeField] private float attackRate = 1;
    private float attackTimer = 0;
    [SerializeField] private float wanderRate = 1;
    private float timer;
    private float walkAnimThresh = 0.1f;
    private float followRange = 1;

    [Header("Enviroment Stats")]
    [SerializeField] private float wanderRadius = 1;
    [SerializeField] private Transform wanderStart = null;
    [SerializeField] private Transform playerSafe = null;
    [SerializeField] protected Transform player = null;
    [SerializeField] private UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] private EnemyMotionController motionController;

    private Root btRoot = BT.Root();
    
    void Start()
    {
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        } else 
        {
            Debug.Log("Cannot find player object");
        }
        GameObject playerSafeObject = GameObject.Find("PlayerSafe");
        if (playerSafeObject != null)
        {
            playerSafe = playerSafeObject.transform;
        } else 
        {
            Debug.Log("Cannot find player safe object");
        }
        GameObject wanderStartObject = GameObject.Find("WanderStart");
        if (wanderStartObject != null)
        {
            wanderStart = wanderStartObject.transform;
        } else 
        {
            Debug.Log("Cannot find wander start object");
        }


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

    public void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
