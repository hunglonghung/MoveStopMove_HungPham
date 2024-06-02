using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 direction;
    private float speed;
    private float distanceTraveled;
    private float maxDistance;
    public Character attacker;
    public Character victim; 

    public void OnInit(Vector3 bulletDirection, float bulletSpeed, Character character, float scanRadius)
    {
        direction = bulletDirection;
        speed = bulletSpeed;
        attacker = character; 
        maxDistance = scanRadius;
        distanceTraveled = 0f;
        transform.rotation = Quaternion.LookRotation(direction);
        transform.Rotate(0,100,0);
    }

    private void Update()
    {
        transform.Rotate(0,0,20f);
        float distanceToTravel = speed * Time.deltaTime;
        transform.Translate(direction * distanceToTravel, Space.World);
        distanceTraveled += distanceToTravel;
        if (distanceTraveled >= maxDistance)
        {
            BulletPool.Instance.ReturnBullet(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character")
        {
            victim = other.GetComponent<Character>(); 
            Debug.Log("Attacker: " + attacker.name + " hit Victim: " + victim.name);
            if(!CheckSameCharacter())
            {
                victim.isDead = true;
            }
        }
    }
    public bool CheckSameCharacter()
    {
        if(attacker != victim) return false;
        else return true;
    }
}
