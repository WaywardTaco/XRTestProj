using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentCaller : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] agents = GameObject.FindGameObjectsWithTag("NavAgent");

        foreach (GameObject agent in agents){
            agent.GetComponent<AgentScript>()?.TravelTo(this.transform.position);
        }
    }

    private void OnDestroy() {
        GameObject[] agents = GameObject.FindGameObjectsWithTag("NavAgent");

        foreach (GameObject agent in agents){
            agent.GetComponent<AgentScript>()?.StopMove();
        }
    }
}
