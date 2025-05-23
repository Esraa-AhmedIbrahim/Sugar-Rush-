using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float floatSpeed = 0.5f;
    public float floatHeight = 0.25f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Rotate around Y axis
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        // Float up and down
        float newY = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPos.x, startPos.y + newY, startPos.z);
    }
}
