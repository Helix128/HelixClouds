using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Helix.Rendering
{
    public class Clouds : MonoBehaviour
    {
        Material material;
        public float cloudHeight = 100;
        public float cloudScale = 1;
        public Vector3 speed = Vector2.one;
        [Tooltip("Samples for the clouds. Less samples will make clouds appear flatter. However, a high sample count may heavily affect performance.")]
        public int samples = 4;
        [Range(0, 1)]
        public float coverage = 0.5f;
        public Mesh quad;
        public Camera camera;
        public Light sun;
        
   
        List<Matrix4x4> matrices = new List<Matrix4x4>();
        private void Update()
        {
            if (material == null)
            {
                material = new Material(Shader.Find("Shader Graphs/Cloud"));
                material.enableInstancing = true;
            }
            material.SetInt("Samples", samples);
            material.SetFloat("Coverage", coverage);
            material.SetFloat("Scale", cloudScale);
            material.SetVector("Sun", sun.transform.forward);
            material.SetVector("Speed", speed);
            Vector3 pos = new Vector3(camera.transform.position.x, cloudHeight, camera.transform.position.z);
            matrices.Clear();
            for (int i = 0; i < samples; i++)
            {
                Matrix4x4 matrix = Matrix4x4.TRS(pos+(Vector3.up*((float)i-((float)samples/2))*4),Quaternion.Euler(90,0,0),new Vector3(16000,16000,1));
                matrices.Add(matrix);
            }
            Graphics.DrawMeshInstanced(quad, 0, material, matrices.ToArray(), samples,null,UnityEngine.Rendering.ShadowCastingMode.Off,false,0,Camera.current);
        }
    }
}