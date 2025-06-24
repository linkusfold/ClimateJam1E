using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    public class Enemy : MonoBehaviour
    {
        public Path path;
        public float speed = 1;
        public int currentNodeId = 1;
        public bool pathing = true;
        public int EnemiesSafe = 0;


        private void Start()
        {

            if (path == null)
            {
                Debug.LogError("Enemy " + gameObject.name + " has no path!");
                return;
            }
            TeleportToPathNode(currentNodeId);
            GoToPathNode(currentNodeId+1);
        }

        private void FixedUpdate()
        {


            if (!pathing)
            {
                Destroy(gameObject);
                EnemiesSafe++;
            }

            if (EnemiesSafe == 4)
            {
                EditorApplication.isPlaying = false; // Delete when finished
                Application.Quit();
            }

            if (transform.position == path.pathNodes[currentNodeId])
            {
                ReachedNode();
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, path.pathNodes[currentNodeId], speed*Time.deltaTime);

        }
        
        public void GoToPathNode(int nodeId)
        {
            currentNodeId = nodeId;
            pathing = true;
        }

        private void ReachedNode()
        {
            pathing = false;
            Debug.Log("Enemy " +gameObject.name + " reached Node " + currentNodeId + "!");
            
            if (path.pathNodes.Count <= currentNodeId+1) return;
            
            currentNodeId++;
            GoToPathNode(currentNodeId);
        }

        public void TeleportToPathNode(int nodeId)
        {
            Vector3 nodePos = path.pathNodes[nodeId];
            transform.position = nodePos;
        }
    }
}