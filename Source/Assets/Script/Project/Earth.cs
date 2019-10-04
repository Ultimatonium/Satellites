using UnityEngine;

public class Earth : StaticSpaceObject
{
    private void Awake()
    {
        mass = 5.974 * Mathf.Pow(10, 24 + Space.scale);
        radius = 6.378;
        drall = Mathf.PI / 43200 * Space.rotationScale;
    }
}
