using UnityEngine;
using System.Collections;




public class Flashlight : MonoBehaviour
{
    public GameObject Lights;
    public AudioSource switch_sound;
    public ParticleSystem dust_particles;
    public int blinkingRadius = 10;
    private LayerMask lm;
    private float blinkCooldown = 0.3f;
    private float blinkTimer;

    private Light spotlight;
    private Material ambient_light_material;
    private Color ambient_mat_color;
    private bool is_enabled = false;
    bool switchedInLastFrame = false;

    public bool Is_Enabled
    {
        get
        {
            return is_enabled;
        }
    }


    private void Update()
    {
        blinkTimer += Time.deltaTime;
        var b = Physics.CheckSphere(this.transform.position, blinkingRadius, lm);
        if (b && blinkTimer > blinkCooldown)
        {
            this.SwitchWithoutSound();
            blinkTimer = 0;
            blinkCooldown = Random.RandomRange(0.05f, 0.4f);
        }


    }


    void Start()
    {
        lm = lm = 1 << 19;
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

    public void SwitchWithoutSound()
    {
        is_enabled = !is_enabled;

        Lights.SetActive(is_enabled);
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


