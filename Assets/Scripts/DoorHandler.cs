using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    public Sprite OpenedDoor;
    public bool opened = false;
    private SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void OpenDoor()
    {
        sr.sprite = OpenedDoor;
    }



  
}
