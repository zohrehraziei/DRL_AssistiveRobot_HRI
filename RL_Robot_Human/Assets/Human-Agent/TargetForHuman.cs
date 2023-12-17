using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetForHuman : MonoBehaviour
{
    // Start is called before the first frame update
    public HumanAgentController humanAgent;
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {

       if (other.transform.CompareTag("Wall"))
        {
            if (humanAgent != null)
                humanAgent.GroundHitPenalty();
        }else if(other.transform.CompareTag("Head"))
        {
            if (humanAgent != null)
                humanAgent.GroundHitPenalty();
        }
 
       
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Wall"))
        {
            if (humanAgent != null)
                humanAgent.GroundHitPenalty();
        }
        else if (other.transform.CompareTag("Head"))
        {
            if (humanAgent != null)
                humanAgent.GroundHitPenalty();
        }


    }
}
 