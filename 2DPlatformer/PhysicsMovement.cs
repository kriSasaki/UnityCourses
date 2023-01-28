using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PhysicsMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _minGroundNormalY = .65f;
    [SerializeField] private float _gravityModifier = 1f;
    [SerializeField] private LayerMask _layerMask;

    private Vector2 _velocity;

    protected Vector2 _targetVelocity;
    protected bool _isGrounded;
    protected Vector2 _groundNormal;
    protected Rigidbody2D _rigidbody2D;
    protected SpriteRenderer _spriteRenderer;
    protected Animator _animator;
    protected ContactFilter2D _contactFilter;
    protected RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);

    protected const float MinMoveDistance = 0.001f;
    protected const float ShellRadius = 0.01f;
    private const string Speed = "Speed";
    private const string IsGrounded = "IsGrounded";

    void OnEnable()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(_layerMask);
        _contactFilter.useLayerMask = true;
    }

    void Update()
    {
        _targetVelocity = new Vector2(Input.GetAxis("Horizontal") * _speed, 0);

        if (_targetVelocity.x < 0)
        {
            _animator.SetFloat(Speed, -_targetVelocity.x);
            _spriteRenderer.flipX = true;
        }
        else if (_targetVelocity.x > 0)
        {
            _animator.SetFloat(Speed, _targetVelocity.x);
            _spriteRenderer.flipX = false;
        }
        else
        {
            _animator.SetFloat(Speed, _targetVelocity.x);
        }

        if (Input.GetKey(KeyCode.Space) && _isGrounded)
        {
            _velocity.y = _jumpForce;
            _animator.SetBool(IsGrounded, !_isGrounded);
        }
    }

    void FixedUpdate()
    {
        _velocity += _gravityModifier * Physics2D.gravity * Time.deltaTime;
        _velocity.x = _targetVelocity.x;

        _isGrounded = false;

        Vector2 deltaPosition = _velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > MinMoveDistance)
        {
            int count = _rigidbody2D.Cast(move, _contactFilter, _hitBuffer, distance + ShellRadius);

            _hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                _hitBufferList.Add(_hitBuffer[i]);
            }

            for (int i = 0; i < _hitBufferList.Count; i++)
            {
                Vector2 currentNormal = _hitBufferList[i].normal;

                if (currentNormal.y > _minGroundNormalY)
                {
                    _isGrounded = true;
                    _animator.SetBool(IsGrounded, _isGrounded);

                    if (yMovement)
                    {
                        _groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(_velocity, currentNormal);

                if (projection < 0)
                {
                    _velocity = _velocity - projection * currentNormal;
                }

                float modifiedDistance = _hitBufferList[i].distance - ShellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        _rigidbody2D.position = _rigidbody2D.position + move.normalized * distance;
    }
}
