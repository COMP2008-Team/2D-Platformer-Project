using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{
    //audio clip for coin
    public AudioClip coinClip;
    public int coinsToGive = 1;
    public TextMeshProGUI coinText;

    void Start()
    {
        // ... your other Start code (movement, health, etc.)

       coinText= GameObject.FindWithTag("CoinText").GetComponent<TextMeshProGUI>();
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            // Add to player coin count
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.coin += coinsToGive;
                player.PlaySFX(coinClip, 0.4f);
                coinText.text =player.coin.ToString();
            }
            // Destroy the coin
            Destroy(gameObject);
        }
    }

}
