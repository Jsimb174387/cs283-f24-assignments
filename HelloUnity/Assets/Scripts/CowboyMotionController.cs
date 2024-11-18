using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CowboyMotion : EnemyMotionController
{
    public float blockDist;
    [SerializeField] private Transform player;
    public void Update()
    {
        if (Vector3.Distance(player.position, transform.position) < blockDist)
        {
            Debug.Log("Player is close");
            playerAnimator.SetBool("blocking", true);
        }
        else
        {
            playerAnimator.SetBool("blocking", false);
        }
    }
    // Update is called once per frame
    public override void attack()
    {
        playerAnimator.SetBool("combo", false);
        float aValue = Random.value;
        Debug.Log(aValue);
        if (aValue < 0.5f)
        {
            playerAnimator.SetTrigger("attack1");
        }
        if (aValue <= 0.75f)
        {
            playerAnimator.SetTrigger("attack2");
        }
        else
        {
            playerAnimator.SetBool("combo", true);
            playerAnimator.SetTrigger("attack1");
        }

    }
}
