using UnityEngine;
using System.Collections;


public class Target : MonoBehaviour
{
    public float health = 100f;

    public Animator animator;

    void Start()
    {

    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            animator.StopPlayback();
            StartCoroutine(Die());
        }
    }

    public IEnumerator Die()
    {
        GetComponent<Collider>().enabled = false;
        Score.numKilled += 1;
        ShopManager.money += 100;
        animator.SetBool("IsDead", true);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Object.Destroy(gameObject);
    }
}
