using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    public int coinCount = 0;
    public TextMeshPro coinText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddCoin()
    {
        coinCount++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (coinText != null)
            coinText.text =  coinCount.ToString(); // ✅ Fix is here
    }
}
