
   
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpikeHandler : MonoBehaviour
{
    public GameObject trigger;
    public GameObject spike;
    // Start is called before the first frame update
    private Vector2 dir;
    public float speed = 0.01f;

    private void Awake()
    {
        if (trigger.transform.position.x == spike.transform.position.x)
        {
            if (trigger.transform.position.y < spike.transform.position.y)
            {
                dir = Vector2.down;
            }
            else
            {
                dir = Vector2.up;
            }
        }
        else
        {
            if (trigger.transform.position.x < spike.transform.position.x)
            {
                dir = Vector2.left;
            }
            else
            {
                dir = Vector2.right;
            }
        }
    }
    public IEnumerator triggerSpike()
    {
        yield return new WaitForSeconds(0.3f); // Wait for 2 seconds
        spike.SetActive(true);
        Debug.Log("Ad");
        while (true)
        {
            spike.transform.position = new Vector2((dir.x * speed) + spike.transform.position.x, (dir.y * speed) + spike.transform.position.y);
            if (dir == Vector2.down)
            {
                if (spike.transform.position.y <= trigger.transform.position.y)
                {
                    break;
                }

            }
            else if (dir == Vector2.up)
            {
                if (spike.transform.position.y >= trigger.transform.position.y)
                {
                    break;
                }

            }
            else if (dir == Vector2.right)
            {
                if (spike.transform.position.x >= trigger.transform.position.x)
                {
                    break;
                }

            }
            else if (dir == Vector2.left)
            {
                if (spike.transform.position.x <= trigger.transform.position.x)
                {
                    break;
                }

            }
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(0.9f);
        dir *= -1;
        while (true)
        {
            spike.transform.position = new Vector2((dir.x * speed) + spike.transform.position.x, (dir.y * speed) + spike.transform.position.y);
            if (dir == Vector2.down)
            {
                if (spike.transform.position.y <= this.transform.position.y)
                {
                    break;
                }

            }
            else if (dir == Vector2.up)
            {
                if (spike.transform.position.y >= this.transform.position.y)
                {
                    break;
                }

            }
            else if (dir == Vector2.right)
            {
                if (spike.transform.position.x >= this.transform.position.x)
                {
                    break;
                }

            }
            else if (dir == Vector2.left)
            {
                if (spike.transform.position.x <= this.transform.position.x)
                {
                    break;
                }

            }
            yield return new WaitForFixedUpdate();
        }
        dir *= -1;
        spike.transform.position = this.transform.position;
        spike.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {

    }
}
