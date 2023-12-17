using UnityEngine;

public class EndEffector : MonoBehaviour
{
    public RobotControllerAgent parentAgent;
    public HumanAgentController humanAgent;
    public WaterCS[] water;

    public bool MouseEndEffect = false;
    public void WaterInit()
    {
        for (int i = 0; i < water.Length; i++)
        {
            water[i].gameObject.SetActive(true);
            water[i].InitPosition();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if( MouseEndEffect)
        {
            if (other.transform.CompareTag("TargetForHuman"))
            {
                if (humanAgent != null)
                    humanAgent.JackpotReward(other.gameObject);
            }
            else if (other.transform.CompareTag("Wall"))
            {
                if (humanAgent != null)
                    humanAgent.GroundHitPenalty();
            }
        }
        else
        {
            if (other.transform.CompareTag("Target"))
            {
                if (parentAgent != null)
                    parentAgent.JackpotReward(other.gameObject);
            }
            else if (other.transform.CompareTag("Wall"))
            {
                if (parentAgent != null)
                    parentAgent.GroundHitPenalty();
            }
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (MouseEndEffect)
        {
            if (other.transform.CompareTag("TargetForHuman"))
            {
                if (humanAgent != null)
                    humanAgent.JackpotReward(other.gameObject);
            }
            else if (other.transform.CompareTag("Wall"))
            {
                if (humanAgent != null)
                    humanAgent.GroundHitPenalty();
            }
        }
        else
        {
            if (other.transform.CompareTag("Target"))
            {
                if (parentAgent != null)
                    parentAgent.JackpotReward(other.gameObject);
            }
            else if (other.transform.CompareTag("Wall"))
            {
                if (parentAgent != null)
                    parentAgent.GroundHitPenalty();
            }
        }
    }

    public bool WaterRemaining()
    {
        for (int i = 0; i < water.Length; i++)
        {
            if( water[i].gameObject.activeInHierarchy ) return true;
        }
        return false;
    }
}
