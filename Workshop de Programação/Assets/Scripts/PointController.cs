using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour
{
    [HideInInspector] public int points;
    [SerializeField] private float speed;

    private GameManager gameManager;
    private Rigidbody2D rb;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Start()
    {
        rb.velocity = speed * Vector2.left;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Player":
                gameManager.AddScore(points);
                Destroy(gameObject);
                break;
            case "Wall":
                Destroy(gameObject, 3f);
                break;
            default:
                break;
        }

    }
}
