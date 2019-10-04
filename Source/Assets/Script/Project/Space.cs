using UnityEngine;
using System.Collections.Generic;
using System;

public class Space : MonoBehaviour
{
    [SerializeField]
    private GameObject satelitePrefab;

    internal const double rotationScale = 10000;
    public const int scale = -10;
    public const bool debug = false;
    private GameObject newSatelite = null;
    private LineRenderer flyLine;

    private List<SpaceObject> crashableObjects = new List<SpaceObject>();

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        CheckCrash();
        if (newSatelite != null)
        {
            if (flyLine == null)
            {
                flyLine = new GameObject("flyLine", typeof(LineRenderer)).GetComponent<LineRenderer>();
                flyLine.SetPosition(0, newSatelite.transform.position);
            }
            flyLine.SetPosition(1, GetSpacePositionFromMouse(Input.mousePosition));
            if (Input.GetMouseButtonDown(0))
            {
                if (GetSpacePositionFromMouse(Input.mousePosition) == newSatelite.transform.position) return;
                newSatelite.GetComponent<Satelite>().flyDirection = VectorXYZ.FromVector3(GetSpacePositionFromMouse(Input.mousePosition) - newSatelite.transform.position).Normalize();
                Debug.Log(newSatelite.GetComponent<Satelite>().flyDirection);
                newSatelite.GetComponent<Satelite>().enabled = true;
                Destroy(flyLine.gameObject);
                crashableObjects.Add(newSatelite.GetComponent<SpaceObject>());
                newSatelite = null;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                newSatelite = Instantiate(satelitePrefab, GetSpacePositionFromMouse(Input.mousePosition), Quaternion.identity);
                newSatelite.GetComponent<Satelite>().enabled = false;
            }
        }
    }

    private void CheckCrash()
    {
        foreach (SpaceObject crashableObject1 in crashableObjects)
        {
            foreach (SpaceObject crashableObject2 in crashableObjects)
            {
                if (crashableObject1.GetInstanceID() == crashableObject2.GetInstanceID()) continue;
                if (VectorXYZ.Direction(crashableObject1.transform.position, crashableObject2.transform.position).Magnitude() < crashableObject1.radius + crashableObject2.radius)
                {
                    Debug.Log("Destroy: " + crashableObject1.GetInstanceID() + " & " + crashableObject2.GetInstanceID());
                    Destroy(crashableObject1);
                    Destroy(crashableObject2);
                }
            }
        }
    }

    private Vector3 GetSpacePositionFromMouse(Vector3 mousePosition)
    {
        return new Vector3(mousePosition.x - Camera.main.pixelWidth / 2, 0, mousePosition.y - Camera.main.pixelHeight / 2);
    }
}
