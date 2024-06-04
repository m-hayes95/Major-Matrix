using UnityEngine;

public class MeleeAttackVisualEffect : MonoBehaviour
{
    [SerializeField] private GameObject hitEffect;

    public void ShowHitEffectVisuals()
    {
        Instantiate(hitEffect, transform.position, transform.rotation);
    }
}
