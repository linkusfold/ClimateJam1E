using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class Enemy : MonoBehaviour
    // This is an abstract base-class for the minions disasters can spawn. 
    // They follow a path and deal damage if they get to the end.
    // They also each have a unique attack.
    {
        protected float speed = 1;
        protected float health = 100;
        protected float defense = 10;
        protected float damage = 10; //How much damage they’ll do if they reach the end

        public Path path;
        public int currentNodeId = 1;
        public bool pathing = true;
        private WaveSpawner waveSpawner;


        protected virtual void Start()
        {
            waveSpawner = GetComponentInParent<WaveSpawner>();

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
            transform.position = Vector3.MoveTowards(transform.position, path.pathNodes[currentNodeId], speed * Time.deltaTime);
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
            waveSpawner.EnemiesSafe++;
            waveSpawner.waves[waveSpawner.currentWaveIndex].enemiesLeft--;
            //Once we reach the end of the path we deal damage to the town/house
            Debug.Log($"{gameObject.name} reached the end and dealt {damage} damage!");
            Destroy(gameObject); // Remove the enemy
        }

        public void TeleportToPathNode(int nodeId)
        {
            Vector3 nodePos = path.pathNodes[nodeId];
            transform.position = nodePos;
        }

        public void TakeDamage(float amount)
        {
            //First reduce incoming damage by the defense amount
            float effectiveDamage = Mathf.Max(amount - defense, 0);
            health -= effectiveDamage;

            Debug.Log("Enemy " + gameObject.name + " health reduced to " + health + "!");

            if (health <= 0)
            {
                Die();
            }
        }

        protected abstract void Attack(); //attack method to be overriden by subclasses
        // we add specific attack behavior in the subclasses

        protected void Die()
        {
            Debug.Log($"{gameObject.name} died.");
            Destroy(gameObject); //remove the enemy
        }

        public void StopPathing()
        {
            pathing = false;
        }

        public void StartPathing()
        {
            pathing = true;
        }
    }
}