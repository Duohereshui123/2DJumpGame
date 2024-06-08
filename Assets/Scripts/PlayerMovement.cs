using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    new BoxCollider2D collider;
    SpriteRenderer sprite;
    Animator animator;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private enum MovementState { idle, move, jump, fall };

    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdataAnimationState();
    }

    private void UpdataAnimationState()
    {
        MovementState moveState;

        if (dirX > 0f)
        {
            moveState = MovementState.move;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            moveState = MovementState.move;
            sprite.flipX = true;
        }
        else
        {
            moveState = MovementState.idle;
        }

        if (rb.velocity.y > 0.1f)
        {
            moveState = MovementState.jump;
        }
        else if (rb.velocity.y < -0.1f)
        {
            moveState = MovementState.fall;
        }

        animator.SetInteger("moveState", (int)moveState);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }
}
