using System.Collections;
using System.Collections.Generic;
using UnityEngine;


abstract class Ouster : MonoBehaviour
{
    public enum Channels
    {
        Channel_32 = 32,
        Channel_64 = 64,
        Channel_128 = 128
    };
    public Channels VerticalResolution;

    public enum HR
    {
        HR_512 = 512,
        HR_1024 = 1024,
        HR_2048 = 2048
    };
    public HR HorizontalResolution;

    public enum Conf
    {
        Uniform,
        Gradient,
        BelowHorizon
    };

    public Conf Configuration;

    public float NoiseMin;
    public float NoiseMax;

    public float SizeParticle;

    public ComputeBuffer ParticleBuffer;
    public int numberOfParticles;

    public Material mt;

    public int layerMask;

    public struct RaycastConf {
        public float Distance { get; set; }
        public int VerticalResolution { get; set; }
        public int HorizontalResolution { get; set; }
        public float VerticalFOV { get; set; }
        public float HorizontalFOV { get; set; }
        public float radius { get; set; }
        public float StartVerticalAngle { get; set; }

        public float GetVerticalStep()
        {
            return this.VerticalFOV / (float)this.VerticalResolution;
        }

        public float GetHorizontalStep()
        {
            return this.HorizontalFOV / (float)this.HorizontalResolution;
        }
    }


    public struct ParticleData
    {
        public Vector4 Position;
        public Vector4 Color;
    }


    public List<ParticleData> Raycast(RaycastConf config, Vector3 TranformPosition)
    {

        RaycastHit hit;
        List<ParticleData> particle_list = new List<ParticleData>();

        float HorizontalAngle = 0;
        float HorizontalStep = config.GetHorizontalStep(); 

        
        float VerticalStep = config.GetVerticalStep();

        for (int i = 0; i < config.HorizontalResolution; ++i)
        {
            float direction_x = config.radius * Mathf.Cos(HorizontalAngle);
            float direction_z = config.radius * Mathf.Sin(HorizontalAngle);

            float VerticalAngle = -(config.VerticalFOV / 2);

            for (int j = 0; j < config.VerticalResolution; ++j)
            {
                float direction_y = config.radius * Mathf.Sin(VerticalAngle);
                Vector3 vDirection = new Vector3(direction_x, direction_y, direction_z);

                if (Physics.Raycast(TranformPosition, vDirection, out hit, config.Distance, layerMask))
                {
                    Vector4 color1 = new Color(0.8f, 0.0f, 0.219f);

                    Debug.DrawRay(TranformPosition, vDirection * config.Distance, color1);

                    ParticleData p = new ParticleData();
                    Vector4 position = new Vector4(hit.point.x , hit.point.y , hit.point.z , 15.0f);
                    Vector3 positionNormalized = new Vector3(hit.point.x, hit.point.y, hit.point.z).normalized;
                    Vector4 color = new Color(positionNormalized.x, positionNormalized.y, positionNormalized.z, hit.distance);
                    p.Position = position;
                    p.Color = color;
                    particle_list.Add(p);
                }
                else
                {
                    ParticleData p = new ParticleData();
                    Vector4 position = new Vector4(0.0f, 0.0f, 0.0f, 0.0f); //Create particle in 0,0,0 for respect data list lenght
                    Vector4 color = new Color(0.8f, 0.0f, 0.219f);
                    p.Position = position;
                    p.Color = color;
                    particle_list.Add(p);
                }

                VerticalAngle += VerticalStep;
            };

            HorizontalAngle += HorizontalStep;
        };
        return particle_list;
    }

    private void OnDestroy()
    {
        ParticleBuffer.Release();
    }

    private static float RandomGaussian(float minValue, float maxValue)
    {
        float u, v, S;

        do
        {
            u = 2.0f * UnityEngine.Random.value - 1.0f;
            v = 2.0f * UnityEngine.Random.value - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0f);

        // Standard Normal Distribution
        float std = u * UnityEngine.Mathf.Sqrt(-2.0f * UnityEngine.Mathf.Log(S) / S);

        float mean = (minValue + maxValue) / 2.0f;
        float sigma = (maxValue - mean) / 3.0f;
        return UnityEngine.Mathf.Clamp(std * sigma + mean, minValue, maxValue);
    }

}

