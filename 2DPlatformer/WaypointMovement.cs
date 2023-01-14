using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private SpriteRenderer _player;
    private Animator _animator;
    private int _currentDirection;
    private int _rightDirection = 1;
    private int _leftDirection = -1;

    private void Start()
    {
        _player = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _currentDirection = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Check Point"))
        {
            if(_currentDirection == _rightDirection)
            {
                _player.flipX = true;
                _currentDirection = _leftDirection;
                _animator.SetFloat("Speed", _speed);
            }
            else
            {
                _player.flipX = false;
                _currentDirection = _rightDirection;
                _animator.SetFloat("Speed", _speed);
            }
        }
    }

    private void Update()
    {
        transform.Translate(_speed * Time.deltaTime * _currentDirection, 0, 0);
    }
}
