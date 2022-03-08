using UnityEngine;
using ASL;

/// <summary>World model for the ASL_ARCorePointCloud tutorial.</summary>
public class GameWorld : MonoBehaviour
{
    /// <summary>The root object tree representing ARCore player objects and components including the AR Camera and AR Session Origin</summary>
    public GameObject AndroidARRoot;

    /// <summary>The root object tree representing PC player specific objects and components including the Camera</summary>
    public GameObject PCPlayerRoot;

    /// <summary>The ARPointCloud ASL Extension object which is able to format and filter point data</summary>
    public ARPointCloudManagerExtension ARPointCloudMgr;

    /// <summary>The ASL ParticleSystem extension object specilizing in point cloud data</summary>
    public ASLParticleSystem ASLPointCloudMgr;

    /// <summary>
    /// Startup initialization for the point cloud game world
    /// </summary>
    void Start()
    {
        AndroidARRoot.SetActive(Application.isMobilePlatform);
        PCPlayerRoot.SetActive(!Application.isMobilePlatform);


        ARPointCloudMgr ??= FindObjectOfType<ARPointCloudManagerExtension>();
        ASLPointCloudMgr ??= FindObjectOfType<ASLParticleSystem>();

        ARPointCloudMgr.ListGenerated += processARParticleList;

        // Set world origin anchor point
        if (GameLiftManager.GetInstance().AmLowestPeer())
        {
            ASLHelper.InstantiateASLObject("SimpleDemoPrefabs/WorldOriginCloudAnchorObject", Vector3.zero, Quaternion.identity, string.Empty, string.Empty, SpawnWorldOrigin);
        }
    }

    /// <summary>
    /// Clears saved particles within the ASL PointCloud
    /// </summary>
    public void ClearParticles()
    {
        ASLPointCloudMgr.Clear();
    }

    /// <summary>
    /// Spawns the world origin cloud anchor after the world origin object visualizer has been created (blue cube)
    /// </summary>
    /// <param name="_worldOriginVisualizationObject">The game object that represents the world origin</param>
    private static void SpawnWorldOrigin(GameObject _worldOriginVisualizationObject)
    {
        _worldOriginVisualizationObject.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
        {
            // Bugged as of 2020.3.22f1.  Getting Null Reference exceptions with 
            // ARWorldOriginHelper.GetInstance().m_ARAnchorManager.AddAnchor((Pose)_hitResults);
            // and 
            // unable to host cloud anchor exceptions with m_ARCloudAnchor = ARWorldOriginHelper.GetInstance().m_ARAnchorManager.HostCloudAnchor(localAnchor);
            // AddAnchor is also deprecated.
            // Using AddComponent<ARAnchor>() to create anchors will need an overhaul of ARWorldOriginHelper and ASLHelper's Anchor methods
            // Repos with all other ASL ARCloudAnchor example scenes.
            ASLHelper.CreateARCoreCloudAnchor(Pose.identity, _worldOriginVisualizationObject.GetComponent<ASL.ASLObject>(), null, true, true);
        });
    }

    /// <summary>
    /// Adapts ARPersistingPointCloud tuple data into arraylist to send to ASLPointCloudManager 
    /// </summary>
    /// <param name="particleList">The particle list data in tuple format for position and color</param>
    private void processARParticleList(object sender, ASLPointCloudEventArgs args)
    {
        const int BATCH_SIZE = 250;

        var particleList = args.RawParticleList;
        if (particleList != null && particleList.Count > 0)
        {
            // batching for loop for list sizes over 250 (250x4 is at the limit of ASL float constraint)
            for (int batchIndex = 0; batchIndex < particleList.Count; batchIndex += BATCH_SIZE)
            {
                int pCount = particleList.Count - batchIndex > BATCH_SIZE ? BATCH_SIZE : particleList.Count - batchIndex;
                Vector3[] positions = new Vector3[pCount];
                Color[] colors = new Color[args.UseCustomColor ? pCount : 0];

                for (int i = 0; i < pCount; i++)
                {
                    positions[i] = particleList[i + batchIndex].position;
                    if (args.UseCustomColor)
                    {
                        colors[i] = particleList[i + batchIndex].color;
                    }
                }

                if (args.UseCustomColor)
                {
                    ASLPointCloudMgr.AddParticles(positions, colors);
                }
                else
                {
                    ASLPointCloudMgr.AddParticles(positions);
                }
            }
        }
    }
}
