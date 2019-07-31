using System.Collections.Generic;
using GoogleARCore;
using GoogleARCore.Examples.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public Camera viewCam;
    public GameObject startButton;
    public GameObject pregameText;
    
    private bool foundGround = false;
    private Pose groundPose;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!foundGround)
        {
            // Raycast against the location to search for planes.
            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
                TrackableHitFlags.FeaturePointWithSurfaceNormal;

            if (Frame.Raycast(viewCam.transform.position.x, viewCam.transform.position.y, raycastFilter, out hit))
            {
                // Use hit pose and camera pose to check if hittest is from the
                // back of the plane.
                if ((hit.Trackable is DetectedPlane) &&
                    Vector3.Dot(viewCam.transform.position - hit.Pose.position,
                        hit.Pose.rotation * Vector3.up) < 0)
                {
                    Debug.Log("Hit at back of the current DetectedPlane");
                }
                else
                {
                    if (hit.Trackable is DetectedPlane)
                    {
                        DetectedPlane detectedPlane = hit.Trackable as DetectedPlane;
                        if (detectedPlane.PlaneType == DetectedPlaneType.HorizontalUpwardFacing)
                        {
                            groundPose = hit.Pose;
                            foundGround = true;
                            pregameText.SetActive(false);
                            SpawnController.instance.SetGroundPose(groundPose);
                            startButton.SetActive(true);
                        }
                    }
                }
            }
        }
    }
}
