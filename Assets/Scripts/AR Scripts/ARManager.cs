using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlaneObjectManager : MonoBehaviour
{
    [SerializeField] GameObject spawn_libro;
    [SerializeField] GameObject spawn_pipa;
    [SerializeField] GameObject spawn_penna;
    [SerializeField] GameObject spawn_altro;
    [SerializeField] GameObject button_onoff;

    bool button_touched = false;
    bool plane_detection = true;
    public TMP_Text planeDetectionText;

    string spawn_prefab;


    [SerializeField] GameObject xr_origin;
    GameObject spawned_object;
    bool object_spawned;
    ARRaycastManager arrayman;
    ARPlaneManager planeManager;
    [SerializeField] ARSession arsession;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    Image button_onoff_image;

    // Start is called before the first frame update
    void Start()
    {
        //xr_origin = GameObject.Find("XR Origin");
        //Debug.Log(xr_origin);
        object_spawned = false;
        arrayman = xr_origin.GetComponent<ARRaycastManager>();
        planeManager = xr_origin.GetComponent<ARPlaneManager>();
        planeDetectionText.text = "Rilevamento ripiani ON";
        button_onoff_image = button_onoff.GetComponent<Image>();
        button_onoff_image.color = Color.green;
    }


    // Update is called once per frame

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeScene("Main Menu");
        }
    }

    public void SpawnObject()
    {
        if (Input.touchCount > 0)
        {
            if(spawn_prefab == "spawn_penna")
            {
                SpawnObjectHelper(spawn_penna);
            }
            else if(spawn_prefab == "spawn_pipa")
            {
                SpawnObjectHelper(spawn_pipa);
            }
            else if (spawn_prefab == "spawn_libro")
            {
                SpawnObjectHelper(spawn_libro);
            }


        }
    }

    public void ChangeSpawnPrefab(string prefab_name)
    {
        spawn_prefab = prefab_name;
        button_touched = true;
        SpawnObject();
    }

    
    public void SpawnObjectHelper(GameObject prefab)
    {
        int x = (Screen.width/2);
        int y = (Screen.height/2);
        if(button_touched)
        {
            if (arrayman.Raycast(new Vector2(x,y), hits, TrackableType.PlaneWithinPolygon))
            {
                var hitpose = hits[0].pose;
                if (!object_spawned)
                {
                    spawned_object = Instantiate(prefab, hitpose.position, prefab.transform.rotation);
                    object_spawned = true;
                    arrayman.enabled = false;
                    planeManager.enabled = false;
                    plane_detection = false;
                    planeDetectionText.text = "Rilevamento ripiani OFF";
                    button_onoff_image.color = Color.red;

                }
                else
                {
                    Destroy(spawned_object);
                    spawned_object = Instantiate(prefab, hitpose.position, prefab.transform.rotation);
                }
            }
            button_touched = false;
        }else
        {
            if (arrayman.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
            {
                var hitpose = hits[0].pose;
                if (!object_spawned)
                {
                    spawned_object = Instantiate(prefab, hitpose.position, prefab.transform.rotation);
                    object_spawned = true;
                    arrayman.enabled = false;
                    planeManager.enabled = false;
                    plane_detection = false;
                    planeDetectionText.text = "Rilevamento ripiani OFF";
                    button_onoff_image.color = Color.red;

                }
                else
                {
                    Destroy(spawned_object);
                    spawned_object = Instantiate(prefab, hitpose.position, prefab.transform.rotation);
                }
            }
        }
    }

    public void ChangeScene(string sceneName)
    {
        arrayman.enabled = true;
        planeManager.enabled = true;
        Destroy(spawned_object);
        arsession.Reset();
        SceneManager.LoadScene(sceneName);
    }

    public void GiraSinistra()
    {
        spawned_object.transform.Rotate(new Vector3(0, 35, 0));
    }
    public void GiraDestra()
    {
        spawned_object.transform.Rotate(new Vector3(0, -35, 0));
    }


    public void FineTurorial(GameObject tutorial)
    {
        tutorial.SetActive(false);
    }

    public void EnablePlaneDetection()
    {
        if (plane_detection)
        {
            arrayman.enabled = false;
            planeManager.enabled = false;
            plane_detection = false;
            planeDetectionText.text = "Rilevamento ripiani OFF";
            button_onoff_image.color = Color.red;
        }
        else
        {
            arrayman.enabled = true;
            planeManager.enabled = true;
            plane_detection = true;
            planeDetectionText.text = "Rilevamento ripiani ON";
            button_onoff_image.color = Color.green;
        }
    }

    public void ResetPlane()
    {
        arrayman.enabled = true;
        planeManager.enabled = true ;
        planeDetectionText.text = "Rilevamento ripiani ON";
        button_onoff_image.color = Color.green;
        Destroy(spawned_object);
        arsession.Reset();
        object_spawned = false;
        spawn_prefab = null;

    }



}
