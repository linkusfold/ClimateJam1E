using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Vector3> pathNodes = new List<Vector3>();

    private void Awake()
    {
        pathNodes = transform.GetComponentsInChildren<Transform>().Select(x => x.position).ToList();
    }
}
