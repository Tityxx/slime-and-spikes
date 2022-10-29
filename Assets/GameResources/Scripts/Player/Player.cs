using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Transform leftPoint;
    [SerializeField]
    private Transform rightPoint;
    [SerializeField]
    private bool isLeftSide;
    [SerializeField]
    private float moveDuration = 0.5f;
    [SerializeField]
    private Ease mvoeType = Ease.Linear;

    [Inject]
    private LevelManager levelManager;

    private bool canMove = true;

    private const string JUMP_TO_LEFT = "JumpToLeft";
    private const string JUMP_TO_RIGHT = "JumpToRight";

    private void Start()
    {
        transform.position = levelManager.GetSidePosition(transform.position, isLeftSide ? leftPoint.position : rightPoint.position, isLeftSide);
        InputController.onPointerDown += Swipe;
    }

    private void OnDestroy()
    {
        InputController.onPointerDown -= Swipe;
    }

    private void Swipe()
    {
        if (!canMove) return;

        canMove = false;
        isLeftSide = !isLeftSide;
        Vector3 targetPosition = levelManager.GetSidePosition(transform.position, isLeftSide ? leftPoint.position : rightPoint.position, isLeftSide);
        transform.DOMove(targetPosition, moveDuration).OnComplete(() => canMove = true);
        anim.SetTrigger(isLeftSide ? JUMP_TO_LEFT : JUMP_TO_RIGHT);
    }
}