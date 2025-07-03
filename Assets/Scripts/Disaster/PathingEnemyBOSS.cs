using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    // This is an abstract class for the minions that disasters can spawn. 
    // They follow a path and deal damage if they get to the end.
    // They also each have a unique attack.
    public abstract class PathingEnemyBOSS : DisasterEnemy
    {
        public Path path;
        public int currentNodeId = 1;
        public bool pathing = true;


        protected override void Start()
        {
            base.Start();

            if (path == null)
            {
                Debug.LogError("Enemy " + gameObject.name + " has no path!");
                return;
            }
            TeleportToPathNode(currentNodeId);
            GoToPathNode(currentNodeId + 1);
        }

        protected override void FixedUpdate()
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
            TakeDamage(50f); //Debug testing
        }
        protected void OnReachedEnd()
        {
            waveSpawner.EnemiesSafe++;
            waveSpawner.levelData.waves[waveSpawner.levelData.currentWaveIndex].enemiesLeft--;
            //Once we reach the end of the path we deal damage to the town/house
            Debug.Log($"{gameObject.name} reached the end and dealt {damage} damage!");
            Destroy(gameObject); // Remove the enemy
        }

        public void TeleportToPathNode(int nodeId)
        {
            Vector3 nodePos = path.pathNodes[nodeId];
            transform.position = nodePos;
        }
    }
}