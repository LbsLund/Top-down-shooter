using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 moveInput;
    Vector2 screenBoundery;

    [SerializeField] public int health = 5;
    [SerializeField] float invinsibleTime = 3f;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float bulletSpeed = 7f;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject gun;

    bool invinsible;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        screenBoundery = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnAttack()
    {
        Rigidbody2D playerBullet = Instantiate(bullet, gun.transform.position, transform.rotation).GetComponent<Rigidbody2D>();
        playerBullet.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
    }

    void Update()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -screenBoundery.x, screenBoundery.x)
                                        , Mathf.Clamp(transform.position.y, -screenBoundery.y, screenBoundery.y));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
        rb.rotation = rb.rotation - moveInput.x * 2f;
    }

    void ResetInvinsibility()
    {
        invinsible = false;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(128 / 255f, 236 / 255f, 125 / 255f, 255 / 255f);
        // Debug.Log("ResetInvinsibility");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player OnCollision");
        if (collision.gameObject.CompareTag("Enemies") && !invinsible)
        {
            if (health <= 1)
            {
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Player Hurt");
                health--;
                invinsible = true;
                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                sr.color = Color.purple;
                Invoke("ResetInvinsibility", invinsibleTime);
                //Debug.Log("Player health" + playerHealth);
            }
        }
    }
}
