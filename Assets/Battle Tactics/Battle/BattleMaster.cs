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
    public class BattleMaster : MonoBehaviour
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

        public void SpawnUnit()
        {
            UnitData data = unitCreator.CreateUnit(UnitType.Infantry, UnitLevel.One);

            Vector3 position = gridManager.GridToWorldPostion(spawnLocation);
            position.z += -.1f;
            UnitUI unitUI = Instantiate(prefab, UnitCanvas.transform);
            unitUI.Initiliaze(data);

            RectTransform rect = unitUI.GetComponent<RectTransform>();

            rect.position = position;
        }
        public void Update()
        {
            
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(BattleMaster))]
public class BattleMasterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BattleMaster exampleScript = (BattleMaster)target;

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

