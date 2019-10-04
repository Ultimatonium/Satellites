using UnityEngine;

public class Satelite : MoveableSpaceObject
{
    private void Awake()
    {
        mass = 1000;
        initialSpeed = 200;
        //initialDistanceToCentralObject = 50;
    }

    protected override void Start()
    {
        base.Start();
        //transform.position = new Vector3((float)initialDistanceToCentralObject, 0, 0);

        //flyDirection = Rotate.RotateY(new VectorXYZ[] { Physic.GravityDirection(VectorXYZ.Direction(transform.position, centralObject.transform.position)) }, -Mathf.PI / 2f)[0].Normalize();
        flyDirection *= initialSpeed;
    }
}
