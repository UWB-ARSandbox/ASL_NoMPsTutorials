using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class DemoManager : MonoBehaviour
{
    DemoObject selectedObject;
    bool sendClaim = false;

    private void Start()
    {
        if (ASL.GameLiftManager.GetInstance().AmLowestPeer())
        {
            ASL.ASLHelper.InstantiateASLObject("SimpleDemoPrefabs/WorldOriginCloudAnchorObject", 
                Vector3.zero, Quaternion.identity, string.Empty, string.Empty, SpawnWorldOrigin);
        }
    }

    private void Update()
    {
        DemoObject demoObject = null;
        DemoObject previousObject = null;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                demoObject = hit.transform.GetComponent<DemoObject>();
            }
            //clicking on the selected object
            if (selectedObject == demoObject && selectedObject != null)
            {
                sendClaim = false;
            }
            //clicking on nothing
            else if (demoObject == null)
            {
                sendClaim = false;
            }
            //clicking on an object while no object is selected
            else if (selectedObject == null)
            {
                selectedObject = demoObject;
                sendClaim = true;
            }
            //clicking on another object while one is selected
            else
            {
                previousObject = selectedObject;
                selectedObject = demoObject;
                sendClaim = true;
            }
        }



        if (sendClaim)
        {
            selectedObject.gameObject.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
            {
                selectedObject.SelectObject(true);
                selectedObject.gameObject.GetComponent<ASL.ASLObject>().SendAndSetObjectColor(Color.yellow, selectedObject.DefaultColor);
            });
            if (previousObject != null)
            {
                previousObject.gameObject.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
                {
                    previousObject.SelectObject(false);
                    previousObject.gameObject.GetComponent<ASL.ASLObject>().SendAndSetObjectColor(
                        selectedObject.DefaultColor, selectedObject.DefaultColor);
                    previousObject = null;
                });
            }
        }
        else
        {
            if (selectedObject != null && selectedObject.IsSelected)
            {
                selectedObject.gameObject.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
                {
                    selectedObject.SelectObject(false);
                    selectedObject.gameObject.GetComponent<ASL.ASLObject>().SendAndSetObjectColor(
                        selectedObject.DefaultColor, selectedObject.DefaultColor);
                    selectedObject = null;
                });
            }
        }
    }

    void createDemoObjects()
    {
        ASL.ASLHelper.InstantiateASLObject("Demo_Owen/InteractableCube", new Vector3(-1.5f, 0, 0), 
            Quaternion.identity);
        ASL.ASLHelper.InstantiateASLObject("Demo_Owen/InteractableSphere", new Vector3(0, 0, 0), 
            Quaternion.identity);
        ASL.ASLHelper.InstantiateASLObject("Demo_Owen/InteractableCylinder", new Vector3(1.5f, 0, 0), 
            Quaternion.identity);
    }

    /// <summary>
    /// Spawns the world origin cloud anchor after the world origin object visualizer has 
    /// been created (blue cube)
    /// </summary>
    /// <param name="_worldOriginVisualizationObject">The game object that represents the 
    /// world origin</param>
    private static void SpawnWorldOrigin(GameObject _worldOriginVisualizationObject)
    {
        _worldOriginVisualizationObject.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
        {
            ASL.ASLHelper.CreateARCoreCloudAnchor(Pose.identity, _worldOriginVisualizationObject.
                GetComponent<ASL.ASLObject>(), _waitForAllUsersToResolve: false);
        });
    }
}
