using Assets.Battle_Tactics.Battle;
using GridMapMaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Battle_Tactics.Battle
{
    [Serializable]
    public class UnitSpawner : MonoBehaviour
    {
        public GridManager gridManager;
        public UnitCreator unitCreator;
        public UnitUI prefab;

        public Canvas UnitCanvas;

        public Vector2Int spawnLocation;

        public void Init()
        {
            gridManager = FindAnyObjectByType<GridManager>();
            unitCreator = FindAnyObjectByType<UnitCreator>();
        }

        public float zp = 0;
        public void SpawnUnit()
        {
            UnitData data = unitCreator.CreateUnit(UnitType.Infantry, UnitLevel.One);

            Vector3 position = gridManager.GridToWorldPostion(spawnLocation);
            position.z += zp;
            UnitUI unitUI = Instantiate(prefab, UnitCanvas.transform);

            RectTransform rect = unitUI.GetComponent<RectTransform>();

            rect.position = position;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(UnitSpawner))]
public class UnitSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        UnitSpawner exampleScript = (UnitSpawner)target;

        if (GUILayout.Button("Init Grid"))
        {
            exampleScript.Init();
        }

        if (GUILayout.Button("Spawn unit"))
        {
            exampleScript.SpawnUnit();
        }


    }
}
#endif

