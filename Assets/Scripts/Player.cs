using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    Vector2 move;

    public Camera followCamera;
    public FixedJoystick joystick;

    public int maxHP;
    public int curHP;

    public bool isAttack = false;

    Rigidbody2D rb;
    Animator anim;
    BoxCollider2D attackRange;

    public enum UnitState
    {
        idle, run, attack, stun, death
    }

    public UnitState unitState = UnitState.idle;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        attackRange = GetComponentInChildren<BoxCollider2D>();
        curHP = maxHP;
    }

    void Update()
    {
        DoMove();
        DoAttack();
    }

    void FixedUpdate()
    {
        
    }

    void SetState(UnitState state)
    {
        unitState = state;
        switch (unitState)
        {
            case UnitState.idle:
                anim.SetFloat("RunState", 0f);
                break;
            case UnitState.run:
                anim.SetFloat("RunState", 0.5f);
                break;
            case UnitState.attack:
                anim.SetTrigger("Attack");
                anim.SetFloat("AttackState", 0.0f);
                anim.SetFloat("NormalState", 0.0f);// 0: Sword, 0.5: Bow, 1.0: Magic
                break;
            case UnitState.stun:
                anim.SetFloat("RunState", 1.0f);
                break;
            case UnitState.death:
                anim.SetTrigger("Die");
                break;
        }
    }

    void DoMove()
    {
        move.x = joystick.Horizontal;
        move.y = joystick.Vertical;

        if (move.x != 0)
        {
            SetState(UnitState.run);

            if (move.x > 0)
                transform.localScale = new Vector3(-1, 1, 1);
            else
                transform.localScale = new Vector3(1, 1, 1);
        }

        if(move.x == 0 && move.y == 0)
        {
            SetState(UnitState.idle);
        }

        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
    }

    void DoAttack()
    {        
        if(!isAttack)
            return;
        StartCoroutine(Attacked());

        isAttack = false;
    }

    IEnumerator Attacked()
    {
        SetState(UnitState.attack);
        attackRange.enabled = true;
        yield return new WaitForSeconds(0.3f);
        attackRange.enabled = false;
    }
}
