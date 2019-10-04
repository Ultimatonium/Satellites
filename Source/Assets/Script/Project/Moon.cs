using UnityEngine;

public class Moon : MoveableSpaceObject
{
    private void Awake()
    {
        mass = 7.359f * Mathf.Pow(10, 22 + Space.scale);
        initialSpeed = 300;
        initialDistanceToCentralObject = 3.844f * Mathf.Pow(10, 8 + Space.scale) * 1000;
        drall = -1;
    }

    protected override void Start()
    {
        base.Start();
        transform.position = new Vector3((float)initialDistanceToCentralObject, 0, 0);
        
        flyDirection = Rotate.RotateY(new VectorXYZ[] { Physic.GravityDirection(VectorXYZ.Direction(transform.position, centralObject.transform.position)) }, -Mathf.PI / 2f)[0].Normalize();
        flyDirection *= initialSpeed;
        //flyDirection *= Physic.PerfectVelocity(centralObject.mass, Radius());
    }
}
