using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerController controller;
    private Shoot shoot;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
        shoot = GetComponent<Shoot>();  
    }
    private void Update()
    { 
        if (controller != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                shoot.FireWeaponPlayer(controller.stats.shotVelocity);
            }
        }
    }
}
