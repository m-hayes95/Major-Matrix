using UnityEngine;

public class BossType : MonoBehaviour
{
    [SerializeField, Tooltip("Set true if the boss is using the Behaviour Tree, or false if the boss is using the State Machine.")]
    private bool hasBT;

    public bool CheckIfBossHasBT()
    {
        return hasBT;
    }
}
