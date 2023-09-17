using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float destroyImpactMagnitude = 5;
    [SerializeField] AudioSource sourceFx;
    [SerializeField] AudioClip enemyHitFx;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground"))
        {
            Die();
        }

        if (collision.relativeVelocity.magnitude > destroyImpactMagnitude)
        {
            Die();
        }
    }

    void Die()
    {
        sourceFx.PlayOneShot(enemyHitFx);
        Invoke(nameof(Destroy), 0.05f);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
