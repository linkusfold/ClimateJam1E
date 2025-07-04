﻿using UnityEngine;

namespace DefaultNamespace
{
    public abstract class Enemy : MonoBehaviour
    {
        protected float speed = 1;
        protected float health = 100;
        protected float defense = 10;
        protected float damage = 10;

        public Path path;
        public int currentNodeId = 1;
        public bool pathing = true;

        public LevelData levelData;

        protected virtual void Start()
        {
            if (path == null)
            {
                Debug.LogError("Enemy " + gameObject.name + " has no path!");
                return;
            }

            TeleportToPathNode(currentNodeId);
            GoToPathNode(currentNodeId + 1);
        }

        protected void FixedUpdate()
        {
            if (!pathing) return;

            if (transform.position == path.pathNodes[currentNodeId])
            {
                ReachedNode();
                return;
            }

            transform.position = Vector3.MoveTowards(
                transform.position,
                path.pathNodes[currentNodeId],
                speed * Time.deltaTime
            );
        }

        protected void GoToPathNode(int nodeId)
        {
            currentNodeId = nodeId;
            pathing = true;
        }

        protected void ReachedNode()
        {
            pathing = false;
            Debug.Log("Enemy " + gameObject.name + " reached Node " + currentNodeId + "!");

            if (path.pathNodes.Count <= currentNodeId + 1)
            {
                OnReachedEnd();
                return;
            }

            currentNodeId++;
            GoToPathNode(currentNodeId);
        }

        protected void OnReachedEnd()
        {
            Debug.Log($"{gameObject.name} reached the end and dealt {damage} damage!");
            WaveSpawner.instance.EnemiesSafe++;
            levelData.OnEnemyRemoved();
            Destroy(gameObject);
        }

        public void TeleportToPathNode(int nodeId)
        {
            transform.position = path.pathNodes[nodeId];
        }

        public void TakeDamage(float amount)
        {
            float effectiveDamage = Mathf.Max(amount - defense, 0);
            health -= effectiveDamage;

            Debug.Log("Enemy " + gameObject.name + " health reduced to " + health + "!");

            if (health <= 0)
            {
                Die();
            }
        }
        
        public void Immobilize(float duration) {}
        
        public void ExtinguishFire() {}

        protected void Die()
        {
            Debug.Log($"{gameObject.name} died.");
            levelData.OnEnemyRemoved(); 
            Destroy(gameObject);
        }

        protected abstract void Attack();
    }
}