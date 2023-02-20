using System.Collections;
using System.Collections.Generic;
using Constants;
using UnityEngine;

/// <summary>
/// Class used as main script for player melee weapon
/// </summary>
public class WeaponMelee : MonoBehaviour
{
    WeaponAttack attack;
    WeaponDetection detection;
    WeaponAnim animator;

    // Start is called before the first frame update
    void Start()
    {
        detection = GetComponent<WeaponDetection>();
        attack = GetComponent<WeaponAttack>();
        animator = GetComponent<WeaponAnim>();
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();

        detection.DetectColliders();
    }

    void Inputs()
    {
        // Attack
        if (!Player.player.isDashing && Input.GetMouseButton(0)) attack.PreformAttack(animator);
    }
}
