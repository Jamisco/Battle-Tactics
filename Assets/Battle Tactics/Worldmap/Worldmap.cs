using GridMapMaker;
using GridMapMaker.Tutorial;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using Cinemachine;
using Assets.Battle_Tactics.Worldmap.VisualDatas;

[SerializeField]
public class Worldmap : MonoBehaviour
{
    public GridManager gridManager;
    public Biosphere biosphere;
    public NoiseGenerator noiseGenerator;
    //
    public bool blockInsert = false;

    [SerializeField]
    public MeshLayerSettings baseLayer;

    [SerializeField]
    public MeshLayerSettings highlightLayer;
    public Material highlightMat;

    public bool instantUpdate = false;

    CinemachineVirtualCamera vCam;
    CinemachineConfiner2D cameraConfiner;
    PolygonCollider2D polyCollider;

    HighlightVisualData highlightVData;

    public void Init()
    {
        vCam = FindAnyObjectByType<CinemachineVirtualCamera>();
        gridManager = GetComponentInChildren<GridManager>();
        polyCollider =  GetComponentInChildren<PolygonCollider2D>();

        biosphere = GetComponent<Biosphere>();
        noiseGenerator = GetComponent<NoiseGenerator>();
        cameraConfiner = vCam.GetComponent<CinemachineConfiner2D>();

        noiseGenerator.ComputeNoises(gridManager.GridSize);
        noiseGenerator.worldMap = this;

        highlightVData = new HighlightVisualData(highlightMat);
    }

    public void ComputeNoise()
    {
        noiseGenerator.ComputeNoises(gridManager.GridSize);
        biosphere.SetBiomeData(ref noiseGenerator, gridManager.GridSize);
    }

    public void Update()
    {
        if (noiseGenerator.NoiseModified && instantUpdate)
        {
            GenerateGrid();
        }

        HighlightMousePos();
    }

    bool working = false;
    Vector2Int previousHigh = Vector2Int.left;
    public void HighlightMousePos()
    {
        working = true;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2Int gridPos = gridManager.WorldToGridPosition(mousePos);

        if (gridPos != previousHigh)
        {
            if(previousHigh != Vector2Int.left)
            {
                gridManager.DeletePosition(previousHigh, highlightLayer.LayerId);
            }

            gridManager.InsertVisualData(gridPos, highlightVData, highlightLayer.LayerId);
            previousHigh = gridPos;

            Debug.Log("Position: " + gridPos.ToString());
        }

        gridManager.RedrawLayer(highlightLayer.LayerId);

        working = false;
    }

    public bool printTime = false;
    public bool generating = false;
    public void GenerateGrid()
    {
        if (generating)
        {
            return;
        }

        generating = true;

        ComputeNoise();

        TimeLogger.StartTimer(23, "Generate Time");

        gridManager.Initialize();
        gridManager.CreateLayer(baseLayer, true);
        gridManager.CreateLayer(highlightLayer, false);

        Vector2Int pos;
        ShapeVisualData vData;
        ShapeVisualData snowData;

        if (blockInsert)
        {
            var data = biosphere.GetBiomeData();

            gridManager.InsertPositionBlock(data.Item1, data.Item2);
        }
        else
        {
            for (int x = 0; x < gridManager.GridSize.x; x++)
            {
                for (int y = 0; y < gridManager.GridSize.y; y++)
                {
                    pos = new Vector2Int(x, y);
                    vData = biosphere.GetBiomeVData(pos);
                    gridManager.InsertVisualData(pos, vData);
                }
            }
        }

        gridManager.DrawGrid();

        TimeLogger.StopTimer(23);

        if (printTime)
        {  
            TimeLogger.Log(23);
            Debug.Log("Unique Biosphere Data: " + biosphere.usedVData.Count);
            Debug.Log("Unique Grid Data: " + gridManager.GetUniqueVisualHashes().Count);
        }

        TimeLogger.ClearTimers();

        Bounds mapBounds = gridManager.LocalBounds;

        polyCollider.points = new Vector2[] { new Vector2(mapBounds.extents.x, mapBounds.extents.y),
                                              new Vector2(-mapBounds.extents.x, mapBounds.extents.y),
                                              new Vector2(-mapBounds.extents.x, -mapBounds.extents.y),
                                              new Vector2(mapBounds.extents.x, -mapBounds.extents.y)};

        polyCollider.offset = mapBounds.center;

        cameraConfiner.m_BoundingShape2D = polyCollider;

        generating = false;
    }

    public float rotation = 0;
    private void OnValidate()
    {
        transform.localEulerAngles = new Vector3(0, rotation, 0);
    }

    public string location = "Assets/Worldmap/WorldmapData.txt";

    public (List<Vector2Int>, List<ShapeVisualData>) GetPositions()
    {
        List<Vector2Int> positions = new List<Vector2Int>();
        List<ShapeVisualData> visualDatas = new List<ShapeVisualData>();

        for (int i = 0; i <= gridManager.GridSize.x; i++)
        {
            for (int j = 0; j <= gridManager.GridSize.y; j++)
            {
                Vector2Int pos = new Vector2Int(i, j);
                ShapeVisualData vData = biosphere.GetBiomeVData(pos);

                positions.Add(pos);
                visualDatas.Add(vData);
            }
        }

        return (positions, visualDatas);
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(Worldmap))]
public class ClassButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Worldmap exampleScript = (Worldmap)target;

        if (GUILayout.Button("Init Grid"))
        {
            exampleScript.Init();
        }

        if (GUILayout.Button("Generate Grid"))
        {
            exampleScript.GenerateGrid();
        }

        if (GUILayout.Button("Save Grid"))
        {
            string s = exampleScript.gridManager.GetSerializeMap();
            File.WriteAllText(exampleScript.location, s);
        }

        if (GUILayout.Button("Load Grid"))
        {
            string s = File.ReadAllText(exampleScript.location);
            exampleScript.gridManager.DeserializeMap(s);
        }

        if (GUILayout.Button("Clear Grid"))
        {
            exampleScript.gridManager.Clear();
            exampleScript.generating = false;
        }
    }
}
#endif
