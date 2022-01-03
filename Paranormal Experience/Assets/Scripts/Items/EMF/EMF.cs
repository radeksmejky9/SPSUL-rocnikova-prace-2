using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMF : MonoBehaviour
{
    bool is_enabled;
    public Material importedMat;
    private Material mat;
    public GameObject model;
    public Texture[] t;
    public AudioSource audio;
    private bool rdy = false;

    public void Start()
    {
        mat = new Material(importedMat.shader);
        mat.CopyPropertiesFromMaterial(importedMat);
        model.GetComponent<Renderer>().material = mat;
    }


    public bool Is_Enabled
    {
        get
        {
            return is_enabled;
        }
    }
    private void Update()
    {
        if (rdy && is_enabled)
            StartCoroutine(changeEMF());
        else if (!is_enabled)
            audio.Stop();

    }

    IEnumerator changeEMF()
    {
        rdy = false;
        if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<Ghost>().activeEvidences.TryGetValue(Ghost.Evidence.EMF, out bool emf))
        {
            if (emf)
            {
                var i = Random.Range(0, 5);
                mat.SetTexture("_EmissionMap", t[i]);
                if (i > 2)
                    audio.Play();
                else
                    audio.Stop();
            }
            else
            {
                var i = Random.Range(0, 3);
                mat.SetTexture("_EmissionMap", t[i]);
            }
        }

        yield return new WaitForSeconds(1f);
        rdy = true;
    }

    public void Switch()
    {
        is_enabled = !is_enabled;
        if (is_enabled)
        {
            rdy = true;
            mat.EnableKeyword("_EMISSION");
            mat.SetTexture("_EmissionMap", t[0]);
            //mat.SetColor("_EmissionColor", );
        }
        else
        {
            rdy = false;
            mat.DisableKeyword("_EMISSION");
        }

    }
}
