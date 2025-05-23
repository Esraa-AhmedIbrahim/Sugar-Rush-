using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrigger : MonoBehaviour
{
    SpikeHandler sh;
    void Awake()
    {
        sh = GetComponentInParent<SpikeHandler>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered by Player!");
        if (other.CompareTag("player"))
        {
            if (sh.spike.gameObject.activeInHierarchy == false)
                StartCoroutine(sh.triggerSpike());

        }
    }

}
