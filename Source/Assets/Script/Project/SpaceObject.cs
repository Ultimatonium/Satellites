using UnityEngine;

public abstract class SpaceObject : MonoBehaviour
{
    [SerializeField]
    public double mass;
    [SerializeField]
    protected double drall = 0;
    [SerializeField]
    protected int longitude;
    [SerializeField]
    protected int latitude;
    [SerializeField]
    public double radius;
    [SerializeField]
    protected Material material;

    protected MeshFilter meshFilter;
    protected TextMesh infoBox;
    
    protected virtual void Start()
    {
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = Sphere.GenerateMesh(longitude, latitude, (float)radius);
        gameObject.AddComponent<MeshRenderer>().material = material;
        DisplayValues();
    }

    protected void Update()
    {
        DisplayValues();
    }

    protected virtual void FixedUpdate()
    {
        meshFilter.mesh = Rotate.RotateY(meshFilter.mesh, (float)drall * (Time.fixedDeltaTime * Time.timeScale));
    }

    protected virtual void DisplayValues()
    {
        if (infoBox == null) {
            GameObject InfoObject = new GameObject("Infobox", typeof(TextMesh));
            InfoObject.transform.parent = this.gameObject.transform;
            InfoObject.transform.position = Vector3.zero;
            InfoObject.transform.Rotate(new Vector3(90, 0, 0));
            infoBox = InfoObject.GetComponent<TextMesh>();
            infoBox.characterSize = 5;
        }
        infoBox.text = "Name: " + name + System.Environment.NewLine
                     + "ID: " + GetInstanceID() + System.Environment.NewLine
                     + "Mass: " + mass + System.Environment.NewLine
                     + "Drall: " + drall + System.Environment.NewLine
                     + "Radius: " + radius + System.Environment.NewLine;
    }
}
