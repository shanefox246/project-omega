using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float detectionRange = 125f;
    public float attackRange = 10f;

    public Animator animator;

    Transform target;
    NavMeshAgent agent;

    bool attackFinished = true;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= detectionRange)
        {
            agent.SetDestination(target.position);
            StartCoroutine(Chase());

            if (distance <= attackRange && attackFinished)
                StartCoroutine(Attack());

            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 7.5f);
    }

    IEnumerator Attack()
    {
        animator.SetBool("AttackRange", true);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetBool("AttackRange", false);
        attackFinished = false;
    }

    IEnumerator Chase()
    {
        if (attackFinished)
        {
            //walkSound.Play();
            animator.SetBool("DetectionRange", true);
            yield return null;
        }
        else
        {
            animator.SetBool("DetectionRange", false);
            yield return new WaitForSeconds(0.1f);
            attackFinished = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            playerHealth.TakeDamage(34);
        }
    }
}