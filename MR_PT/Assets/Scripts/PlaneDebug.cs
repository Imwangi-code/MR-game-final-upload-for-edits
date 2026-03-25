using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class PlaneDebug : MonoBehaviour
{
    public ARPlaneManager planeManager;
    // Update is called once per frame

    void Start()
    {
        Debug.Log("Plane Debug Started");
    }
    void Update()
    {
        Debug.Log("Plane Debug Update");

        if (planeManager == null)
        {
            Debug.Log("Plane Manager is null");
            return;
        }
        int count = 0;
        foreach (var plane in planeManager.trackables)
        {
            count++;
        }
        Debug.Log("Number of planes: " + count);

    }
}
