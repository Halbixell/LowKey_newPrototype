using UnityEngine;

public class CollectibleRaven : MonoBehaviour
{
    public float idleAmplitude = 0.1f;
    public float idleSpeed = 1.0f;
    public float flyAwaySpeed = 5.0f;
    public Transform flyAwayDestination;

    private Vector3 initialPosition;
    private bool isPlayerInRange = false;
    private bool isMoving = false;
    private bool FlownAway = false;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (!isMoving && !FlownAway)
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
        }
    }

    private void FlyAway()
    {
        // Play your fly away animation here if needed

        // Choose a direction (Up-left or up-right)
        Vector3 flyAwayDirection = (Random.Range(0, 2) == 0) ? Vector3.up + Vector3.left : Vector3.up + Vector3.right;

        // Move towards the fly away destination
        transform.position = Vector3.MoveTowards(transform.position, flyAwayDestination.position, flyAwaySpeed * Time.deltaTime);

        // Optionally, rotate the bird towards the fly away direction
        transform.up = flyAwayDirection;

        // Destroy the bird when it reaches the destination (you can customize this condition)
        if (Vector3.Distance(transform.position, flyAwayDestination.position) < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    private void Idle()
    {
        // Simple hopping idle animation
        float idleOffset = idleAmplitude * Mathf.Sin(idleSpeed * Time.time);
        transform.position = initialPosition + Vector3.up * idleOffset;
    }
}