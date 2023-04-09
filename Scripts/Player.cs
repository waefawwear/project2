using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    PlayerInputActions input;
    public Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer rend;
    GameObject keyBoard;

    Vector2 dir;

    public enum PlayerMode
    {
        Basic = 0,
        Hardcore
    }

    public PlayerMode playermode = PlayerMode.Basic;

    public float jumpForce = 7.0f;
    public float moveSpeed = 5.0f;
    public float slideSpeed = 3.0f;

    private bool isJumping = false;
    private bool isWindZone = false;
    private bool isBigWindZone = false;
    private bool isRightWindZone = false;
    private bool isBigRightWindZone = false;
    private bool alive = true;

    public Action onTrigger;
    public Action playerDie;

    public GameObject KeyBoard => keyBoard;

    private void Awake()
    {
        input = new PlayerInputActions();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        keyBoard = transform.GetChild(0).gameObject;
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Move.performed += OnMove;
        input.Player.Move.canceled += OnMove;
        input.Player.Jump.performed += OnJump;
        input.Player.Trigger.performed += UseTrigger;
    }

    private void OnDisable()
    {
        input.Player.Trigger.performed -= UseTrigger;
        input.Player.Jump.performed -= OnJump;
        input.Player.Move.canceled -= OnMove;
        input.Player.Move.performed -= OnMove;
        input.Disable();
    }

    private void OnJump(InputAction.CallbackContext _)
    {
        if (!isJumping)
        {
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        dir = context.ReadValue<Vector2>();
        if(dir.x < 0.0f)
        {
            rend.flipX = true;
        }
        else if(dir.x > 0.0f)
        {
            rend.flipX = false;
        }
    }

    private void UseTrigger(InputAction.CallbackContext obj)
    {
        onTrigger?.Invoke();
    }

    private void JumpCheck()
    {
        anim.SetFloat("Jumphigh", rigid.velocity.y);
        anim.SetBool("IsJumping", isJumping);
        if (rigid.velocity.y == 0 && Physics2D.Raycast(transform.position, Vector2.down, 0.001f))
        {
            isJumping = false;
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(moveSpeed * dir.x * Vector2.right * Time.fixedDeltaTime);
        JumpCheck();
        CheckWindZone();

        if(Physics2D.Raycast(transform.position, Vector2.down, 1.0f, LayerMask.GetMask("IceGround")))
        {
            rigid.AddForce(moveSpeed * dir.x * slideSpeed * Vector2.right);
        }
    }

    private void CheckWindZone()    // 윈드존에 있을 때
    {
        if (isWindZone)
        {
            transform.Translate(moveSpeed * 0.8f * Vector2.left * Time.fixedDeltaTime);
        }

        if (isRightWindZone)
        {
            transform.Translate(moveSpeed * 0.8f * Vector2.right * Time.fixedDeltaTime);
        }

        if (isBigWindZone)
        {
            transform.Translate(moveSpeed * 1.3f * Vector2.left * Time.fixedDeltaTime);
        }

        if (isBigRightWindZone)
        {
            transform.Translate(moveSpeed * 1.3f * Vector2.right * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            if (alive)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        if (collision.CompareTag("MainFinish"))
        {
            if (alive)
            {
                SceneManager.LoadScene("MainScene");
            }
        }

        if (collision.CompareTag("Button"))
        {
            keyBoard.SetActive(true);
        }

        if (collision.CompareTag("Trap"))
        {
            if (alive)
            {
                Die();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("WindZone"))
        {
            isWindZone = true;
        }        
        
        if (collision.CompareTag("BigWindZone"))
        {
            isBigWindZone = true;
        }

        if (collision.CompareTag("WindZoneRight"))
        {
            isRightWindZone = true;
        }

        if (collision.CompareTag("BigWindZoneRight"))
        {
            isBigRightWindZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Button"))
        {
            keyBoard.SetActive(false);
        }

        if (collision.CompareTag("WindZone"))
        {
            isWindZone = false;
        }

        if (collision.CompareTag("BigWindZone"))
        {
            isBigWindZone = false;
        }

        if (collision.CompareTag("WindZoneRight"))
        {
            isRightWindZone = false;
        }

        if (collision.CompareTag("BigWindZoneRight"))
        {
            isBigRightWindZone = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Trap"))
        {
            if (alive)
            {
                Die();
            }
        }

        if (collision.collider.CompareTag("SkullTrap"))
        {
            ParticleSystem ps = collision.transform.GetComponentInChildren<ParticleSystem>();
            if (ps != null)
            {
                ps.Play();
                if (alive)
                {
                    Die();
                }
            }
        }
    }

    public void Die()
    {
        if (alive)
        {
            anim.SetTrigger("Die");
            alive = false;

            Destroy(gameObject, 2.0f);
            input.Disable();
            playerDie?.Invoke();
        }
    }
}
