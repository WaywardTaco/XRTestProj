using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentScript : MonoBehaviour
{
    NavMeshAgent navAgent;
    [SerializeField] Vector3 debugTarget;

    // Start is called before the first frame update
    void Start()
    {
        this.navAgent = this.GetComponent<NavMeshAgent>();
        navAgent.enabled = false;

        TravelTo(debugTarget);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TravelTo(Vector3 targetPosition){
        navAgent.enabled = true;
        navAgent.destination = targetPosition;
    }

    public void StopMove(){
        navAgent.enabled = false;
    }

    void ResumeMove(){
        navAgent.enabled = true;
    }
}
