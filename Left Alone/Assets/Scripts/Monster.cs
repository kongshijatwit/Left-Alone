using System;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public enum State { ROAM, CHASE, CATCH };
    private State currentState = State.ROAM;

    public static Action OnCaughtPlayer = delegate { };

    NavMeshAgent enemyAgent;
    EnemyDetection detectionField;
    Collision collisionData;

    [SerializeField] Transform roomContainer = null;
    Transform currentDestination = null;
    Vector3 lastKnown;

    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        detectionField = GetComponentInChildren<EnemyDetection>();
        FindNewRoom();
    }

    void Update()
    {
        switch (currentState)
        {
            case State.ROAM:
                HandleRoaming();
                break;
            
            case State.CHASE:
                ChasePlayer();
                break;

            case State.CATCH:
                CaughtPlayer();
                break;

            default:
                Debug.LogWarning("It seems that I have added an unregistered state mwahaha, have fun with your bugs now spiderman");
                break;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collisionData = other;
            currentState = State.CATCH;
        }
    }

    void HandleRoaming()
    {
        // Snapshot of current position
        Vector3 currentPos = transform.position;
        currentPos.y = currentDestination.position.y;

        // Arrived at destination
        float lastKnownDistance = (lastKnown - currentPos).magnitude;
        float destinationDistance = (currentDestination.position - currentPos).magnitude;
        if (destinationDistance < 0.75f || lastKnownDistance < 0.75f)
        {
            FindNewRoom();
        }

        // Player detection
        if (detectionField.FoundPlayer())
        {
            currentState = State.CHASE;
        }
    }

    public void ChasePlayer()
    {
        currentDestination = detectionField.GetDetectedData();
        lastKnown = currentDestination.position;
        enemyAgent.SetDestination(currentDestination.position);

        if (!detectionField.FoundPlayer())
        {
            currentState = State.ROAM;
        }
    }

    void CaughtPlayer()
    {
        if (collisionData == null) 
        {
            return;
        }

        // Stop agent after collision
        enemyAgent.velocity = Vector3.zero;
        enemyAgent.GetComponent<Rigidbody>().isKinematic = true;  // Temporary until i find a jumpscare animation
        enemyAgent.isStopped = true;

        // Make player face the monster
        Vector3 temp = transform.position - collisionData.transform.position;
        temp.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(temp);
        collisionData.transform.rotation = lookRotation;

        // Play jumpscare if we somehow find one

        // GameOver flag
        OnCaughtPlayer();
    }
    
    void FindNewRoom()
    {
        int randomChild = UnityEngine.Random.Range(0, roomContainer.childCount - 1);
        currentDestination = roomContainer.GetChild(randomChild);
        lastKnown = currentDestination.position;
        enemyAgent.SetDestination(currentDestination.position);
    }
}