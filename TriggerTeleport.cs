using System;
using System.Collections;
using System.Collections.Generic;
using Mirror.Examples.Pong;
using UnityEngine;

public class TriggerTeleport : MonoBehaviour
{
    [SerializeField] private Transform point;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().Teleport(point);
        }
    }
}