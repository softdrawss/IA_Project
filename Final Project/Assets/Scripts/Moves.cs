using UnityEngine;
using UnityEngine.AI;

public class Moves : MonoBehaviour
{
    private float radius = 1;
    private float offset = 1;

    private NavMeshHit hit;
    private Vector3 tempTarget;
    private Vector3 worldTarget;

    public GameObject[] obstacles;


    public Vector3 Hide(NavMeshAgent hideFrom)
    {
        GameObject closestObj = null;
        float closestDist = -1;
        foreach (GameObject item in obstacles)
        {
            float temp = Vector3.Distance(hideFrom.transform.position, item.transform.position);
            if (closestDist == -1 || temp < closestDist)
            {
                closestObj = item;
                closestDist = temp;
            }
        }

        // Compute position at the other side of closest object from police
        float RelDist = 1.65f / closestDist;
        Vector3 lerpPoint = Vector3.Lerp(closestObj.transform.position, hideFrom.transform.position, RelDist);
        //agent.destination = closestObj.transform.position + (closestObj.transform.position - lerpPoint);

        return closestObj.transform.position + (closestObj.transform.position - lerpPoint);
    }
}