using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float speed;

    private Vector2 direction;
    private SpriteRenderer renderer;

    void Start()
    {
        renderer = transform.parent.GetComponent<SpriteRenderer>();
        direction = transform.parent.up;
        transform.position = new Vector2(transform.parent.position.x + (renderer.bounds.size.x / 2 * direction.x), transform.parent.position.y + (renderer.bounds.size.y / 2 * direction.y));

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Coins") && !collision.CompareTag("Bee"))
        {
            transform.position = new Vector2(transform.parent.position.x + (renderer.bounds.size.x / 2 * direction.x), transform.parent.position.y + (renderer.bounds.size.y / 2 * direction.y));
        }
    }
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.position = new Vector2(transform.parent.position.x + (transform.parent.GetComponent<SpriteRenderer>().bounds.size.x / 2 * direction.x), transform.parent.position.y + (transform.parent.GetComponent<SpriteRenderer>().bounds.size.y / 2 * direction.y));
 
    }*/
}