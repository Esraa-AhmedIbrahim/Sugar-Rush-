using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    public AudioClip collectSound;
    public ParticleSystem ParticleSystem;

    //private GameObject obj;

    public void PlaySound()
    {
        AudioSource.PlayClipAtPoint(collectSound, transform.position);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            other.GetComponent<Player>().IncCoins();
            if(ParticleSystem != null)
            {
                var obj = Instantiate(ParticleSystem, transform.position, Quaternion.identity);
                Debug.Log("eh tab");
                Destroy(obj, 1f);
            }
         

            gameObject.SetActive(false); // Hide coin after collection
        }
    }
}
