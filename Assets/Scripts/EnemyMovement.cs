using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent nav;
    private int wavepointIndex = 0;

    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Wavepoints.waypoints[0];

        nav = GetComponent<NavMeshAgent>();
        nav.SetDestination(target.position);
    }
    
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        //if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        //{
        //    GetNextWaypoint();
        //}

        enemy.speed = enemy.startSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndBox"))
        {
            EndPath();
        }
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Wavepoints.waypoints.Length - 1)
        {
            EndPath();
            return;
        }
        wavepointIndex++;
        target = Wavepoints.waypoints[wavepointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
