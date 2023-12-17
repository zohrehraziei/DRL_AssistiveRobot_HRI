using System;
using System.Data.Common;
using System.Linq;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.MLAgents.Actuators;
using Random = UnityEngine.Random;

public class RobotControllerAgent : Agent
{
    [SerializeField]
    private Axis[] armAxes; // robot arms
    [SerializeField]
    private GameObject endEffector; // Rbot End Position

    public bool trainingMode; // Traning Mode
    
    public GameObject nearestComponent; // Target Position
    public Transform HumanHead; // Human Head
    
    private float[] angles = new float[7]; // Angles for Robot 7 Axis
    
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
    [SerializeField]
    float Axis6Rotation;
    private void Start()
    {
        if( trainingMode == false)  Time.timeScale = 0.3f;
        ResetAllAxis(); // Reset Axis Rotation with Initial Rotation
        MoveToSafeRandomPosition(); //
        //if (!trainingMode) MaxStep = 0;
    }

    bool Reached = false;
    float CupRotate = 0;
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxis("Axis0");
        continuousActions[1] = Input.GetAxis("Axis1");
        continuousActions[2] = Input.GetAxis("Axis2");
        continuousActions[3] = Input.GetAxis("Axis3");
        continuousActions[4] = Input.GetAxis("Axis4");
        continuousActions[5] = Input.GetAxis("Axis5");
        continuousActions[6] = Input.GetAxis("Axis6");
    }

    public override void Initialize()
    {

    }

    private void ResetAllAxis()
    {
        for (int i = 0; i < armAxes.Length; i++)
        {
            armAxes[i].InitAngle();
        }
    }

    /*
     * “OnEpisodeBegin”. The ML Agent trainings are executed in episodes. A default episode setting for 5000 cycles of training. “OnEpisodeBegin” is the place where you reset old episodes and initialize a new episode. In our case, we reset the axis rotations, randomise the axis rotations and place the target component in a random location.
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
    private void UpdateNearestComponent() // Reset Head rotation and target position when Training
    {

        if (trainingMode)
        {
            nearestComponent.transform.localPosition = new Vector3(Random.Range(-0.1f, 0.04f), Random.Range(1.22f, 1.3f), Random.Range(0.29f, 0.32f));
            //HumanHead.localEulerAngles = new Vector3(Random.Range(-5f, 0f), Random.Range(-30, 30f), Random.Range(-10f, 10f));
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
        sensor.AddObservation(angles); // 7 Axis Angles
        sensor.AddObservation(transform.position.normalized); // Robot Position
        sensor.AddObservation(nearestComponent.transform.position.normalized); // Target Position Normalized
        sensor.AddObservation(endEffector.transform.TransformPoint(Vector3.zero).normalized); // normalized world position of endEffector
        Vector3 toComponent = (nearestComponent.transform.position - endEffector.transform.TransformPoint(Vector3.zero)); 
        sensor.AddObservation(toComponent.normalized); // Distance normalized
        sensor.AddObservation(Vector3.Distance(nearestComponent.transform.position, endEffector.transform.TransformPoint(Vector3.zero)));
        sensor.AddObservation(StepCount / 5000);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        if( trainingMode == false) // Drinking or Seeding function
        {
            if( Reached)
            {
                CupRotate -= 0.1f;
                if (CupRotate < -1) CupRotate = -1;
                armAxes[6].GetComponent<Axis>().RotateAngle(CupRotate, Axis6Rotation);
                return;
            }
        }
        // Set the Axis from sequence of angles
        angles = vectorAction;
        armAxes[0].GetComponent<Axis>().RotateAngle(angles[0], 10);
        armAxes[1].GetComponent<Axis>().RotateAngle(angles[1], 10);
        armAxes[2].GetComponent<Axis>().RotateAngle(angles[2], 10);
        armAxes[3].GetComponent<Axis>().RotateAngle(angles[3], 10);
        armAxes[4].GetComponent<Axis>().RotateAngle(angles[4], 10);
        armAxes[5].GetComponent<Axis>().RotateAngle(angles[5], 10);
        armAxes[6].GetComponent<Axis>().RotateAngle(angles[6], 10);
        //if (trainingMode)
        {
            // Translate the floating point actions into Degrees of rotation for each axis
          
            float distance = Vector3.Distance(endEffector.transform.TransformPoint(Vector3.zero), nearestComponent.transform.position);
            float diff = beginDistance - distance;

            if (distance > prevBest)
            {
                // Penalty if the arm moves away from the closest position to target
                AddReward(prevBest - distance);
            }
            else
            {
                // Reward if the arm moves closer to target
                AddReward(diff);
                prevBest = distance; 
            }
            AddReward(stepPenalty);
        }
       
    }

    // None reached Penalty
    public void GroundHitPenalty()
    {
        AddReward(-1f);
        EndEpisode();
        if( trainingMode == false)
        {
            MaxStep = 0;
            Reached = true; // Stop Criteria
        }
        UpdateMateraial(false);
    }

    // Reached Reward
    public void JackpotReward(GameObject other)
    {
        if (other.transform.CompareTag("Target"))
        {
            float SuccessReward = 0.5f;
            float bonus = Mathf.Clamp01(Vector3.Dot(nearestComponent.transform.up.normalized,
                             endEffector.transform.up.normalized));
            float reward = SuccessReward + bonus;
            if (float.IsInfinity(reward) || float.IsNaN(reward)) return;
            Debug.LogWarning("Great! Component reached. Positive reward:" + reward);

            if( trainingMode == false )  Reached = true;
            AddReward(reward);
            UpdateNearestComponent();

            UpdateMateraial(true);
        }else if(other.gameObject.CompareTag("Mouse"))  // 
        {
            float reward = 1.0f;
            
            AddReward(reward);
            
            UpdateMateraial(true);
            if (trainingMode == false)
            {
                if (endEffector.GetComponent<EndEffector>().WaterRemaining() == false)
                {
                    Reached = true; // Stop Criteria
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
            armAxes.All(axis =>
               {
                   Axis ax = axis.GetComponent<Axis>();
                   float angle = Random.Range(ax.MinAngle, ax.MaxAngle);
                   ax.RotateAngle(angle, 10);
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
