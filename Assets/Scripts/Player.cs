using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using static UnityEngine.UI.Image;

public class Player : MonoBehaviour
{
    public Tilemap wallTilemap;
    public TileBase deadlyVineTile;
    public TextMeshProUGUI coinText;
    public float moveSpeed = 5f;

    private bool isMoving = false;
    private bool WillDie = false;
    private bool isFinishing = false;
    private Vector2 targetPos;
    private Vector2 moveDirection;
    private int CoinCounter;
    private int CollectedKeys;


    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public Transform spriteHolder;
   
    void Start()
    {
        CoinCounter = 0;
        CollectedKeys = 0;
       
        animator = spriteHolder.GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.W)) moveDirection = Vector2.up;
            else if (Input.GetKeyDown(KeyCode.S)) moveDirection = Vector2.down;
            else if (Input.GetKeyDown(KeyCode.A)) moveDirection = Vector2.left;
            else if (Input.GetKeyDown(KeyCode.D)) moveDirection = Vector2.right;
            else return;

            if (moveDirection != Vector2.zero)
            {
                Vector3Int currentCell = wallTilemap.WorldToCell(transform.position);
                Vector3Int nextCell = currentCell + new Vector3Int((int)moveDirection.x, (int)moveDirection.y, 0);
                while (!wallTilemap.HasTile(nextCell) && !IsPrefabWall(nextCell))
                {
                    
                    currentCell = nextCell;
                    nextCell += new Vector3Int((int)moveDirection.x, (int)moveDirection.y, 0);
                }
                TileBase tileAtTarget = wallTilemap.GetTile(nextCell);
                if (tileAtTarget != null && tileAtTarget == deadlyVineTile)
                {
                    WillDie = true;
                }

                targetPos = (Vector2)wallTilemap.GetCellCenterWorld(currentCell);
                StartCoroutine(MoveToTarget());
            }

           
        }
    }

    bool IsPrefabWall(Vector3Int cell)
    {
        Vector3 worldPos = wallTilemap.GetCellCenterWorld(cell);
        int layerMask = LayerMask.GetMask("Walls", "Door");

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, 0f, layerMask);
       
        if (hit.collider != null)
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.layer == LayerMask.NameToLayer("Walls"))
            {
                return true;
            }
            else if (hitObject.layer == LayerMask.NameToLayer("Door")) 
            {
                
                var script = hitObject.GetComponent<DoorHandler>();
                if (!script.opened)
                {
                    if(CollectedKeys > 0)
                    {
                        CollectedKeys--;
                        script.opened = true;
                        script.OpenDoor();
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }


    IEnumerator MoveToTarget()
    {
        isMoving = true;
        animator.SetBool("isFlying", true); // Start flying animation

        Vector3 scale = spriteHolder.localScale;
        Quaternion rotation = Quaternion.identity;

        // Flip horizontally
        if (moveDirection.x != 0)
        {
            scale.x = Mathf.Abs(scale.x) * (moveDirection.x < 0 ? -1 : 1);
         
            rotation = Quaternion.Euler(0f, 0f, moveDirection.x > 0 ? 90f : -90f); // Right = -90°, Left = 90°
        }
        else if (moveDirection.y != 0)
        {
            scale.x = Mathf.Abs(scale.x); 
                                         
            rotation = Quaternion.Euler(0f, 0f, moveDirection.y > 0 ? 180f : 0f); // Up = 180°, Down = 0°
        }

        spriteHolder.localScale = scale;
        spriteHolder.rotation = rotation;

        while ((Vector2)transform.position != targetPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        if (WillDie)
        {
     
            SceneManager.LoadScene("Game Over");
        }

        isMoving = false;
        animator.SetBool("isFlying", false);
    }

    public void IncCoins()
    {
        CoinCounter++;
        if (coinText != null)
            coinText.text =  CoinCounter.ToString();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("lethals") || other.CompareTag("Bee"))
        {
            
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game Over");
          
        }
        if (other.CompareTag("Coins"))
        {
            
            other.GetComponent<CoinCollect>().PlaySound();
            other.gameObject.SetActive(false);
            
        }
        if (other.CompareTag("Key"))
        {
            other.gameObject.SetActive(false);
            CollectedKeys++;
        }
        if (other.CompareTag("Winning Candy"))
        {
            if (!isFinishing)
            {

                StartCoroutine(FinishSequence());
            }
        }
    }

    IEnumerator FinishSequence()
    {
        isFinishing = true;

        moveSpeed = 0.5f;

        yield return new WaitForSeconds(0.8f);

        UnityEngine.SceneManagement.SceneManager.LoadScene("success");


    }
   

}
