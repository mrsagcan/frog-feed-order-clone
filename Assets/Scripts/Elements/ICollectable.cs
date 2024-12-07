using System.Collections.Generic;
using UnityEngine;

public interface ICollectable
{
    public bool HasCollected { get; set; }

    void Collect(List<Vector3> route);
}
