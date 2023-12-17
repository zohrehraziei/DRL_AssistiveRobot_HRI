using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMouse : MonoBehaviour
{
    public HumanAgent humanAgent;
    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "TargetForHuman")
    //    {
    //        humanAgent.SetReward(1f);
    //        humanAgent.EndEpisode();
    //        humanAgent.UpdateMateraial(true);
    //    }
    //    else if (other.gameObject.tag == "Wall")
    //    {
    //        humanAgent.SetReward(-1f);
    //        humanAgent.EndEpisode();
    //        humanAgent.UpdateMateraial(false);
    //    }
    //}
    //public void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == "TargetForHuman")
    //    {
    //        humanAgent.SetReward(1f);
    //        humanAgent.EndEpisode();
    //        humanAgent.UpdateMateraial(true);
    //    }
    //    else if (other.gameObject.tag == "Wall")
    //    {
    //        humanAgent.SetReward(-1f);
    //        humanAgent.EndEpisode();
    //        humanAgent.UpdateMateraial(false);
    //    }
    //}
}
