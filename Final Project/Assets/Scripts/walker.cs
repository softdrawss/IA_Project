using UnityEngine;
using UnityEngine.AI;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class Walker : Agent
{
    NavMeshAgent navMeshAgent;
    float episodeTimer;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public Transform Target;
    public override void OnEpisodeBegin()
    {
        episodeTimer = 0f;
        
        this.transform.localPosition = new Vector3(0.4231435f, 5.572f, -28.06085f);
        
        Target.localPosition = new Vector3(0.9f, 5.037637f, 3.78f);

       
        navMeshAgent.SetDestination(Target.position);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        // Agent velocity
        sensor.AddObservation(navMeshAgent.velocity.x);
        sensor.AddObservation(navMeshAgent.velocity.z);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        Vector3 targetPosition = new Vector3(actionBuffers.ContinuousActions[0], this.transform.position.y, actionBuffers.ContinuousActions[1]);
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, navMeshAgent.speed * Time.deltaTime);

        // Rewards
        float distanceToTarget = Vector3.Distance(this.transform.position, Target.position);

        // Reached target
        if (distanceToTarget < 1.42f)
        {
            SetReward(1.0f);
            EndEpisode();
        }

        // Fell off platform or reached max time
        else if (this.transform.localPosition.y < 0 || episodeTimer >= 60f)
        {
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
}