using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneClassifier : MonoBehaviour
{
    public ARPlaneManager planeManager;

    void Update()
    {
        if (planeManager == null) return;

        foreach (var plane in planeManager.trackables)
        {
            Vector3 up = plane.transform.up;

            float dotUp = Vector3.Dot(up, Vector3.up);

            if (dotUp > 0.9f)
            {
                plane.gameObject.name = "FloorOrCeilingPlane";
                SetPlaneColor(plane, Color.green);
            }
            else if (Mathf.Abs(dotUp) < 0.2f)
            {
                plane.gameObject.name = "WallPlane";
                SetPlaneColor(plane, Color.blue);
            }
            else
            {
                plane.gameObject.name = "AngledPlane";
                SetPlaneColor(plane, Color.yellow);
            }
        }
    }

    void SetPlaneColor(ARPlane plane, Color color)
    {
        var renderer = plane.GetComponent<MeshRenderer>();
        if (renderer == null || renderer.material == null) return;

        renderer.material.color = color;
    }
}