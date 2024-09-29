using Assets.Battle_Tactics.World.VisualDatas;
using Assets.World;
using Assets.World.VisualDatas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GridMapMaker.Tutorial
{
    public class Biosphere : MonoBehaviour
    {
        [SerializeField]
        public LandscapeProperties lProps;

        Dictionary<Vector2Int, ShapeVisualData> biomeData = new Dictionary<Vector2Int, ShapeVisualData>();

        Dictionary<Vector2Int, ShapeVisualData> snowBiomeData = new Dictionary<Vector2Int, ShapeVisualData>();

        LavaVisualData lavaVData;

        public void SetBiomeData(ref NoiseGenerator noiseGen, Vector2Int planetSize)
        {
            float land, rain, temp;
            biomeData.Clear();
            usedVData.Clear();
            snowBiomeData.Clear();
            usedSnowVData.Clear();

            lavaVData = new LavaVisualData(lProps.lavaMaterial);

            Vector2Int pos;

            for (int x = 0; x < planetSize.x; x++)
            {
                for (int y = 0; y < planetSize.y; y++)
                {
                    land = noiseGen.GetLandNoise(x, y);
                    rain = noiseGen.GetRainNoise(x, y);
                    temp = noiseGen.GetTempNoise(x, y);

                    pos = new Vector2Int(x, y); 

                    biomeData.Add(pos, GetBiome(land, rain, temp));
                }
            }
        }

        public Dictionary<int, ShapeVisualData> usedVData = new Dictionary<int, ShapeVisualData>();
        public Dictionary<int, ShapeVisualData> usedSnowVData = new Dictionary<int, ShapeVisualData>();

        private ShapeVisualData GetBiome(float land, float rain, float temp)
        {
            Landscape ls;

            if (land > lProps.lavaHeight)
            {
                ls = Landscape.Land;
            }
            else
            {
                ls = Landscape.Lava;
            }

            switch (ls)
            {
                case Landscape.Land:

                    int r = Mathf.RoundToInt(rain * 10);
                    int t = Mathf.RoundToInt(temp * 10);

                    (int, int) rt = (r, t);

                    if (usedVData.ContainsKey(rt.GetHashCode()))
                    {
                        return usedVData[rt.GetHashCode()];
                    }

                    LandVisualProps lvp = new LandVisualProps();

                    lvp.Temperature = (t / 10f);
                    lvp.Rain = (r / 10f);

                    LandVisualData v = new LandVisualData(lvp, lProps.textureGenerator);

                    usedVData.Add(rt.GetHashCode(), v);

                    return v;

                case Landscape.Lava:
                    return lavaVData;
                default:
                    return null;
            }
        }
        public ShapeVisualData GetBiomeVData(Vector2Int pos)
        {
            return biomeData[pos];
        }

        public ShapeVisualData GetSnowBiomeVData(Vector2Int pos)
        {
            return snowBiomeData[pos];
        }

        public Color GetBiomeColor(Vector2Int pos)
        {
            return biomeData[pos].mainColor;
        }

        public (List<Vector2Int>, List<ShapeVisualData>) GetBiomeData()
        {
            return (biomeData.Keys.ToList(), biomeData.Values.ToList());
        }

        public enum Landscape
        {
            Land,
            Lava
        }

        [Serializable]
        public struct LandscapeProperties
        {
            public float lavaHeight;
            public Shader textureGenerator;
            public Material lavaMaterial;
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(Biosphere))]
        public class ClassButtonEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                Biosphere myScript = (Biosphere)target;


            }
        }
#endif
    }
}
