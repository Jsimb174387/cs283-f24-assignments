using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CowboyMotion : EnemyMotionController
{
    //private float blockDist = 14;
    [SerializeField] private Transform player;
    public void Update()
    {
    }
    
    // Update is called once per frame
    public override void attack()
    {
        playerAnimator.SetBool("blocking", true);
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
