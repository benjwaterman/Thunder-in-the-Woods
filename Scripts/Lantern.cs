using UnityEngine;
using System.Collections;

public class Lantern : MonoBehaviour
{
    public float FlickerVariance = 0.5f;
    public float FlickerSpeed = 0.1f;

    private GameObject lanternObject;
    private Light lanternLight;
    private float defaultIntensity;
    public float targetIntensity;
    private float flickerSpeed;

    void Start()
    {
        lanternObject = this.gameObject;
        lanternLight = lanternObject.GetComponent<Light>();
        defaultIntensity = lanternLight.intensity;
        targetIntensity = defaultIntensity + Random.Range(-FlickerVariance, FlickerVariance);
        flickerSpeed = FlickerSpeed;
    }

    void FixedUpdate()
    {
        if (targetIntensity == defaultIntensity)
            targetIntensity = defaultIntensity + Random.Range(-FlickerVariance, FlickerVariance);

        else if (targetIntensity > defaultIntensity)
        {
            flickerSpeed = FlickerSpeed;
            if (lanternLight.intensity >= targetIntensity)
                targetIntensity = defaultIntensity + Random.Range(-FlickerVariance, FlickerVariance);
        }

        else if (targetIntensity < defaultIntensity)
        {
            flickerSpeed = -FlickerSpeed;
            if (lanternLight.intensity <= targetIntensity)
                targetIntensity = defaultIntensity + Random.Range(-FlickerVariance, FlickerVariance);
        }

        lanternLight.intensity += flickerSpeed * Time.deltaTime;
    }
}
