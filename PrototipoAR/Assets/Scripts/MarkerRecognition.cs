using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class MarkerRecognition : MonoBehaviour
{
    [SerializeField]
    GameObject[] trackableGos;

    [SerializeField]
    private ARTrackedImageManager imageManager;
    Dictionary<string, GameObject> ARGameobjects = new Dictionary<string, GameObject>();

    private void Awake()
    {
        //Each of the trackable objects added with the corresponding key
        ARGameobjects.Add("Mark_Cube_1", trackableGos[0]);
        ARGameobjects.Add("Mark_Cube_2", trackableGos[1]);
        ARGameobjects.Add("Mark_Cube_3", trackableGos[2]);
        ARGameobjects.Add("Mark_Cube_4", trackableGos[3]);
        ARGameobjects.Add("Mark_Cube_5", trackableGos[4]);
        ARGameobjects.Add("Mark_Cube_6", trackableGos[5]);
        
    }

    void Start()
    {
        for (int i = 0; i < trackableGos.Length; i++) trackableGos[i].SetActive(false);
    }


    void Update()
    {
        
    }

    public void OnEnable() => imageManager.trackedImagesChanged += OnImageChanged;    
    public void OnDisable() => imageManager.trackedImagesChanged -= OnImageChanged;
    

    public void OnImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            //Debug.Log("Image added: "+newImage.name);
            UpdateImage(newImage);
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            //Debug.Log("Image updated: " + updatedImage.name);
            UpdateImage(updatedImage);
        }

        foreach (var removedImage in eventArgs.removed)
        {
            //Debug.Log("Image removed: " + removedImage.name);
            RemoveImage(removedImage);
        }
    }

    private void UpdateImage(ARTrackedImage image)
    {
       
        string name = image.referenceImage.name;
        if (ARGameobjects.ContainsKey(name))
        {
            GameObject trackGo = ARGameobjects[name];
            trackGo.transform.position = image.transform.position;
            trackGo.transform.rotation = image.transform.rotation;
            if (image.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
            {
                //Debug.Log("State: Tracking");
                if (!trackGo.activeSelf) trackGo.SetActive(true);
                trackGo.SendMessage("InSight");
                //Deactivate all other prefabs
                foreach (var marker in ARGameobjects)
                {
                    if (marker.Key != name) marker.Value.SendMessage("OutSight");
                }
            }
            else if (image.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited)
            {
                //Debug.Log("State: Limited");
                trackGo.SendMessage("OutSight");
            }
            else
            {
                //Debug.Log("State: None");
                trackGo.SendMessage("OutSight");
            }
        }
    }

    private void RemoveImage(ARTrackedImage image)
    {
        
        
    }
}
