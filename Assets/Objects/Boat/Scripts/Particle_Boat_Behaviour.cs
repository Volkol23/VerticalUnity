using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Boat_Behaviour : MonoBehaviour
{
    private new ParticleSystem particleSystem;

    private ParticleSystem.MainModule mainModule;

    [SerializeField]
    private Color particleColor;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        mainModule = particleSystem.main;
    }

    // Update is called once per frame
    void Update()
    {
        mainModule.startColor = particleColor;
    }
}
