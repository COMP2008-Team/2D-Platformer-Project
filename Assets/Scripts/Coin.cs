using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{
    //audio clip for coin
    public AudioClip coinClip;
    public int coinsToGive = 1;
    public TextMeshProUGUI coinText;  // FIXED

    void Start()
    {
        // Find the CoinText UI in the scene by tag
        coinText = GameObject.FindWithTag("CoinText").GetComponent<TextMeshProUGUI>(); // FIXED
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Add to player coin count
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.coin += coinsToGive;
                player.PlaySFX(coinClip, 0.4f);

                // Update UI
                coinText.text = player.coin.ToString();
            }

            // Destroy the coin object
            Destroy(gameObject);
        }
    }
}



