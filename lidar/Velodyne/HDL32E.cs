using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class HDL32E : Velodyne
{

    void Start()
    {
        layerMask = 1 << 8;
        layerMask = ~layerMask;

        config.Distance = 150.0f;
        config.AngularResolution = AngularResolution;
        config.VerticalResolution = 32;
        config.HorizontalResolution = config.GetHorizontalResolution();

        config.VerticalFOV = 0.71558499f;
        config.VerticalFOVMin = -0.52359878f;
        config.VerticalFOVMax = 15.0f;

        config.HorizontalFOV = 3.14f * 2.0f;
        config.radius = 1.0f;

        config.HorizontalStep = config.GetHorizontalStep();
        config.VerticalStep = config.GetVerticalStep();


        numberOfParticles = (int)(config.VerticalResolution * config.HorizontalResolution);
        ParticleBuffer = new ComputeBuffer(numberOfParticles, sizeof(float) * 8);

    }

    private void OnRenderObject()
    {
        //Set shader parameter /!\ -> Transorm inutile because lidar point are already in world matrix
        mt.SetPass(0);

        mt.SetBuffer("_ParticleDataBuff", ParticleBuffer);
        mt.SetFloat("_ParticleSize", SizeParticle);

        Graphics.DrawProceduralNow(MeshTopology.Triangles, 3 * 4, numberOfParticles);
    }


    void Update()
    {

        List<ParticleData> particledata = Raycast(transform.position);

        numberOfParticles = (int)(config.VerticalResolution * config.HorizontalResolution);
        ParticleBuffer = new ComputeBuffer(numberOfParticles, sizeof(float) * 8);
        ParticleBuffer.SetData(particledata);


    }

}