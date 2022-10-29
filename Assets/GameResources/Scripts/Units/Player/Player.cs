using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : BaseUnit
{
    public event Action onStartMove = delegate { };

    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GameObject skin;
    [SerializeField]
    private Transform leftPoint;
    [SerializeField]
    private Transform rightPoint;
    [SerializeField]
    private bool isLeftSide;
    [SerializeField]
    private float moveDuration = 0.5f;
    [SerializeField]
    private Ease moveType = Ease.Linear;
    [SerializeField]
    private float invulnerableDuration = 2f;
    [SerializeField]
    private float invulnerableFrequence = 0.5f;

    [Inject]
    private LevelManager levelManager;

    private Coroutine invulnerableCoroutine;

    private bool canMove = true;

    private const string JUMP_TO_LEFT = "JumpToLeft";
    private const string JUMP_TO_RIGHT = "JumpToRight";

    private void Start()
    {
        transform.position = levelManager.GetSidePosition(transform.position, isLeftSide ? leftPoint.position : rightPoint.position, isLeftSide);
        InputController.onPointerDown += Move;
    }

    private void OnDestroy()
    {
        InputController.onPointerDown -= Move;
    }

    private void Move()
    {
        if (!canMove) return;

        canMove = false;
        isLeftSide = !isLeftSide;
        Vector3 targetPosition = levelManager.GetSidePosition(transform.position, isLeftSide ? leftPoint.position : rightPoint.position, isLeftSide);
        transform.DOMove(targetPosition, moveDuration).OnComplete(() => canMove = true);
        anim.SetTrigger(isLeftSide ? JUMP_TO_LEFT : JUMP_TO_RIGHT);

        onStartMove();
    }

    public override void TakeDamage(float damage = 1)
    {
        if (invulnerableCoroutine != null) return;

        invulnerableCoroutine = StartCoroutine(InvulnerableCoroutine());
        base.TakeDamage(damage);
    }

    private IEnumerator InvulnerableCoroutine()
    {
        float time = Time.time;
        while (Time.time < time + invulnerableDuration)
        {
            skin.SetActive(false);
            yield return new WaitForSeconds(invulnerableFrequence / 2);
            skin.SetActive(true);
            yield return new WaitForSeconds(invulnerableFrequence / 2);
        }
        invulnerableCoroutine = null;
    }
}