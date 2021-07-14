using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MapManger : MonoBehaviour
{

    public GameObject ground;
    public CinemachineVirtualCamera vCamera;

    public float mapScale;
    public float targetScale;
    public float lerpStep;

    public float cameraField;
    public float textScale;
    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        mapScale = 1f;
        cameraField = vCamera.m_Lens.OrthographicSize;

        material = ground.GetComponent<MeshRenderer>().material;
        
        textScale = material.GetFloat("_Scale");
    }

    // Update is called once per frame
    void Update()
    {
        if (mapScale < targetScale)
        {
            mapScale += lerpStep * Time.deltaTime;
            ground.transform.localScale = new Vector3(mapScale, mapScale, mapScale);

            vCamera.m_Lens.OrthographicSize = 0.8f * cameraField * mapScale;
            material.SetFloat("_Scale", textScale / mapScale);
        }
    }

    public void setTargetScale(float scale)
    {
        targetScale = scale;
    }

    public Vector4 getEdge()
    {
        return new Vector4(5 * mapScale, -5 * mapScale, -5 * mapScale, 5 * mapScale);
    }
}
