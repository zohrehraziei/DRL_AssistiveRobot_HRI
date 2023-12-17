using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCS : MonoBehaviour
{
    public RobotControllerAgent robotAgent;
    Vector3 InitLocalPostion;

    private void Awake()
    {
        InitLocalPostion = transform.localPosition;
    }
   
    public void InitPosition()
    {
        transform.localPosition = InitLocalPostion;
    }

    private void OnCollisionStay(Collision collision)
    {
        if( collision.gameObject.CompareTag("Wall"))
        {
            robotAgent.GroundHitPenalty();
            gameObject.SetActive(false);
        }else if(collision.gameObject.CompareTag("Mouse"))
        {
            robotAgent.JackpotReward(collision.gameObject);
            gameObject.SetActive(false);
        }
        
    } 

    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.CompareTag("Wall"))
        {
            robotAgent.GroundHitPenalty();
            gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Mouse"))
        {
            robotAgent.JackpotReward(other.gameObject);
            gameObject.SetActive(false);
        }

    }
}
