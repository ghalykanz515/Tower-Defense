using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Assets.Master.Scripts.Enemy.States;
using UnityEngine.UI;

namespace Assets.Master.Scripts.Enemy.Base
{
    public class EnemyBase : MonoBehaviour
    {
        public NavMeshAgent agent;
        public Transform target;
        public EnemyStateMachine stateMachine;
        public int health = 100;
        public float speed = 6f;

        public GameObject deathEffect;

        [Header("Unity Stuff")]
        public Image healthBar;

        public float startHealth = 100f;
        public float startSpeed = 6f;

        private WaveSpawner waveSpawner;

        protected virtual void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            target = GameObject.Find("FinalTower").transform;
            stateMachine = new EnemyStateMachine(this);

            health = (int)startHealth;
            speed = startSpeed;
        }

        protected virtual void Update()
        {
            stateMachine.Update();
        }

        public void TakeDamage(int damage)
        {
            health -= damage;

            healthBar.fillAmount = health / startHealth;

            if (health <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            waveSpawner.ReturnEnemyToPool(this);
            GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);
        }


        public void Init(WaveSpawner waveSpawner) 
        {
            this.waveSpawner = waveSpawner;
        }

        public void Slow(float pct)
        {
            speed = startSpeed * (1f - pct);
        }
    }
}