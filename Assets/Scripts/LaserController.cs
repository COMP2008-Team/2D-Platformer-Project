using UnityEngine;

public class LaserController : MonoBehaviour
{
    public GameObject leftLaserHeadIdle;
    public GameObject leftLaserHeadShoot;
    public GameObject leftLaserBeam;
    
    public GameObject rightLaserHeadIdle;
    public GameObject rightLaserHeadShoot;
    public GameObject rightLaserBeam;

    public float fireInterval = 3f;
    public float shootDuration = 1f;
    private float timer = 0f;

    public float speed = 2f;
    public Transform[] points;
    private int i = 0;

    void Start()
    {
        leftLaserHeadIdle.SetActive(true);
        leftLaserHeadShoot.SetActive(false);
        leftLaserBeam.SetActive(false);
        rightLaserHeadIdle.SetActive(true);
        rightLaserHeadShoot.SetActive(false);
        rightLaserBeam.SetActive(false);
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.25f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);

        timer += Time.deltaTime;
        if (timer >= fireInterval)
        {
            StartCoroutine(FireLaser());
            timer = 0;
        }
    }

    private System.Collections.IEnumerator FireLaser()
    {
        leftLaserHeadIdle.SetActive(false);
        leftLaserHeadShoot.SetActive(true);
        leftLaserBeam.SetActive(true);

        rightLaserHeadIdle.SetActive(false);
        rightLaserHeadShoot.SetActive(true);
        rightLaserBeam.SetActive(true);

        yield return new WaitForSeconds(shootDuration);

        leftLaserHeadShoot.SetActive(false);
        leftLaserHeadIdle.SetActive(true);
        leftLaserBeam.SetActive(false);

        rightLaserHeadShoot.SetActive(false);
        rightLaserHeadIdle.SetActive(true);
        rightLaserBeam.SetActive(false);
    }
}
