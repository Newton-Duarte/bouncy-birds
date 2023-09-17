using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    Vector3 startPosition;
    Rigidbody2D rb;
    [SerializeField] float maxDragDistance = 2;
    [SerializeField] float launchPower = 350;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (FindAnyObjectByType<Enemy>(FindObjectsInactive.Exclude) == null)
        {
            Debug.Log("Game Over");
            int levelToLoad = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(levelToLoad);
        }
    }

    void OnMouseUp()
    {
        Vector3 directionAndMagnitude = startPosition - transform.position;
        rb.AddForce(directionAndMagnitude * launchPower);
        rb.gravityScale = 1;
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
    }
}
