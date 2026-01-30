using TMPro;
using UnityEngine;

public class BotMovement : MonoBehaviour
{
    public float speed = 5f;

    private Vector3 targetPosition;
    private int queueIndex;

    private BotInteractive botInteractive;

    public enum BotState
    {
        Moving,
        Idle,
        Ordering
    }

    private BotState currentState = BotState.Moving;

    private void Awake()
    {
        botInteractive = GetComponent<BotInteractive>();
    }

    private void Update()
    {
        if (currentState == BotState.Moving) MoveToTarget();
        else if (currentState == BotState.Idle && queueIndex == 0) currentState = BotState.Ordering;

        if (currentState == BotState.Ordering) Order();
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentState = BotState.Idle;
        }
    }

    private void Order()
    {
        botInteractive.Interactable = true;

    }

    public void SetTarget(Transform waypoint, int newQueueIndex, float queueDistance)
    {
        queueIndex = newQueueIndex;

        // Riktning FRÅN waypoint bakåt i kön
        Vector3 backDirection = -waypoint.forward;

        targetPosition = waypoint.position + backDirection * queueIndex * queueDistance;

        targetPosition.y = transform.position.y;

        // Vänd botten direkt rätt
        if (queueIndex == 0)
            transform.forward = waypoint.forward;
        else
            transform.forward = -backDirection;
    }
}
