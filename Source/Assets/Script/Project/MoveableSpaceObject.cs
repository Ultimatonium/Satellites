using UnityEngine;

public abstract class MoveableSpaceObject : SpaceObject
{
    [SerializeField]
    private bool paintTrail;
    [SerializeField]
    protected double initialDistanceToCentralObject;
    [SerializeField]
    protected double initialSpeed;

    protected SpaceObject centralObject;
    public VectorXYZ flyDirection;

    #region debug
    double minSpeed = double.PositiveInfinity;
    double maxSpeed = double.NegativeInfinity;
    #endregion

    protected override void Start()
    {
        base.Start();
        centralObject = GameObject.Find("Earth").GetComponent<SpaceObject>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        flyDirection += Physic.Gravity(VectorXYZ.Direction(transform.position, centralObject.transform.position), mass, centralObject.mass, Radius());
        transform.position = Translate.TranslateVector3(new Vector3[] { transform.position }, flyDirection * Time.fixedDeltaTime * Time.timeScale) [0];

        Debuging();
        PaintTrail();
    }



    protected double Radius()
    {
        if (centralObject == null) return 0;
        return VectorXYZ.Direction(transform.position, centralObject.transform.position).Magnitude();
    }

    private double Period()
    {
        return 2 * Mathf.PI * Mathf.Sqrt((float)(Mathf.Pow((float)Radius(), 3) / Physic.gravitationalConstant));
    }

    protected override void DisplayValues()
    {
        base.DisplayValues();
        infoBox.text += "Period: " + Period() + System.Environment.NewLine
                      + "Direction: " + flyDirection.Normalize() + System.Environment.NewLine
                      + "Speed: " + flyDirection.Magnitude() + System.Environment.NewLine;
    }

    private void Debuging()
    {
        if (!Space.debug) return;
        UnityEngine.Debug.DrawLine(centralObject.transform.position, transform.position);

        double mag = flyDirection.Magnitude();
        if (mag > maxSpeed) maxSpeed = mag;
        if (mag < minSpeed) minSpeed = mag;
        Debug.Log(minSpeed + " " + maxSpeed);

        //Debug.DrawLine(transform.position, (GravityDirection() * GravityVelocity()).ToVector3(), Color.black);
        //Debug.DrawLine(transform.position, (flyDirection * Velocity()).ToVector3(), Color.red);
        //Debug.DrawLine(transform.position, (finalDirection * (Time.fixedDeltaTime * Time.timeScale)).ToVector3(), Color.green);
    }

    private void PaintTrail()
    {
        if (!paintTrail) return;
        GameObject dot = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Destroy(dot.GetComponent<SphereCollider>());
        dot.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        dot.transform.position = transform.position;
        Destroy(dot, 10);
    }
}
