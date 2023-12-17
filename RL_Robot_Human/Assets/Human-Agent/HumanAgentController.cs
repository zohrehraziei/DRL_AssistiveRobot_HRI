using System;
using System.Data.Common;
using System.Linq;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.MLAgents.Actuators;
using Random = UnityEngine.Random;

public class HumanAgentController : Agent
{
    [SerializeField]
    private Axis[] SpinAxis; // Spines
    [SerializeField]
    private GameObject endEffector; // mouse End Position

    public bool trainingMode; // Traning Mode
    
    public GameObject nearestComponent; // Target Position
    
    private float[] angles = new float[3]; // Angles for Spin Axis
    
    private float beginDistance;
    private float prevBest;
    private float baseAngle;
    private const float stepPenalty = -0.0001f;
    [SerializeField]
    private GameObject GroundMaterial;
    [SerializeField]
    private Material NormalMaterail;
    [SerializeField]
    private Material WinMaterial;
    [SerializeField]
    private Material LoseMaterial;
    private void Start()
    {
        if( trainingMode == false)  Time.timeScale = 0.3f;
        ResetAllAxis(); // Reset Axis Rotation with Initial Rotation
        MoveToSafeRandomPosition(); //
        //if (!trainingMode) MaxStep = 0;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxis("Horizontal");
        continuousActions[1] = Input.GetAxis("Vertical");
        continuousActions[2] = Input.GetAxis("Vertical");
    }

    public override void Initialize()
    {

    }

    private void ResetAllAxis()
    {
        for (int i = 0; i < SpinAxis.Length; i++)
        {
            SpinAxis[i].InitAngle();
        }
    }

    /*
     * “OnEpisodeBegin”. The IRL Agent trainings are executed in episodes. A default episode setting for 5000 cycles of training.
     * “OnEpisodeBegin” is the place where you reset old episodes and initialize a new episode. In our case, we reset the axis rotations, 
     * randomise the axis rotations and place the target component in a random location.
     */
    public override void OnEpisodeBegin()
    {
        endEffector.GetComponent<EndEffector>().WaterInit(); // Water or Seed reset
        if (trainingMode)
        {
            ResetAllAxis(); // Robot Axis Reset
            MoveToSafeRandomPosition(); // Random Position
            UpdateNearestComponent(); // Reset Target Position
            
            InitMaterial(); // Reset Matarial
        }
    }

    // Reset Material to Normal Floor
    public void InitMaterial()
    {
        GroundMaterial.GetComponent<MeshRenderer>().material = NormalMaterail;
    }

    //Update Floor materail when reached or far away
    public void UpdateMateraial(bool reached)
    {
        if (reached)
        {
            GroundMaterial.GetComponent<MeshRenderer>().material = WinMaterial;
            MaxStep = 0;
        }
        else
        {
            GroundMaterial.GetComponent<MeshRenderer>().material = LoseMaterial;
        }
    }
    private void UpdateNearestComponent() // Reset first distance between endeffector and targetforhuman, and target position when Training
    {

        if (trainingMode)
        {
            nearestComponent.transform.localPosition = new Vector3(Random.Range(-0.073f, 0.043f), Random.Range(1.168f, 1.3f), Random.Range(0.35f , 0.40f));
        }
        beginDistance = Vector3.Distance(endEffector.transform.TransformPoint(Vector3.zero), nearestComponent.transform.position);
        prevBest = beginDistance;

        baseAngle = Mathf.Atan2(transform.position.x - nearestComponent.transform.position.x, transform.position.z - nearestComponent.transform.position.z) * Mathf.Rad2Deg;
        if (baseAngle < 0) baseAngle = baseAngle + 360f;
    }

    /// <summary>
    /// Markov Decision Process - Observes state for the current time step
    /// </summary>
    /// <param name="sensor"></param>
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(angles); // 3 Axis Angles
        sensor.AddObservation(transform.position.normalized); // Human Position
        sensor.AddObservation(nearestComponent.transform.position.normalized); // Target Position Normalized
        sensor.AddObservation(endEffector.transform.TransformPoint(Vector3.zero).normalized); // normalized world position of endEffector
        Vector3 toComponent = (nearestComponent.transform.position - endEffector.transform.TransformPoint(Vector3.zero)); 
        sensor.AddObservation(toComponent.normalized); // Distance normalized
        sensor.AddObservation(Vector3.Distance(nearestComponent.transform.position, endEffector.transform.TransformPoint(Vector3.zero)));
        sensor.AddObservation(StepCount / 5000);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        angles = vectorAction;
        SpinAxis[0].GetComponent<Axis>().RotateAngle(angles[0], 20);
        SpinAxis[1].GetComponent<Axis>().RotateAngle(angles[1], 20);
        SpinAxis[2].GetComponent<Axis>().RotateAngle(angles[2], 20);
        float distance = Vector3.Distance(endEffector.transform.TransformPoint(Vector3.zero), nearestComponent.transform.position);
        float diff = beginDistance - distance;

        if (distance > prevBest)
        {
            // Penalty if the spine moves away from the closest position to target
            AddReward(prevBest - distance);
        }
        else
        { 
            // Reward if the spine moves closer to target
            AddReward(diff);
            prevBest = distance;
        }
        AddReward(stepPenalty); 
    }

    // None reached Penalty
    public void GroundHitPenalty()
    {
        AddReward(-1f);
        EndEpisode();
        if( trainingMode == false)
        {
            MaxStep = 0;
        }
        UpdateMateraial(false);
    }

    // Reached Reward
    public void JackpotReward(GameObject other)
    {
        if (other.transform.CompareTag("TargetForHuman"))
        {
            float SuccessReward = 0.5f;
            float bonus = Mathf.Clamp01(Vector3.Dot(nearestComponent.transform.up.normalized,endEffector.transform.up.normalized));
            float reward = SuccessReward + bonus;
            if (float.IsInfinity(reward) || float.IsNaN(reward)) return;
            Debug.LogWarning("Great! Component reached. Positive reward:" + reward);

            AddReward(reward);
            UpdateNearestComponent();

            UpdateMateraial(true);
        }else if(other.gameObject.CompareTag("Cup"))  // 
        {
            float reward = 1.0f;
            
            AddReward(reward);
            
            UpdateMateraial(true);
            if (trainingMode == false)
            {
                if (endEffector.GetComponent<EndEffector>().WaterRemaining() == false)
                {
                    Invoke("Restart", 3);
                }
            }
        }
    }
    public int SceneID;
    void Restart()
    {
        SceneManager.LoadSceneAsync(SceneID);
    }

    private void MoveToSafeRandomPosition() // Rotate Random
    {
        int maxTries = 100;

        while (maxTries > 0)
        {
            SpinAxis.All(axis =>
               {
                   Axis ax = axis.GetComponent<Axis>();
                   float angle = Random.Range(ax.MinAngle, ax.MaxAngle);
                   ax.RotateAngle(angle, 20);
                   return true;
               }
            );
            Vector3 tipPosition = endEffector.transform.TransformPoint(Vector3.zero);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float distanceFromGround = groundPlane.GetDistanceToPoint(tipPosition);
            if (distanceFromGround > 0.1f && distanceFromGround <= 1f && tipPosition.y > 0.01f)
            {
                break;
            }
            maxTries--;
        }
    }
    private void Update()
    {
        if (nearestComponent != null)
            Debug.DrawLine(endEffector.transform.position, nearestComponent.transform.position, Color.green);
    }
}
