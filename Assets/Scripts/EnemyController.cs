using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
  
    private Animator animator;
    
    public ParticleSystem runPartical;

    public NavMeshAgent enemy;


    void Start()
    {
      
        player = GameObject.Find("Player");

        animator = GetComponent<Animator>();

        enemy.isStopped = false;


    }

    
    void LateUpdate()
    {
       
        enemy.SetDestination(player.transform.position);

        EnemyBound();

        İsStopped();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.isStopped = true;
            runPartical.Stop();
        }
    }


    void İsStopped()
    {
        if(enemy.isStopped == false)
        {
            animator.SetBool("isRun", true);
        }

        if (enemy.isStopped == true)
        {
            animator.SetBool("isRun", false);
        }

    }


    void EnemyBound()
    {
        if (transform.position.z < -35.0f)
        {
           transform.position = new Vector3(transform.position.x, transform.position.y, -35.0f);
        }
    }

}
