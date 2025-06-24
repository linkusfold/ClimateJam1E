using UnityEngine;

namespace DefaultNamespace
{
    public class Enemy : MonoBehaviour
    {
        public float speed = 1;
        public float health = 100;
        public float defense = 10;
        public float damage = 10; //How much damage they’ll do if they reach the end

        public Path path;
        public int currentNodeId = 1;
        public bool pathing = true;

        protected void Start()
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
            currentNodeId++;
            GoToPathNode(currentNodeId);
        }
        protected void OnReachedEnd()
        {
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

            if (health <= 0)
            {
                Die();
            }
        }

        protected void Die()
        {
            Debug.Log($"{gameObject.name} died.");
            Destroy(gameObject); //remove the enemy
        }
    }
}