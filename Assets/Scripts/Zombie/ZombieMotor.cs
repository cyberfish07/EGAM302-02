using UnityEngine;
using UnityEngine.AI;

public class ZombieMotor : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 position)
    {
        navMeshAgent.SetDestination(position);
    }

    public void Stop()
    {
        navMeshAgent.ResetPath();
    }

    public void FaceTowards(Vector3 position)
    {
        Vector3 direction = new Vector3(position.x, transform.position.y, position.z);
        transform.LookAt(direction);
    }
    public bool HasArrived()
    {
        if (navMeshAgent.pathPending) return false;
        return navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance;
    }
}
