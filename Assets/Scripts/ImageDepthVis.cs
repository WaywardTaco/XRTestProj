using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ImageDepthVis : MonoBehaviour
{
    [SerializeField] private AROcclusionManager occlusionManager;
    [SerializeField] private RawImage rawImage;
    [SerializeField] private TMP_Text debugtext;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Texture2D texture = occlusionManager.environmentDepthTexture;
        if(texture != null){
            debugtext.text = "Has Texture";
            rawImage.texture = texture;
        }
        else
            debugtext.text = "Texture Null";
            
    }
}
