using System.Collections;
using System.Collections.Generic;
using UnityEngine;


abstract class Velodyne : MonoBehaviour
{

    [Range(0.1f, 0.4f)]
    public float AngularResolution;

    public struct RaycastConf
    {
        public float Distance { get; set; }
        public int VerticalResolution { get; set; }
        public float AngularResolution { get; set; }
        public float HorizontalResolution { get; set; }

        public float VerticalFOV { get; set; }
        public float VerticalFOVMin { get; set; }
        public float VerticalFOVMax { get; set; }

        public float HorizontalFOV { get; set; }
        public float radius { get; set; }
        public float StartVerticalAngle { get; set; }

        public float HorizontalStep { get; set; }
        public float VerticalStep { get; set; }

        public float GetHorizontalResolution()
        { 
            return this.AngularResolution * 10.0f * 360.0f;
        }

        public float GetVerticalStep()
        {
            return this.VerticalFOV / (float)this.VerticalResolution;
        }

        public float GetHorizontalStep()
        {
            return this.HorizontalFOV / (float)this.HorizontalResolution;
        }
    }

    public RaycastConf config = new RaycastConf();

    public float NoiseMin;
    public float NoiseMax;

    public float SizeParticle;

    public ComputeBuffer ParticleBuffer;
    public int numberOfParticles;

    public Material mt;

    public int layerMask;

    public struct ParticleData
    {
        public Vector4 Position;
        public Vector4 Color;
    }



    public List<ParticleData> Raycast(Vector3 TranformPosition)
    {

        RaycastHit hit;
        List<ParticleData> particle_list = new List<ParticleData>();

        float HorizontalAngle = 0;
        float HorizontalStep = config.HorizontalStep;
        float VerticalStep = config.VerticalStep;

        for (int i = 0; i < config.HorizontalResolution; ++i)
        {
            float direction_x = config.radius * Mathf.Cos(HorizontalAngle);
            float direction_z = config.radius * Mathf.Sin(HorizontalAngle);

            float VerticalAngle = config.VerticalFOVMin;

            for (int j = 0; j < config.VerticalResolution; ++j)
            {
                float direction_y = config.radius * Mathf.Sin(VerticalAngle);
                Vector3 vDirection = new Vector3(direction_x, direction_y, direction_z);

                if (Physics.Raycast(TranformPosition, vDirection, out hit, config.Distance, layerMask))
                {
                    Vector4 color1 = new Color(0.8f, 0.0f, 0.219f);

                    ParticleData p = new ParticleData();
                    Vector4 position = new Vector4(hit.point.x, hit.point.y, hit.point.z, 15.0f);
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
}