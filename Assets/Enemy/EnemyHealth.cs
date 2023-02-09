using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] 
    int MaxHealth = 5;
    [Tooltip("Adds amount to MaxHealth when enemy dies.")]
    [SerializeField]
    GameObject DeadPar;

    int Health = 0;

    Enemy enemy;
    Animator ani;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        ani = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Health = MaxHealth;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    public void IncraaseHP(float RampUpMultiplier)
    {
        Debug.Log(Mathf.RoundToInt(MaxHealth * RampUpMultiplier));
        MaxHealth = Mathf.RoundToInt(MaxHealth * RampUpMultiplier);
    }

    void ProcessHit()
    {
        ani.SetTrigger("Hit");
        Health--;

        if (Health <= 0)
        {
            Instantiate(DeadPar, transform.position, Quaternion.identity);
            enemy.RewardGold();
            gameObject.SetActive(false);
        }
    }
}
