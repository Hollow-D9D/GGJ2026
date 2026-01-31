using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;

    public Vector2 directionNormalized;

    public int damage = 25;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Debug.Log(rb.velocity);
        StartCoroutine(ReduceVelocity());
    }

    private IEnumerator ReduceVelocity()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, .02f);

        //rb.AddForce(10 * -directionNormalized);

    }
}
