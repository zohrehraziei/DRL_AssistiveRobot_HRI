using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
public class MoveToGoal : Agent
{
    // Start is called before the first frame update
    [SerializeField] private Transform targetTransform;
    [SerializeField] private MeshRenderer floorMeshRenderer;
    [SerializeField] private Material winMaterial;
    [SerializeField] private Material lossMaterial;
    [SerializeField] private Material normalMaterial;

    public override void OnEpisodeBegin()
    {
        floorMeshRenderer.material = normalMaterial;
        targetTransform.localPosition = new Vector3(Random.Range(-3.7f, 3.7f), 1, Random.Range(-4.0f, 4.0f));
        transform.localPosition = new Vector3(Random.Range(-3.7f, 3.7f), 1, Random.Range(-4.0f, 4.0f));
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxis("Horizontal");
        continuousActions[1] = Input.GetAxis("Vertical");
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(targetTransform.localPosition);
        sensor.AddObservation(transform.localPosition);
        
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * 4;
    }

    public void Win()
    {
        floorMeshRenderer.material = winMaterial;
    }
    public void Lose()
    {
        floorMeshRenderer.material = lossMaterial;
    }

    public void OnTriggerEnter(Collider other)
    {
        if( other.gameObject.tag == "Food")
        {
            SetReward(1f);
            EndEpisode();
            Win();
        }else if( other.gameObject.tag == "Wall")
        {
            SetReward(-1f);
            EndEpisode();
            Lose();
        }
    }
}
