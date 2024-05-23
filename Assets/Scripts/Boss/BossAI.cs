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
    // Thresholds
    public float closeRangeAttackThreshold;
    public float longRangeAttackThreshold;
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
    private void FacePlayer()
    {
        // Look towards the player
    }
    public float DistanceFromPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log($"player is {distanceToPlayer} away from the boss");
        return distanceToPlayer;
    }
    public void Move()
    {
       // Move the boss character
    }
    
    public void NormalRangeAttack()
    {
        // Attack the player if they cross a certain distance
    }
    public void NormalCloseAttack()
    {
        // Attack the player if they get too close
    }

    public void SpecialLowAttack()
    {
        // Cross Screen attack from the ground
    }
    public void SpecialHighAttack()
    {
        // Cross screen attack from the ceiling
    }
    public void Sheild()
    {

    }

}
