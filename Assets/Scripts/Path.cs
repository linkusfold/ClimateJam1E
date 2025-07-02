using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Vector3> pathNodes = new List<Vector3>();

    public static Path instance;

    private void Awake()
    {
        instance = this;
        pathNodes = transform.GetComponentsInChildren<Transform>().Select(x => x.position).ToList();
    }
}
