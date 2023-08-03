using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemyDetection : MonoBehaviour
{
    Monster monster;

    Transform detectedData;
    bool foundPlayer = false;

    void Start()
    {
        monster = GetComponentInParent<Monster>();
    }

    void OnTriggerStay(Collider other)
    {
        // Raycast Setup
        Vector3 origin = transform.position;
        Vector3 direction = (other.GetComponent<Transform>().position - transform.position).normalized;
        float distance = (other.GetComponent<Transform>().position - transform.position).magnitude;

        // Raycast
        if (Physics.Raycast(origin, direction, out RaycastHit hit, distance))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                detectedData = hit.collider.transform;
                foundPlayer = true;
            }
        }

        Debug.DrawRay(origin, direction*distance, Color.red);  // DEBUG PURPOSES ONLY
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            foundPlayer = false;
        }
    }

    public bool FoundPlayer()
    {
        return foundPlayer;
    }

    public Transform GetDetectedData()
    {
        return detectedData;
    }
}