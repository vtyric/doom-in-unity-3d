using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health=100;
    [SerializeField] private float sightRange;
    [SerializeField] private float walkPointRange;
    [SerializeField] private LayerMask whatIsPlayer, whatIsGround;

    private Transform player;
    private NavMeshAgent enemy;
    private Vector3 walkPoint;
    private bool playerInSightRange;
    private bool walkPointSet;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        enemy = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (!playerInSightRange)
            Patrolling();
        else
            ChasePlayer();
    }

    private void Patrolling()
    {
        if (!walkPointSet)
            SearchWalkPoint();
        else
            enemy.SetDestination(walkPoint);

        walkPointSet = (transform.position - walkPoint).magnitude >= 1f;
    }

    private void SearchWalkPoint()
    {
        var rndX = Random.Range(-walkPointRange, walkPointRange);
        var rndZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(
            transform.position.x + rndX,
            transform.position.y,
            transform.position.z + rndZ);

        walkPointSet = Physics.Raycast(
            walkPoint,
            -transform.up,
            2f,
            whatIsGround);
    }

    private void ChasePlayer()
    {
        enemy.SetDestination(player.position);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

}
