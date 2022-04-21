using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private int playerHP;
    [SerializeField] private HealthBarController hpBar;
    public int playerDamage;

    [Header("Shooting")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int bulletSpeed;
    [SerializeField] private float shotCooldown;

    private Rigidbody2D rb;
    private IEnumerator shotCoroutine;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        shotCoroutine = ShotCoroutine();
        hpBar.SetMaxHealth(playerHP);
    }

    private void Update()
    {
        if (transform.position.y > 4)
        {
            transform.position = new Vector3(transform.position.x, 4, transform.position.z);
        }
        else if (transform.position.y < -4)
        {
            transform.position = new Vector3(transform.position.x, -4, transform.position.z);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 velocity = context.ReadValue<float>() * Vector2.up;
        rb.velocity = velocity * playerSpeed;
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        switch(context.phase)
        {
            case InputActionPhase.Performed:
                StartCoroutine(shotCoroutine);
                break;
            case InputActionPhase.Canceled:
                StopCoroutine(shotCoroutine);
                break;
            default:
                break;
        }
    }

    private IEnumerator ShotCoroutine()
    {
        while (true)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = Vector2.right * bulletSpeed;
            Destroy(bullet, 2f);

            yield return new WaitForSeconds(shotCooldown);
        }
    }

    public void TakeDamage(int damage)
    {
        playerHP -= damage;
        hpBar.SetHealth(playerHP);
        if (playerHP <= 0)
        {
            //Kill player
            gameManager.ReloadScene();
        }
    }
}
