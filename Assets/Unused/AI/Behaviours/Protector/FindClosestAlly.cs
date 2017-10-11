using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(DefenderVision))]
public class FindClosestAlly : StateMachineBehaviour
{
    private NavMeshAgent m_agent;
    private ProtectorVision m_protectorVision;
    private FindObjectsInRadius m_foir;
    public float m_orbitRange = 2.0f;
    public float m_orbitSpeed = 0.1f;
    public float m_scanRotateSpeed = 100.0f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_agent = animator.GetComponent<NavMeshAgent>();
        m_protectorVision = animator.GetComponent<ProtectorVision>();
        m_foir = animator.GetComponent<FindObjectsInRadius>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if(m_protectorVision.leaderInSight && m_protectorVision.m_leader != null)
        {
            Vector3 distVect =  m_protectorVision.m_leader.transform.position - this.m_agent.transform.position;
            distVect.y = 0;
            distVect.Normalize();
            

            m_agent.SetDestination(m_protectorVision.m_leader.position - distVect * m_orbitRange + m_agent.transform.right * m_orbitSpeed);
            
            //m_agent.transform.forward = -distVect;
        }

        //m_agent.transform.Rotate(Vector3.up * m_scanRotateSpeed * Time.deltaTime);
        animator.SetBool("hasLeader", m_protectorVision.leaderInSight);

        animator.SetBool("inRange", m_foir.inRange);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
