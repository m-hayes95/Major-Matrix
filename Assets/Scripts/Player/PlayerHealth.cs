using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    private PlayerController playerController;
    private float currentHP;
    private UnityEvent OnDeath;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        currentHP = playerController.stats.maxHP;
        if (OnDeath == null)
            OnDeath = new UnityEvent();
        OnDeath.AddListener(PlayerDead);
    }

    private void Update()
    {
        Debug.Log($"Players current HP = {currentHP}");
        
    }

    public float GetPlayersCurrentHP()
    {
        return currentHP;
    }

    public void DamagePlayer(float damageAmount)
    {
        if (currentHP > 0)
        {
            currentHP -= damageAmount;
            if (currentHP <= 0) 
                OnDeath.Invoke();
        }
    }

    private void PlayerDead()
    {
        Debug.Log("Players current HP reached 0, Player Died");
    }
}
