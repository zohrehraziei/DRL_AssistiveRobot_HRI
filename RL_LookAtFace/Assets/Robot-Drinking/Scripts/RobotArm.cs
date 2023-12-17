using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
public class RobotArm : MonoBehaviour
{
    public RobotControllerAgent robotAgent;

    private void OnTriggerEnter(Collider other)
    {

        robotAgent.GroundHitPenalty();

    }

    private void OnTriggerStay(Collider other)
    {
        robotAgent.GroundHitPenalty();
    }
}
