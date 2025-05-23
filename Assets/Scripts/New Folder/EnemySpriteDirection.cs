using UnityEngine;


//change sprite based on move

public class EnemySpriteDirection : MonoBehaviour
{
    public Sprite rightFacingSprite;
    public Sprite leftFacingSprite;

    private SpriteRenderer spriteRenderer;
    private Vector3 lastPosition;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastPosition = transform.position;
    }

    void Update()
    {
        Vector3 movement = transform.position - lastPosition;

        if (movement.x > 0.01f) // Moved right
        {
            spriteRenderer.sprite = rightFacingSprite;
        }
        else if (movement.x < -0.01f) // Moved left
        {
            spriteRenderer.sprite = leftFacingSprite;
        }

        lastPosition = transform.position;
    }


 




}
