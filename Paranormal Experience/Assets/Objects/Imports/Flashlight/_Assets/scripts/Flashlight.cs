using UnityEngine;
using System.Collections;




public class Flashlight : MonoBehaviour
{
    public GameObject Lights;
    public AudioSource switch_sound;
    public ParticleSystem dust_particles;


    private Light spotlight;
    private Material ambient_light_material;
    private Color ambient_mat_color;
    private bool is_enabled = false;

    public bool Is_Enabled
    {
        get
        {
            return is_enabled;
        }
    }





    void Start()
    {
        spotlight = Lights.transform.Find("Spotlight").GetComponent<Light>();
        ambient_light_material = Lights.transform.Find("ambient").GetComponent<Renderer>().material;
        ambient_mat_color = ambient_light_material.GetColor("_TintColor");
    }


    public void Switch()
    {
        is_enabled = !is_enabled;

        Lights.SetActive(is_enabled);

        if (switch_sound != null)
            switch_sound.Play();
    }



    public void Enable_Particles(bool value)
    {
        if (dust_particles != null)
        {
            if (value)
            {
                dust_particles.gameObject.SetActive(true);
                dust_particles.Play();
            }
            else
            {
                dust_particles.Stop();
                dust_particles.gameObject.SetActive(false);
            }
        }
    }


}
