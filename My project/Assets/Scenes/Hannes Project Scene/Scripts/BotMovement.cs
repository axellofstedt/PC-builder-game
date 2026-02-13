using UnityEngine;

public class BotMovement : MonoBehaviour
{
    public bool IsAtTarget => Vector3.Distance(transform.position, targetPosition) <= ArrivalThreshold;

    [SerializeField] private float speed = 5f;
    private const float ArrivalThreshold = 0.1f;

    private Vector3 targetPosition;
    private int queueIndex;

    private BotState currentState = BotState.Moving;

    private enum BotState
    {
        Moving,
        Idle,
        Ordering
    }

    private void Update()
    {
        switch (currentState)
        {
            case BotState.Moving:
                HandleMovement();
                break;

            case BotState.Idle:
                HandleIdle();
                break;

            case BotState.Ordering:
                HandleOrdering();
                break;
        }
    }

    private void HandleMovement()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, targetPosition) <= ArrivalThreshold)
        {
            SetState(BotState.Idle);
        }
    }

    private void HandleIdle()
    {
        // animations and stuff
    }

    public void SetOrderingState()
    {
        SetState(BotState.Ordering);
    }

    private void HandleOrdering()
    {
        // Ordiering animations, Play Once?
    }

    private void SetState(BotState newState)
    {
        currentState = newState;
    }

    public void SetTarget(Transform waypoint, int newQueueIndex, float queueDistance)
    {
        queueIndex = newQueueIndex;

        Vector3 backDirection = -waypoint.forward;

        targetPosition =
            waypoint.position +
            backDirection * queueIndex * queueDistance;

        targetPosition.y = transform.position.y;

        if (Vector3.Distance(transform.position, targetPosition) > ArrivalThreshold)
            SetState(BotState.Moving);

        // Rätt riktning direkt
        if (queueIndex == 0) transform.forward = waypoint.forward; 
        else transform.forward = -backDirection;
    }
}
