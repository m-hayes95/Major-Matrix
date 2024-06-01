
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject playerVisuals;
    [SerializeField] private ParticleSystem deathEffect;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIController uIController;
    private PlayerController playerController;
    private float currentHP;
    private UnityEvent OnDeath;
    private bool isDead;
    

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
        isDead = false; 
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
        isDead = true;
        Debug.Log("Players current HP reached 0, Player Died");
        DeathEffect();
        playerVisuals.SetActive(false);
        StartCoroutine(uIController.DisplayEndGameMenu(3f));
    }

    private void DeathEffect()
    {
        ParticleSystem newEffect = Instantiate(deathEffect, transform.position, Quaternion.identity);
    }
    public bool GetIsDead()
    {
        return isDead;
    }
}
