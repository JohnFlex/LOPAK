using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectOnCollision : MonoBehaviour
{
    [SerializeField]
    ParticleSystem particleSystem;

    Vector2 storedVelocity;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        storedVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        storedVelocity = GetMaxDirection(storedVelocity);

        /*Debug.Log(storedVelocity);
        Debug.Log(storedVelocity.normalized);*/



        
        particleSystem.transform.localPosition = (storedVelocity.normalized) * 0.5f;

        /*
        if (collision.contacts[collision.contactCount-1].point.x > transform.position.x)
        {

            particleSystem.transform.localPosition = new Vector3(0, 1.9f);

        }
        else if (collision.contacts[collision.contactCount - 1].point.x < transform.position.x)
        {
            particleSystem.transform.localPosition = new Vector3(0, -1.9f);

        }
        else if (collision.contacts[collision.contactCount - 1].point.y > transform.position.y)
        {
            particleSystem.transform.localPosition = new Vector3(1.9f, 0);

        }
        else if (collision.contacts[collision.contactCount - 1].point.y < transform.position.y)
        {
            particleSystem.transform.localPosition = new Vector3(-1.9f,0);
        }
        */

        particleSystem.transform.LookAt(transform);
        particleSystem.Play();
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


}
