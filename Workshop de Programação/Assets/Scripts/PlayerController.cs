using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private GameObject shot;
    [SerializeField] private int shotSpeed;
    [SerializeField] private int shotsPerSecond;
    public HealthBar hpBar;
    public int damage;
    public int health;
    private Rigidbody2D rb;


    private void Awake()
    {
        hpBar.SetMaxHealth(health);
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (transform.position.y > 4)
        {
            transform.position = new Vector2(transform.position.x, 4);
        }
        else if (transform.position.y < -4)
        {
            transform.position = new Vector2(transform.position.x, -4);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        float _direction = context.ReadValue<float>();
        rb.velocity = _direction * speed * Vector2.up;
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SingleShot();
            StartCoroutine(ShootCoroutine());
        }
        else if (!context.control.IsPressed())
        {
            StopAllCoroutines();
        }
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        hpBar.SetHealth(health);
        if (health <= 0)
        {
            //Kill player
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void SingleShot()
    {
        var temp = Instantiate(shot, transform.position, transform.rotation);
        var rb = temp.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * shotSpeed;
        Destroy(temp, 2f);
    }
    
    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f/shotsPerSecond);
            SingleShot();
        }
    }
}
