using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health;
    public int damage;
    public int value;
    public int speed;
    public GameObject pointPrefab;   
    public HealthBar hpBar;
    private SpriteRenderer sprRend;
    private Vector2 sprSize;
    private PlayerController playerController;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        sprRend = GetComponent<SpriteRenderer>();
        sprSize = sprRend.size;
    }
    private void Start()
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * (float) speed/10f;
    }
    public void FillValues(Enemy properties)
    {
        var renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = properties.sprite;
        GetComponent<BoxCollider2D>().size = renderer.sprite.bounds.size;
        health = properties.health;
        damage = properties.damage;
        value = properties.value;
        speed = properties.speed;
        hpBar.SetMaxHealth(health);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "shot")
        {
            Destroy(other.gameObject);
            TakeDamage(playerController.damage);
        }
        if (other.tag == "wall")
        {
            playerController.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void TakeDamage(int dmg)
    {
        StartCoroutine(AnimateDamage());
        health -= dmg;
        hpBar.SetHealth(health);
        if (health <= 0)
        {
            //Generate PointObject
            GameObject point = Instantiate(pointPrefab, transform.position, Quaternion.identity);
            var rb = point.GetComponent<Rigidbody2D>();
            rb.velocity = transform.up * (float) speed/10f;
            point.GetComponent<Point>().points = value;
            Destroy(gameObject);
        }
    }

    private IEnumerator AnimateDamage()
    {
        sprRend.size = sprSize * 0.8f;
        yield return new WaitForSeconds(0.1f);
        sprRend.size = sprSize;
    }
}
