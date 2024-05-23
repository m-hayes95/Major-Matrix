using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    //Boss stats - add sciptable object
    [Header("Boss Stats"), SerializeField] private float moveSpeed;
    [SerializeField] private float normalAttackDamage;
    [SerializeField] private float specialAttackDamage;
    [SerializeField] private float hP;
    // Find gameobject
    private PlayerController playerController;
    private GameObject player;
    // Distance from player
    

    private void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        player = playerController.gameObject;
    }
    private void Update()
    {
        DistanceFromPlayer();
    }
    private void Move()
    {
       // Move the boss character
    }
    private void FacePlayer()
    {
        // Look towards the player
    }
    private float DistanceFromPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log($"player is { distanceToPlayer} away from the boss");
        return distanceToPlayer;
    }
    private void NormalCloseAttack()
    {
        // Attack the player if they get too close
    }
    private void NormalRangeAttack()
    {
        // Attack the player if they cross a certain distance
    }
    private void SpecialLowAttack()
    {
        // Cross Screen attack from the ground
    }
    private void SpecialHighAttack()
    {
        // Cross screen attack from the ceiling
    }
    private void Sheild()
    {

    }

}
