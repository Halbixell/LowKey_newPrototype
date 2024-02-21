using UnityEngine;

public class CollectibleRaven : MonoBehaviour
{
    public float idleAmplitude = 0.1f;
    public float idleSpeed = 1.0f;
    public float flyAwaySpeed = 5.0f;
    public Transform flyAwayDestination;

    private Vector3 initialPosition;
    //private bool isMoving = false;
    private bool FlownAway = false;

    public GameObject FlyingRaven;
    public bool isTriggered = false;
    public RavenCounter _ravenCounter;
    public int RavenIndex = 1;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (!FlownAway)
        {
            
            Idle();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            FlyAway();
            _ravenCounter.CollectRaven(RavenIndex-1);
        }
    }

    private void FlyAway()
    {
        if (!isTriggered)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Instantiate(FlyingRaven, transform.position, transform.rotation);
            isTriggered = true;
            FlownAway = true;
        }
    }

    private void Idle()
    {
        // Simple hopping idle animation
        float idleOffset = idleAmplitude * Mathf.Sin(idleSpeed * Time.time);
        
        transform.position = initialPosition + Vector3.up * idleOffset;
    }
}