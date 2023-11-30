using UnityEngine;
using UnityEngine.AI;

public class agentRun : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] points;
    private int destinationIndex;

    public Transform player;
    public int playerDestinationIndex = 2;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        destinationIndex = Random.Range(0, points.Length);
        agent.autoBraking = false;

        if (points.Length > 0)
        {
            SetDestination();
        }
    }
    private void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            SetDestination();

            if (destinationIndex == playerDestinationIndex)
            {
                agent.SetDestination(points[playerDestinationIndex-1].position);
                agent.autoBraking = true;
            }
        }
    }
    private void SetDestination()
    {
        if (points.Length == 0)
            return;

        destinationIndex = Random.Range(0, points.Length);
        agent.SetDestination(points[destinationIndex].position);
    }
}