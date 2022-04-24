using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private HealthBarController hpBar;
    [SerializeField] private GameObject pointPrefab;

    private Sprite sprite;
    private int hp;
    private int damage;
    private int reward;
    private float speed;

    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private SpriteRenderer sr;
    private Vector2 srSize;
    private PlayerController pc;

    private void Awake()
    {
        pc = FindObjectOfType<PlayerController>();
        bc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        srSize = sr.size;
    }

    private void Start()
    {
        rb.velocity = speed * Vector2.left;
    }

    public void FillValues(EnemySO enemySO)
    {
        sr.sprite = enemySO.sprite;
        bc.size = enemySO.sprite.bounds.size;

        hp = enemySO.hp;
        damage = enemySO.damage;
        reward = enemySO.reward;
        speed = enemySO.speed;
        hpBar.SetMaxHealth(hp);
    }

    private IEnumerator TakeDamage(int dmg)
    {
        hp -= dmg;
        hpBar.SetHealth(hp);

        if (hp <= 0)
        {
            GameObject point = Instantiate(pointPrefab, transform.position, Quaternion.identity);
            var pointController = point.GetComponent<PointController>();
            pointController.points = reward;
            Destroy(gameObject);
        }

        //Animate Damage
        sr.color = Color.red;
        sr.size = srSize * 0.8f;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
        sr.size = srSize;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Bullet":
                Destroy(other.gameObject);
                StartCoroutine(TakeDamage(pc.playerDamage));
                break;

            case "Wall":
                pc.TakeDamage(damage);
                Destroy(gameObject);
                break;

            default:
                break;
        }
    }
}
