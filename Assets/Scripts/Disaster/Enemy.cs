using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class Enemy : MonoBehaviour
    {
        protected float speed = 1;
        protected float health = 100;
        protected float defense = 10;
        protected float damage = 10;
        protected float atkSpeed = 1;

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
            
            Vector3 targetPos = path.pathNodes[currentNodeId];

            // Check for tower in the way
            Collider2D hit = Physics2D.OverlapCircle(targetPos, 0.1f, LayerMask.GetMask("Tower"));

            if (hit != null && hit.gameObject.TryGetComponent(out IDamageableBuilding bldg) && !bldg.IsDestroyed)
            {
                Attack(bldg);
                return;
            }

            transform.position = Vector3.MoveTowards(
                transform.position,
                path.pathNodes[currentNodeId],
                speed * Time.deltaTime
            );
        }

        private void GoToPathNode(int nodeId)
        {
            currentNodeId = nodeId;
            pathing = true;
        }

        private void ReachedNode()
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

        private void OnReachedEnd()
        {
            Debug.Log($"{gameObject.name} reached the end and dealt {damage} damage!");
            WaveSpawner.instance.EnemiesSafe++;
            WaveSpawner.instance.EnemiesAlive--;
            levelData.OnEnemyRemoved();
            Destroy(gameObject);
        }

        private void TeleportToPathNode(int nodeId)
        {
            transform.position = path.pathNodes[nodeId];
        }
        
        private float nextAttackTime = 0f;

        
        protected bool CanAttack()
        {
            return Time.time >= nextAttackTime;
        }

        protected void UpdateAttackCooldown()
        {
            nextAttackTime = Time.time + (1f / atkSpeed);
        }

        protected void Attack(IDamageableBuilding bldg)
        {
            if (!CanAttack()) return;
            PerformAttack(bldg);
            UpdateAttackCooldown();
        }

        protected abstract void PerformAttack(IDamageableBuilding building);



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

        public void Immobilize(float duration)
        {
            StartCoroutine(ImmobilizeCoroutine(duration));
        }

        private IEnumerator ImmobilizeCoroutine(float duration)
        {
            pathing = false;
            yield return new WaitForSeconds(duration);
            pathing = true;
        }

        public void ExtinguishFire()
        {
            
        }

        protected void Die()
        {
            Debug.Log($"{gameObject.name} died.");
            WaveSpawner.instance.EnemiesAlive--;
            levelData.OnEnemyRemoved(); 
            Destroy(gameObject);
        }
    }
}