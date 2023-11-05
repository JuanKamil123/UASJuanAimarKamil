using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _speed;
    [Range(100, 500)]public float _jumpForce;
    public GameManager _gameManager;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private float moveX;
    private bool canJump;
    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(_gameManager.isWin) return;
        HorizontalMove();

        if(Input.GetButtonDown("Jump"))
        {
            if(!canJump) return;
            Jump();
        }
    }

    void FixedUpdate()
    {
        _animator.SetFloat("Speed", Mathf.Abs(moveX));
    }

    void HorizontalMove()
    {
        moveX = Input.GetAxisRaw("Horizontal");

        if (!Mathf.Approximately(0, moveX))
            {
                transform.rotation = -moveX > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
            }

        _rigidbody.velocity = new Vector2(moveX * _speed, _rigidbody.velocity.y); 
    }

    void Jump()
    {
        //_animator.SetTrigger("isJump");
        _rigidbody.AddForce(new Vector2(0, _jumpForce));
        canJump = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Ground"))
        {
            _animator.ResetTrigger("isJump");
            canJump = true;
        }
        else
        {
            _gameManager.RetryScene();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        _animator.SetTrigger("isJump");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Coin"))
        {
            Destroy(collider.gameObject);
            _gameManager.coin++;
        }

        if(collider.CompareTag("Finish"))
        {
            if(_gameManager.IsRequirementFilled()) _gameManager.isWin = true;
        }
    }
}
