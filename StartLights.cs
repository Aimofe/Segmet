using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class StartLights : MonoBehaviour
{
    Volume volume;
    public StarLightData starLightData;
    public Light2D[] starLight;
    void Start()
    {
        volume = GetComponent<Volume>();
        starLight[0].color = starLightData.color;
    }


    void Update()
    {
        starLight[0].intensity = Mathf.PingPong(Time.time * starLightData.velocity, starLightData.maxIntensity);
     
    }
}
