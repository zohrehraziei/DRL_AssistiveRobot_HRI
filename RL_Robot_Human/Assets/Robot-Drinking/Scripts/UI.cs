using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickDrinkingSAC()
    {
        SceneManager.LoadScene("Drinking_SAC");
    }
    public void OnClickDrinkingPPO()
    {
        SceneManager.LoadScene("Drinking_PPO");
    }

    public void OnClickFeedingPPO()
    {
        SceneManager.LoadScene("Robot_Feeding_ppo");
    }
    public void OnClickFeedingSAC()
    {
        SceneManager.LoadScene("Robot_Feeding_sac");
    }
}
 