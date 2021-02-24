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
        public float VerticalFOV { get; set; }
        public float HorizontalFOV { get; set; }
        public float radius { get; set; }
        public float StartVerticalAngle { get; set; }

        public float GetHorizontalNBRays()
        { 
            return this.AngularResolution * 100.0f * 360.0f;
        }

        //public float GetVerticalStep()
        //{
        //    return this.VerticalFOV / (float)this.VerticalResolution;
        //}

        //public float GetHorizontalStep()
        //{
        //    return this.HorizontalFOV / (float)this.HorizontalResolution;
        //}
    }
}