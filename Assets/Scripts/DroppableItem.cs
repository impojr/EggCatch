using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppableItem : MonoBehaviour
{
    public Item item;
    public SpriteRenderer sprite;
    public float dropSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        if (item == null)
            throw new MissingReferenceException("item is missing assignment.");

        if (sprite == null)
            throw new MissingReferenceException("sprite is missing assignment.");

        sprite.sprite = item.image;
        gameObject.name = item.name;
    }

    void Update()
    {
        transform.position -= transform.up * Time.deltaTime * dropSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            item.Interact(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
