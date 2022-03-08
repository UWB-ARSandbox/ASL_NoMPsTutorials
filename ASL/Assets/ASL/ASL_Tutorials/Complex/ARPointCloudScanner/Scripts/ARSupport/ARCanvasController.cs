using UnityEngine;
using UnityEngine.UI;

/// <summary>UI Controller for the ASL_ARCorePointCloud tutorial.</summary>
public class ARCanvasController : MonoBehaviour
{
    /// <summary>Button to toggle point cloud capturing.</summary>
    public Button TogglePointCapture;

    /// <summary>Button to toggle a filter sphere for point capture filtering.</summary>
    public Button ToggleFilterSphere;

    /// <summary>Button to toggle background render color capture for points.</summary>
    public Button ToggleBackgroundColor;

    /// <summary>Button to clear all saved/persisted point cloud data.</summary>
    public Button ClearPoints;

    /// <summary>The tutorial gameworld model.  Handles UI controller to game state and links the ARPointCloudManagerExtension events to ASLParticleSystem.</summary>
    public GameWorld TheWorld;

    /// <summary>The ASLParticleSystem object component.  This is the ASL persisting particle system for the point cloud.</summary>
    public ASLParticleSystem ASLPCManager;

    /// <summary>The ARPointCloudManagerExtension object component.  This is normally a component in the AR Session Origin object.</summary>
    public ARPointCloudManagerExtension ARPointCloudMgr;

    /// <summary>Used to return to original button grey colors.</summary>
    private Color _originalButtonColor;

    /// <summary>Unity start method.  Will add button listeners and capture original button colors.</summary>
    void Start()
    {
        TogglePointCapture.onClick.AddListener(togglePointCloudEnabled);
        ToggleFilterSphere.onClick.AddListener(toggleFilterSphereEnabled);
        ToggleBackgroundColor.onClick.AddListener(toggleBackgroundColor);
        ClearPoints.onClick.AddListener(clearAllPoints);
        _originalButtonColor = TogglePointCapture.image.color;

    }

    /// <summary>
    /// Update method to control the TogglePointCloudEnabled button.  
    /// Will show the following colors:  
    /// Grey: point capture not enabled.  
    /// Red: point capture recording.  
    /// Yellow: Movement is too fast/slow.  Recording temporarily paused.
    /// </summary>
    void Update()
    {
        if (ARPointCloudMgr.ScanEnabled)
        {
            TogglePointCapture.image.color = ARPointCloudMgr.IsFiltering ? Color.yellow : Color.red;
        }
        else
        {
            TogglePointCapture.image.color = _originalButtonColor;
        }
    }

    /// <summary>
    /// Internal onClick helper for the TogglePointCapture button.  Will enable/disable point capture.
    /// Toggles the ARPointCloudManager.ScanEnabled bool.
    /// </summary>
    private void togglePointCloudEnabled()
    {
        ARPointCloudMgr.ScanEnabled = !ARPointCloudMgr.ScanEnabled;
    }

    /// <summary>
    /// Internal onClick helper for the ToggleFilterSphere button.  Will enable/disable a filter sphere.
    /// Toggles the ARPointCloudManager.SetFilterSphereEnabled bool.
    /// </summary>
    private void toggleFilterSphereEnabled()
    {
        ARPointCloudMgr.SetFilterSphereEnabled(!ARPointCloudMgr.GetFilterSphereEnabled());
    }

    /// <summary>
    /// Internal onClick helper for the ToggleBackgroundColor button.  Will either use the background texture points to generate 
    /// particle colors, or use a default color of Green.  Toggles the ARPointCloudManager.UseBackgroundPointColor bool.
    /// If default color is used, the button will trun green
    /// </summary>
    private void toggleBackgroundColor()
    {
        ARPointCloudMgr.UseBackgroundPointColor = !ARPointCloudMgr.UseBackgroundPointColor;
        ToggleBackgroundColor.image.color = ARPointCloudMgr.UseBackgroundPointColor ? _originalButtonColor : Color.green;
    }

    /// <summary>
    /// Internal onClick helper for the ClearPoints button.  Will remove all points from the persisting particle system.
    /// </summary>
    private void clearAllPoints()
    {
        TheWorld.ClearParticles();
    }
}
