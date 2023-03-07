using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

/// <summary>
/// Class used as main script for player melee weapon
/// </summary>
public class WeaponMelee : MonoBehaviour
{
    #region Variables
    WeaponAttack attack;
    WeaponDetection detection;
    WeaponAnim animator;

    public InputActionReference input;
    #endregion

    #region Events
    private void OnEnable()
    {
        input.action.started += Attack;
    }

    private void OnDisable()
    {
        input.action.started -= Attack;
    }
    #endregion

    #region Methods
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
        detection.DetectColliders();
    }

    void Attack(InputAction.CallbackContext obj)
    {
        // Attack
        if (!Player.player.isDashing && !Player.player.isExhausted) attack.PreformAttack(animator);
    }
    #endregion
}
