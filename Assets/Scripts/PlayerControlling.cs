using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControlling : MonoBehaviour
{
    public float speedMultiplier;

    bool canMove = true;

    MainControls inputs;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        inputs = new MainControls();

        inputs.MainMap.Direction.performed += ctx => MovePlayer(ctx.ReadValue<Vector2>());

    }

    private void OnEnable()
    {
        inputs.MainMap.Enable();
    }

    private void OnDisable()
    {
        inputs.MainMap.Disable();
    }


    void MovePlayer(Vector2 direction)
    {
        if (canMove)
        {
            canMove = false;
            //rb.AddForce(GetMaxDirection(direction) * speedMultiplier);
            
#if UNITY_ANDROID

            rb.AddForce(Vector2.ClampMagnitude(GetMaxDirection(direction), 1.7f) * speedMultiplier);

#endif

#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
            rb.AddForce((GetMaxDirection(direction)) * speedMultiplier);
#endif
            
        }
    }

    private void Update()
    {
        Debug.Log(canMove);
        if (Mathf.Approximately(rb.velocity.x, 0f) && Mathf.Approximately(rb.velocity.y, 0f))
        {
            canMove = true;
            
        }
        else canMove = false;
    }

    Vector2 GetMaxDirection(Vector2 gotDirection)
    {
        if (Mathf.Abs(gotDirection.x) > Mathf.Abs(gotDirection.y))
        {
            return new Vector2(gotDirection.x, 0);
        }
        else
        {
            return new Vector2(0, gotDirection.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider.gameObject.transform.position.x > transform.position.x)
        {
            transform.position = new Vector3(transform.position.x - 0.001f, transform.position.y);

        }
        if (collision.otherCollider.gameObject.transform.position.x < transform.position.x)
        {
            transform.position = new Vector3(transform.position.x + 0.001f, transform.position.y);

        }
        if (collision.otherCollider.gameObject.transform.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.001f);

        }
        if (collision.otherCollider.gameObject.transform.position.y < transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.001f);

        }
    }
}
