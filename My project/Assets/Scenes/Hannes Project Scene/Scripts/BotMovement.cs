using System.Collections.Generic;
using UnityEngine;

public class BotMovement : MonoBehaviour
{
    public float speed = 2f;
    public float rotationSpeed = 360f;

    private Quaternion targetRotation;

    private int waypointIndex = 0;
    private List<Transform> waypoints;

    private int queueIndex;
    private float spacing;
    private Transform currentTarget;

    private State state = State.Moving;

    public bool IsReadyToOrder { get { return state == State.Queuing && queueIndex == 0; } }

    public void Init(List<Transform> waypoints, int queueIndex, float spacing)
    {
        this.waypoints = waypoints;
        this.queueIndex = queueIndex;
        this.spacing = spacing;
        this.currentTarget = waypoints[0];
    }

    enum State
    {
        Moving,
        Rotating,
        Queuing,
        Ordering
    }

    private void Update()
    {
        switch (state)
        {
            case State.Moving:
                UpdateMoving();
                break;

            case State.Rotating:
                UpdateRotating();
                break;

            case State.Queuing:
                UpdateQueuing();
                break;

            case State.Ordering:
                UpdateOrdering();
                break;
        }

    }

    private void UpdateMoving()
    {
        Vector3 dir = currentTarget.position - transform.position;
        dir.y = 0;

        float distance = CalculateDistance();

        if (distance <= queueIndex * spacing)
        {
            state = State.Queuing;
            return;
        }

        if (dir.magnitude < 0.1f) Advance();

        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
    }

    void Advance()
    {
        waypointIndex++;

        if (waypointIndex >= waypoints.Count)
        {
            state = State.Queuing;
        }
        else
        {
            currentTarget = waypoints[waypointIndex];

            Vector3 dir = currentTarget.position - transform.position;
            dir.y = 0;

            targetRotation = Quaternion.LookRotation(dir);
            state = State.Rotating;
        }
    }

    private float CalculateDistance()
    {
        float distance = 0f;
        for (int i = waypointIndex; i < waypoints.Count - 1; i++)
        {
            float segmentDistance = Vector3.Distance(waypoints[i].position, waypoints[i + 1].position);
            distance += segmentDistance;
        }
        distance += Vector3.Distance(transform.position, currentTarget.position);
        return distance;
    }

    void UpdateRotating()
    {
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );

        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.5f)
        {
            transform.rotation = targetRotation;
            state = State.Moving;
        }
    }

    private void UpdateQueuing()
    {
        // Stå still i kön
    }

    private void UpdateOrdering()
    {
        // Order Animation once
    }

    public void SetQueueIndex(int newIndex)
    {
        queueIndex = newIndex;
        UpdateQueueTarget();
    }

    void UpdateQueueTarget() { state = State.Moving; }
    public void SetOrderingState() { state = State.Ordering; }

}
