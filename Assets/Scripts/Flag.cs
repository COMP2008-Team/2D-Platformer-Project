using UnityEngine;

public class Flag : MonoBehaviour
{

    public GameObject winUI;   // The Win UI panel you will assign in Inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
           Time.timeScale = 0;
            // Show the Win UI
            winUI.SetActive(true);
        }
    }
}
