using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _jumpForce;

    private SpriteRenderer _player;
    private Animator _animator;
    private bool _isGrounded;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<SpriteRenderer>();
        _rigidbody2D= GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            _animator.SetFloat("Speed", _speed);
            _player.flipX = false;
            transform.Translate(_speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _animator.SetFloat("Speed", _speed);
            _player.flipX = _player.flipX = true;
            transform.Translate(_speed * Time.deltaTime * -1, 0, 0);
        }
        else
        {
            _animator.SetFloat("Speed", 0);
        }

        if(Input.GetKey(KeyCode.Space) && _isGrounded==true) 
        {
            _rigidbody2D.AddForce(transform.up*_jumpForce, ForceMode2D.Impulse);
            _isGrounded= false;
            _animator.SetBool("IsGrounded", _isGrounded);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _isGrounded = true;
            _animator.SetBool("IsGrounded", _isGrounded);
        }
    }
}

