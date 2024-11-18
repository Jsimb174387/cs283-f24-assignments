using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMotionController : MonoBehaviour
{
    protected Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame

    public void walk()
    {
        playerAnimator.SetBool("isWalking", true);
    }
    public void stopWalk()
    {
        playerAnimator.SetBool("isWalking", false);
    }
    public virtual void attack()
    {
        playerAnimator.SetTrigger("Attack");
    }
        public void OnFootstep()
    {
    }
}
