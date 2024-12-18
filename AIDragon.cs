using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

/// <summary>
/// 龙的Ai
/// </summary>
public class AIDragon : MonoBehaviour
{
    public enum EState
    {
        Idle,
        Move,
        Fly,
        Push,
    }

    private Dragon Owner;
    private NavMeshAgent m_Agent;
    public EState m_State = EState.Idle;
    public float speed = 5;
    public float attackRange = 1;
    public Transform center;
    
    public void Start()
    {
        Owner = gameObject.GetComponent<Dragon>();
        m_Agent = gameObject.GetComponent<NavMeshAgent>();
        Idle();
    }

    void SetState()
    {
        switch (m_State)
        {
            case EState.Idle:
                Idle();
                break;
            case EState.Move:
                Move();
                break;
            case EState.Fly:
                Fly();
                break;
            case EState.Push:
                Push();
                break;
        }
    }
    
    void Idle()
    {
        m_Agent.enabled = false;
        CheckEnemy();
    }

    void CheckEnemy()
    {
        var cols = Physics.OverlapSphere(transform.position, attackRange);
        foreach (var col in cols)
        {
            if (col.gameObject.tag == "Player" && col.gameObject != gameObject)
            {
                Owner.Foward(col.gameObject);
                m_State = EState.Push;
                return;
            }
        }

        StartCoroutine((DelayRandomState()));
    }

    void Move()
    {
        m_Agent.enabled = true;
        var pos = center.position + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
        m_Agent.SetDestination(pos);
        StartCoroutine(DelayRandomState());
    }
    
    void Fly()
    {
        m_Agent.enabled = false;
        Owner.Jump();
        StartCoroutine(DelayRandomState());
    }

    void Push()
    {
        Owner.Push();
        m_Agent.enabled = false;
        StartCoroutine(DelayRandomState());
    }

    IEnumerator DelayRandomState()
    {
        yield return new WaitForSeconds(3);
        m_State = (EState)Random.Range(0, 4);
        SetState();
    }
}