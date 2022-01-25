using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMF : Item, ISwitchable
{
    bool is_enabled;
    public int emfradius;
    private LayerMask lm;
    public Material importedMat;
    private Material mat;
    public GameObject model;
    public Texture[] t;
    public AudioSource audio;
    private bool rdy = false;
    private Dictionary<Ghost.Evidence, bool> evidences;

    public void Start()
    {
        mat = new Material(importedMat.shader);
        mat.CopyPropertiesFromMaterial(importedMat);
        model.GetComponent<Renderer>().material = mat;
        evidences = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Ghost>().activeEvidences;
        lm = 1 << 19;
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
        {
            StartCoroutine(changeEMF());
        }
        else if (!is_enabled)
            audio.Stop();
    }

    IEnumerator changeEMF()
    {
        rdy = false;
        if (evidences.TryGetValue(Ghost.Evidence.EMF, out bool emf))
        {
            var b = Physics.CheckSphere(this.transform.position, emfradius, lm);

            if (emf && b)
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
        }
        else
        {
            rdy = false;
            mat.DisableKeyword("_EMISSION");
        }

    }
}
