using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealInLight : MonoBehaviour
{
    public Material importedMat;
    public Material Mat;
    public Light SpotLight;
    public GameObject Flashlight;
    public bool isRevealed = false;
    private bool fp;

    private void Start()
    {
        Mat = new Material(importedMat.shader);
        Mat.CopyPropertiesFromMaterial(importedMat);
        GameObject[] items = GameObject.FindGameObjectsWithTag("Grabbable");
        foreach (var x in items)
        {
            if (x.layer == 13)
            {
                this.Flashlight = x;
                continue;
            }
        }

        this.SpotLight = Flashlight.transform.Find("lights").gameObject.transform.Find("Spotlight").gameObject.GetComponent<Light>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
            isRevealed = true;
    }

    void Update()
    {
        float lerp = mapValue(Vector3.Distance(SpotLight.transform.position, this.transform.position), 0, 12, 0f, 0.7f);
        if (Mat && SpotLight && isRevealed)
        {
            Mat.SetVector("MyLightPosition", SpotLight.transform.position);
            Mat.SetVector("MyLightDirection", -SpotLight.transform.forward);
            Mat.SetFloat("MyLightAngle", SpotLight.spotAngle);
            Mat.SetFloat("MyStrengthScalor", Mathf.Lerp(50, 0, lerp));
        }
        if (!Flashlight.GetComponent<Flashlight>().Is_Enabled)
        {
            Mat.SetVector("MyLightPosition", SpotLight.transform.position * 0);
            Mat.SetVector("MyLightDirection", -SpotLight.transform.forward * 0);
            Mat.SetFloat("MyLightAngle", 0);
        }

        float mapValue(float mainValue, float inValueMin, float inValueMax, float outValueMin, float outValueMax)
        {
            return (mainValue - inValueMin) * (outValueMax - outValueMin) / (inValueMax - inValueMin) + outValueMin;
        }

        this.GetComponent<MeshRenderer>().material = Mat;

    }
}
