using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Player player;
    private const string IS_WALKING = "IsWalking";

    private Animator playerAnimator;

    private void Awake() {
        playerAnimator = GetComponent<Animator>();
    }

    private void Update() {
        playerAnimator.SetBool(IS_WALKING, player.IsWalking());
    }
}
