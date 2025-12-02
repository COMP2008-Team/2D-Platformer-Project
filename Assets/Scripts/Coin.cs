using UnityEngine;

public class Coin : MonoBehaviour
{
    //audio clip for coin
    public AudioClip coinClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            // Add to player coin count
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.coin += 1;
                player.PlaySFX(coinClip, 0.4f);
            }
            // Destroy the coin
            Destroy(gameObject);
        }
    }

}
