using System;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
    public class ParticleSystemMultiplier : MonoBehaviour
    {
        // a simple script to scale the size, speed and lifetime of a particle system

        public float multiplier = 1;


        private void Start()
        {
            var systems = GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem system in systems)
            {
                ParticleSystem.MainModule main = system.main;
                ParticleSystem.MinMaxCurve minMaxCurve = main.startSize;
                minMaxCurve.constant *= multiplier;
                main.startSize = minMaxCurve;

                ParticleSystem.MinMaxCurve startSpeedCurve = main.startSpeed;
                startSpeedCurve.constant *= multiplier;
                main.startSpeed = startSpeedCurve;

                ParticleSystem.MinMaxCurve lifetimeCurve = main.startLifetime;
                lifetimeCurve.constant *= Mathf.Lerp(multiplier, 1, .5f);
                main.startLifetime = lifetimeCurve;
                system.Clear();
                system.Play();
            }
        }
    }
}
