using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarBackground : MonoBehaviour
{
    // Start is called before the first frame update
    public RawImage image;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        image.transform.Rotate(new Vector3(0f, 0f, Time.realtimeSinceStartup *0.001f));
    }
}
