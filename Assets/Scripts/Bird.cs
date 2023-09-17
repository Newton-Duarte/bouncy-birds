using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    Vector3 startPosition;
    Rigidbody2D rb;
    LineRenderer lr;
    [SerializeField] float maxDragDistance = 2;
    [SerializeField] float launchPower = 350;
    [SerializeField] AudioSource sourceFx;
    [SerializeField] AudioClip wooshFx;
    [SerializeField] AudioClip impactFx;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, transform.position);
        lr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (FindAnyObjectByType<Enemy>(FindObjectsInactive.Exclude) == null)
        {
            Invoke(nameof(LoadNextLevel), 1f);
        }
    }

    void LoadNextLevel()
    {
        int levelToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(levelToLoad);
    }

    void OnMouseUp()
    {
        Vector3 directionAndMagnitude = startPosition - transform.position;
        rb.AddForce(directionAndMagnitude * launchPower);
        rb.gravityScale = 1;
        lr.enabled = false;
        sourceFx.PlayOneShot(wooshFx);
    }

    void OnMouseDrag()
    {
        Vector3 destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        destination.z = 0;
        if (Vector2.Distance(destination, startPosition) > maxDragDistance)
        {
            destination = Vector3.MoveTowards(startPosition, destination, maxDragDistance);
        }
        transform.position = destination;
        lr.SetPosition(1, transform.position);
        lr.enabled = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            ReloadLevel();
            return;
        }
        sourceFx.PlayOneShot(impactFx);
        Invoke(nameof(ReloadLevel), 5);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
