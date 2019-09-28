using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SharpNoise;
using SharpNoise.Generators;
using MeshGeneration.Marching;

namespace ProceduralGeneration
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class GenerationTest : MonoBehaviour
    {

        MeshFilter meshFilter;

        public int width;
        public int height;
        public int depth;

        protected void Awake()
        {
            meshFilter = GetComponent<MeshFilter>();
            Generate();
        }

        // Use this for initialization
        protected void Start()
        {
            
        }

        // Update is called once per frame
        protected void Update()
        {
            if(Input.GetKeyDown(KeyCode.F)) {
                Generate();
            }
        }

        private void Generate()
        {
            var seed = System.DateTime.Now.Millisecond;
            var perlin = new GeneratorValue(seed, 1, 0.5f, 0.025f, 2, 4);

            float[] density = new float[width * height * depth];

            for(int k = 0; k < depth; k++) {
                for(int j = 0; j < height; j++) {
                    for (int i = 0; i < width; i++) {
                        density[i + j * width + k * width * height] = perlin.GetNoise3D(new Vector3(i, j, k));
                    }
                }
            }

            var cubeMarcher = new MarchingCubes(0.1f);

            var verts = new List<Vector3>();
            var indicies = new List<int>();

            cubeMarcher.GenerateMesh(density, width, height, depth, verts, indicies);
            Debug.Log(verts.Count);
            Debug.Log(indicies.Count);

            var mesh = new Mesh();
            mesh.SetVertices(verts);
            mesh.SetIndices(indicies.ToArray(), MeshTopology.Triangles, 0);
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();

            meshFilter.mesh = mesh;
        }

    }
}
