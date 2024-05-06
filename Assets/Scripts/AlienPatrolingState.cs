using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienPatrolingState : StateMachineBehaviour
{
    float timer;
    public float patrolingTime = 10f;

    Transform player;
    NavMeshAgent agent;

    public float detectionArea = 18f;
    public float patrolSpeed = 2f;

    List<Transform> waypointsList = new List<Transform>();
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // Initialization

       player = GameObject.FindGameObjectWithTag("Player").transform;
       agent = animator.GetComponent<NavMeshAgent>();

       agent.speed = patrolSpeed;
       timer = 0;

       // move to first waypoint

       GameObject waypointCluster = GameObject.FindGameObjectWithTag("Waypoints");
       foreach (Transform t in waypointCluster.transform)
       {
            waypointsList.Add(t);
       }

       Vector3 nextPosition = waypointsList[Random.Range(0, waypointsList.Count)].position;
       agent.SetDestination(nextPosition);
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {     

          if (SoundManager.Instance.alienChannel.isPlaying == false)
          {
               SoundManager.Instance.alienChannel.clip =SoundManager.Instance.alienWalking;
               SoundManager.Instance.alienChannel.PlayDelayed(1f);
          }

       // check if agent arraived at waypoiunt and moves to next waypoint

          if (agent.remainingDistance <= agent.stoppingDistance)
          {
               agent.SetDestination(waypointsList[Random.Range(0, waypointsList.Count)].position);
          }

       // transtion to idle state

          timer += Time.deltaTime;
          if (timer > patrolingTime)
          {
               animator.SetBool("isPatroling", false);
          }


        //transtion to chase state
          float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
          if (distanceFromPlayer < detectionArea)
          {
               animator.SetBool("isChasing", true);
          }

    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // stop the agent 
          agent.SetDestination(agent.transform.position);

          SoundManager.Instance.alienChannel.Stop();
    }
}
