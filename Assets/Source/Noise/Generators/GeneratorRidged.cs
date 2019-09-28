using UnityEngine;
using System;

namespace SharpNoise.Generators
{

    public class GeneratorRidged : IGenerator
    {

        private PerlinNoise perlin;

        public float Amplitude { get; set; }
        public float Persistence { get; set; }
        public float Frequency { get; set; }
        public float FreqMulti { get; set; }
        public int Octaves { get; set; }

        private float offsetX;
        private float offsetY;
        private float offsetZ;
        private float offsetW;

        public GeneratorRidged(int seed, float amplitude = 1, float persistence = 0.5f,
            float frequency = 1f, float freqMulti = 2f, int octaves = 1)
        {
            this.perlin = new PerlinNoise(seed);
            this.Amplitude = amplitude;
            this.Persistence = persistence;
            this.Frequency = frequency;
            this.FreqMulti = freqMulti;
            this.Octaves = octaves;

            System.Random rand = new System.Random(seed);

            offsetX = (float)rand.NextDouble();
            offsetY = (float)rand.NextDouble();
            offsetZ = (float)rand.NextDouble();
            offsetW = (float)rand.NextDouble();
        }

        /// <summary>
        /// 1D Ridged Noise
        /// </summary>
        /// <param name="x">Location</param>
        /// <returns>Ridged Noise Normalized to -1, 1</returns>
        public float GetNoise1D(float x)
        {
            float curAmp = Amplitude;
            float curFreq = Frequency;
            float signal = perlin.GetNoise1D(x * Frequency + offsetX);
            signal = (1 - signal * signal) * Amplitude;

            float max = Amplitude;

            for (int i = 1; i < Octaves; i++) {
                curAmp *= Persistence;
                curFreq *= FreqMulti;
                float curSig = perlin.GetNoise1D(x * curFreq + offsetX);
                signal += (1 - curSig * curSig) * curAmp;
                max += curAmp;
            }

            return signal / max;
        }

        /// <summary>
        /// 2D Ridged Noise
        /// </summary>
        /// <param name="x">Location</param>
        /// <returns>Ridged Noise Normalized to -1, 1</returns>
        public float GetNoise2D(Vector2 x)
        {
            float curAmp = Amplitude;
            float curFreq = Frequency;
            float signal = perlin.GetNoise2D(x.x * Frequency + offsetX, x.y * Frequency + offsetY);
            signal = (1 - signal * signal) * Amplitude;

            float max = Amplitude;

            for (int i = 1; i < Octaves; i++) {
                curAmp *= Persistence;
                curFreq *= FreqMulti;
                float curSig = perlin.GetNoise2D(x.x * curFreq + offsetX, x.y * curFreq + offsetY);
                signal += (1 - curSig * curSig) * curAmp;
                max += curAmp;
            }

            return signal / max;
        }

        /// <summary>
        /// 3D Ridged Noise
        /// </summary>
        /// <param name="x">Location</param>
        /// <returns>Ridged Noise Normalized to -1, 1</returns>
        public float GetNoise3D(Vector3 x)
        {
            float curAmp = Amplitude;
            float curFreq = Frequency;
            float signal = perlin.GetNoise3D(x.x * Frequency + offsetX, x.y * Frequency + offsetY, x.z * Frequency + offsetZ);
            signal = (1 - signal * signal) * Amplitude;

            float max = Amplitude;

            for (int i = 1; i < Octaves; i++) {
                curAmp *= Persistence;
                curFreq *= FreqMulti;
                float curSig = perlin.GetNoise3D(x.x * curFreq + offsetX, x.y * curFreq + offsetY, x.z * curFreq + offsetZ);
                signal += (1 - curSig * curSig) * curAmp;
                max += curAmp;
            }

            return signal / max;
        }

        /// <summary>
        /// 4D Ridged Noise
        /// </summary>
        /// <param name="x">Location</param>
        /// <returns>Ridged Noise Normalized to -1, 1</returns>
        public float GetNoise4D(Vector4 x)
        {
            float curAmp = Amplitude;
            float curFreq = Frequency;
            float signal = perlin.GetNoise4D(x.x * Frequency + offsetX, x.y * Frequency + offsetY, x.z * Frequency + offsetZ, x.w * Frequency + offsetW);
            signal = (1 - signal * signal) * Amplitude;

            float max = Amplitude;

            for (int i = 1; i < Octaves; i++) {
                curAmp *= Persistence;
                curFreq *= FreqMulti;
                float curSig = perlin.GetNoise4D(x.x * curFreq + offsetX, x.y * curFreq + offsetY, x.z * curFreq + offsetZ, x.z * curFreq + offsetW);
                signal += (1 - curSig * curSig) * curAmp;
                max += curAmp;
            }

            return signal / max;
        }

    }

}
