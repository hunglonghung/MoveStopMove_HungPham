using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum CharacterState
        {
            Run,
            Attack,
            Idle,
            Win,
            Lose
        }
    public float MoveSpeed;
    public Animator anim;
    public string currentAnimName;
    public Vector3 MoveDirection;
    public CharacterState CurrentState;
    public float scanRadius = 5.0f;
    public GameObject target;
    [SerializeField] public GameObject Hand;
    [SerializeField] public GameObject BulletPrefab;
     private GameObject currentBullet = null;
    [SerializeField] public GameObject Weapon;
    [SerializeField] public float bulletSpeed = 5f;

    // Run
    protected void Move(Vector3 direction)
    {
        
        transform.Translate(direction * MoveSpeed * Time.deltaTime, Space.World);
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
        ChangeAnim("run"); 
    }
    //Idle
    protected void Idle()
    {
        ChangeAnim("idle");
    }
    //Attack
    protected void Attack()
    {
        if(currentBullet == null)
        {
            ChangeAnim("attack");
            Fire();
        }
        
        // StartCoroutine(Reattack());
    }

    // protected IEnumerator Reattack()
    // {
    //     yield return new WaitForSeconds(1f); 
    //     anim.ResetTrigger("attack");
    // }
    protected void Fire()
    {
        currentBullet = Instantiate(BulletPrefab, Weapon.transform.localPosition, Quaternion.identity);
        Vector3 bulletDirection = (target.transform.position - gameObject.transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(bulletDirection);
        currentBullet.AddComponent<Bullet>().OnInit(bulletDirection, bulletSpeed,this,5f);
    }

    public void OnBulletDestroyed()
    {
        currentBullet = null;
    }
    //Weapon
    public void setWeapon(GameObject Weapon)
    {
        GameObject characterWeapon = Instantiate(Weapon,Hand.transform.position,Quaternion.identity,Hand.transform);
        characterWeapon.transform.rotation = Quaternion.Euler(180,0,0);
    }
    //Animation

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }   
}