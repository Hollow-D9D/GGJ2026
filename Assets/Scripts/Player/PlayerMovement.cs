using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement parameters")]
    [SerializeField]
    private float speed;
    Rigidbody2D _rb;
    float _dir;

    [Header("Jump parameters")]
    [SerializeField]
    LayerMask ground;
    [SerializeField]
    float groundCheckRadius;
    bool _jumped;
    [SerializeField]
    private float jumpSpeed;


    bool _isGrounded;
    [SerializeField] private Transform _groundCheck;

    [SerializeField]
    float hangTime;
    float _hangCounter;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //movement
        _dir = Input.GetAxis("Horizontal");
        if (_dir != 0)
            _rb.velocity = new Vector2(_dir * speed, _rb.velocity.y);
        else if (0 > _dir)
             _rb.velocity = new Vector2(_dir * speed, _rb.velocity.y);
        else
        {
             float newXVal = _rb.velocity.x;
             if (newXVal > 0)
                 newXVal -= .2f;
             else if (newXVal < 0)
                 newXVal += .2f;
             newXVal = (newXVal <= .19f && newXVal >= -.19f) ? 0 : newXVal;
            if (Mathf.Abs(_rb.velocity.x) > 0)
                _rb.velocity = new Vector2(_rb.velocity.x * .7f, _rb.velocity.y);
            else
                _rb.velocity = new Vector2(0, _rb.velocity.y);

        }

        //ground check
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, groundCheckRadius, ground) || _rb.velocity.y == 0;
        //_isGrounded = Physics2D.Linecast(transform.position, _groundCheck.position, ground);
        if (_isGrounded)
        {
            _hangCounter = hangTime;
        }
        else
        {
            _hangCounter -= Time.fixedDeltaTime;
        }

        _jumped = !_isGrounded;
        //Jump
        if (Input.GetAxisRaw("Jump") != 0)
        {
            if (_hangCounter > 0 && !_jumped)
            {
                _jumped = true;
                _rb.velocity = new Vector2(_rb.velocity.x, jumpSpeed);
            }
        }
        if ((Input.GetAxisRaw("Jump") == 0 || _hangCounter <= 0) && _rb.velocity.y > 0)
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * .2f);
    }
}
