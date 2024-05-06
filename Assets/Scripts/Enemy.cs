using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int HP = 100;
    private Animator animator;

    private NavMeshAgent navAgent;

    public bool isDead;


    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if (HP <= 0)
        {

            int randomValue = Random.Range(0,2);

            if (randomValue == 0)
            {
                animator.SetTrigger("DIE1");
            }
            else
            {
                animator.SetTrigger("DIE2");
            }

            isDead = true;

            SoundManager.Instance.alienChannel.PlayOneShot(SoundManager.Instance.alienDeath);
            
        }
        else
        {
            animator.SetTrigger("DAMAGE");

            SoundManager.Instance.alienChannel2.PlayOneShot(SoundManager.Instance.alienHurt);
        }
    }

    private void OnDrawGizmos() 
    {         
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.5f); // attacking// stop attacking

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 120f);// detection start chasing

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 121f); // stop chasing
    }
}
