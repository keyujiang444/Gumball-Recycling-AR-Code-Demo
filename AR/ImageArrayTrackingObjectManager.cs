using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageArrayTrackingObjectManager : MonoBehaviour
{
    [SerializeField] [Tooltip("Image manager on the AR Session Origin")]
    ARTrackedImageManager m_ImageManager;

    /// <summary>
    /// Get the <c>ARTrackedImageManager</c>
    /// </summary>
    public ARTrackedImageManager ImageManager
    {
        get => m_ImageManager;
        set => m_ImageManager = value;
    }

    [SerializeField] [Tooltip("Reference Image Library")]
    XRReferenceImageLibrary m_ImageLibrary;

    /// <summary>
    /// Get the <c>XRReferenceImageLibrary</c>
    /// </summary>
    public XRReferenceImageLibrary ImageLibrary
    {
        get => m_ImageLibrary;
        set => m_ImageLibrary = value;
    }

    [SerializeField] [Tooltip("Prefab for tracked 1 image")]
    GameObject[] m_Prefabs;

    GameObject[] m_SpawnedPrefabs;

    int m_NumberOfTrackedImages;

    NumberManager[] m_NumberManagers;

    private void Start()
    {
        m_SpawnedPrefabs = new GameObject[m_ImageLibrary.count];
        m_NumberManagers = new NumberManager[m_ImageLibrary.count];

        for (int i = 0; i < m_ImageLibrary.count; i++)
        {
            m_SpawnedPrefabs[i] = Instantiate(m_Prefabs[i]);
            m_NumberManagers[i] = m_SpawnedPrefabs[i].GetComponent<NumberManager>();
        }
    }

    void OnEnable()
    {
        m_ImageManager.trackedImagesChanged += ImageManagerOnTrackedImagesChanged;
    }

    void OnDisable()
    {
        m_ImageManager.trackedImagesChanged -= ImageManagerOnTrackedImagesChanged;
    }

    void ImageManagerOnTrackedImagesChanged(ARTrackedImagesChangedEventArgs obj)
    {
        // added, spawn prefab
        foreach (ARTrackedImage image in obj.added)
        {
            Debug.Log("Image added：" + image.referenceImage.name);
            foreach (var mgr in m_NumberManagers)
            {
                mgr.Enable3DNumber(false);
            }

            for (int i = 0; i < m_NumberManagers.Length; i++)
            {
                var mgr = m_NumberManagers[i];
                if (image.referenceImage.guid == m_ImageLibrary[i].guid)
                {
                    mgr.Enable3DNumber(true);
                    m_SpawnedPrefabs[i].transform
                        .SetPositionAndRotation(image.transform.position, image.transform.rotation);
                }
            }
        }

        // updated, set prefab position and rotation
        foreach (ARTrackedImage image in obj.updated)
        {
            // image is tracking or tracking with limited state, show visuals and update it's position and rotation
            if (image.trackingState == TrackingState.Tracking)
            {
                foreach (var mgr in m_NumberManagers)
                {
                    mgr.Enable3DNumber(false);
                }

                for (int i = 0; i < m_NumberManagers.Length; i++)
                {
                    var mgr = m_NumberManagers[i];
                    if (image.referenceImage.guid == m_ImageLibrary[i].guid)
                    {
                        mgr.Enable3DNumber(true);
                        m_SpawnedPrefabs[i].transform
                            .SetPositionAndRotation(image.transform.position, image.transform.rotation);
                    }
                }
            }
            // image is no longer tracking, disable visuals TrackingState.Limited TrackingState.None
            else
            {
                foreach (var mgr in m_NumberManagers)
                {
                    mgr.Enable3DNumber(false);
                }
            }
        }

        // removed, destroy spawned instance
        foreach (ARTrackedImage image in obj.removed)
        {
            Debug.Log("Image removed：" + image.referenceImage.name);

            foreach (var mgr in m_NumberManagers)
            {
                mgr.Enable3DNumber(false);
            }
        }
    }

    public int NumberOfTrackedImages()
    {
        m_NumberOfTrackedImages = 0;
        foreach (ARTrackedImage image in m_ImageManager.trackables)
        {
            if (image.trackingState == TrackingState.Tracking)
            {
                m_NumberOfTrackedImages++;
            }
        }

        return m_NumberOfTrackedImages;
    }
}