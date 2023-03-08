using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public Animator anim;
    public Animator NextAnimator;

    public Transform AttackPoint;
    public float AttackRange = 10f;
    public LayerMask EnemyLayers;

    public float bufSpeed;

    public bool CanAttack = true;

    public int AttackDamage;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {     
        Attack();     
    }

    void Attack()
    {
        if (GetComponent<MainObject>().energy >= Player.energyWaste && Input.GetMouseButtonDown(0) && CanAttack == true)
        {     
            bufSpeed = GetComponent<MainObject>().speed;    //���������� ����������� �������� ��������� � �����
            GetComponent<MainObject> ().speed = 0f;         //������������ ���������

            anim.SetTrigger("Attack");  //������������� �������� �����
            GetComponent<MainObject>().energy -= GetComponent<MainObject>().energyWaste; // �������� ������ ������� �� ������� ��������� 
            CanAttack = false;
            NextAnimator.SetTrigger("OnAnimationEnded"); //�������� ����� AttackToogle()

            Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayers);

            foreach(Collider2D enemy in HitEnemies)
            {
               // enemy.GetComponent<Enemy>().TakeDamage(AttackDamage);
            }

            Invoke("AttackReload", 1);
        }
    }

    void AttackReload()
    {
        CanAttack = true;
    }

    public void AttackToogle()
    {
        Debug.Log("WORK");
        GetComponent<MainObject>().speed = bufSpeed;   //�������������� �������� ���������
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere (AttackPoint.position, AttackRange);
    }

}

