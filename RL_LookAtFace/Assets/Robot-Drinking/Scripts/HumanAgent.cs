using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
public class HumanAgent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Transform headTransform;

    [SerializeField]
    private GameObject GroundMaterial;
    [SerializeField]
    private Material NormalMaterail;
    [SerializeField]
    private Material WinMaterial;
    [SerializeField]
    private Material LoseMaterial;
    [SerializeField]
    private void Awake()
    {
        //Debug.Log(transform.localEulerAngles);
        //Debug.Log(headTransform.localEulerAngles);
        //Debug.Log(targetTransform.localPosition);
    }
    //public override void OnEpisodeBegin()
    //{
    //    targetTransform.localPosition = new Vector3(Random.Range(-0.03f, 0.03f), Random.Range(1.25f, 1.283f), Random.Range(0.284f, 0.311f));
    //    transform.localEulerAngles = new Vector3(Random.Range(-20f, 20f), Random.Range(-40, 40f), Random.Range(-20f, 20f));
    //    headTransform.localEulerAngles = new Vector3(Random.Range(-20f, 20f), Random.Range(-40, 40f), Random.Range(-20f, 20f));

    //    InitMaterial();
    //}
    private void Update()
    {
        transform.LookAt(targetTransform);
    }
    //public override void Heuristic(in ActionBuffers actionsOut)
    //{
    //    ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
    //    continuousActions[0] = Input.GetAxis("Horizontal");
    //    continuousActions[1] = Input.GetAxis("Vertical");
    //    continuousActions[2] = Input.GetAxis("Horizontal");
    //    continuousActions[3] = Input.GetAxis("Horizontal");
    //    continuousActions[4] = Input.GetAxis("Horizontal");
    //    continuousActions[5] = Input.GetAxis("Horizontal");
    //}
    //public override void CollectObservations(VectorSensor sensor)
    //{
    //    sensor.AddObservation(transform.localEulerAngles);
    //    sensor.AddObservation(headTransform.localEulerAngles);
    //    sensor.AddObservation(targetTransform.localPosition);
    //}
    //public void InitMaterial()
    //{
    //    GroundMaterial.GetComponent<MeshRenderer>().material = NormalMaterail;
    //}

    ////Update Floor materail when reached or far away
    //public void UpdateMateraial(bool reached)
    //{
    //    if (reached)
    //    {
    //        GroundMaterial.GetComponent<MeshRenderer>().material = WinMaterial;
    //        MaxStep = 0;
    //    }
    //    else
    //    {
    //        GroundMaterial.GetComponent<MeshRenderer>().material = LoseMaterial;
    //    }
    //}
    //public override void OnActionReceived(ActionBuffers actions)
    //{
    //    float moveX = actions.ContinuousActions[0];
    //    float moveY = actions.ContinuousActions[1];
    //    float moveZ = actions.ContinuousActions[2];
    //    float moveX1 = actions.ContinuousActions[3];
    //    float moveY1 = actions.ContinuousActions[4];
    //    float moveZ1 = actions.ContinuousActions[5];
    //    transform.localEulerAngles += new Vector3(moveX, moveY, moveZ) * Time.deltaTime * 4;
    //    headTransform.localEulerAngles += new Vector3(moveX1, moveY1, moveZ1) * Time.deltaTime * 4;

    //    DecideStopStates();
    //}

    //public void DecideStopStates()
    //{
    //    Vector3 rot1 = transform.localEulerAngles;
    //    Vector3 rot2 = headTransform.localEulerAngles;

    //    if (rot1.x < -60 || rot1.x > 60 || rot1.y < -60 || rot1.y > 60 || rot1.z < -60 || rot1.z > 60)
    //    {
    //        SetReward(-1f);
    //        EndEpisode();
    //        UpdateMateraial(false);
    //    }
    //    if (rot2.x < -60 || rot2.x > 60 || rot2.y < -60 || rot2.y > 60 || rot2.z < -60 || rot2.z > 60)
    //    {
    //        SetReward(-1f);
    //        EndEpisode();
    //        UpdateMateraial(false);
    //    }

    //}


}
