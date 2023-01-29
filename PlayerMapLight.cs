using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class PlayerMapLight : MonoBehaviour
{
    
    Volume volume;
    public PlayerMapLightData playerLightData;
    public Light2D playerLight;
    void Start()
    {
        volume = GetComponent<Volume>();
        playerLight.color = playerLightData.color;
    }

   
    void Update()
    {
        //Mejor hacer por eventos
        if (Level_Manager.Instance.map_enabled)
        {          

            if (volume.profile.TryGet<Bloom>(out var bloom))
            {
                bloom.intensity.value = Mathf.PingPong(Time.time * playerLightData.velocity, playerLightData.maxIntensity);
            }
        }
    }
}
