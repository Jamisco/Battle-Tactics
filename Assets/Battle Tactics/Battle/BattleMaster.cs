using Assets.Battle_Tactics.Battle;
using Assets.Battle_Tactics.World;
using Assets.Battle_Tactics.World.VisualDatas;
using GridMapMaker;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Assets.Battle_Tactics.Battle
{
    [Serializable]
    public class BattleMaster : MonoBehaviour
    {
        public Worldmap worldMap;
        public UnitCreator unitCreator;
        public UnitUI prefab;

        public Canvas UnitCanvas;


        public Dictionary<Vector2Int, UnitUI> battleUnits = new Dictionary<Vector2Int, UnitUI>();

        public int troopAmountPerside = 10;
        private void Awake()
        {
            Init();

        }
        private void Start()
        {
            SpawnUnits();
        }

        public void Init()
        {
            worldMap = FindAnyObjectByType<Worldmap>();
            unitCreator = FindAnyObjectByType<UnitCreator>();
        }

        public void SpawnUnits()
        {
            Clear();

            Vector2Int gridSize = worldMap.gridManager.GridSize;


            // Calculate the dimensions for the block of troops based on the amount per side
            int blockSide = Mathf.CeilToInt(Mathf.Sqrt(troopAmountPerside));  // Size of the block (square root)

            // Side 1: Bottom-left corner troop spawning
            for (int x = 0; x < blockSide; x++)
            {
                for (int y = 0; y < blockSide; y++)
                {
                    if (x * blockSide + y >= troopAmountPerside) break;  // Stop if exceeding troop amount

                    // Calculate the grid position for Side 1 in bottom-left
                    Vector2Int spawnPosition = new Vector2Int(x, y);

                    SpawnUnit(ArmyColor.Green, spawnPosition);
                }
            }

            // Side 2: Top-right corner troop spawning
            for (int x = 0; x < blockSide; x++)
            {
                for (int y = 0; y < blockSide; y++)
                {
                    if (x * blockSide + y >= troopAmountPerside) break;  // Stop if exceeding troop amount

                    // Calculate the grid position for Side 2 in top-right
                    Vector2Int spawnPosition = new Vector2Int(gridSize.x - 1 - x, gridSize.y - 1 - y);

                    SpawnUnit(ArmyColor.Yellow, spawnPosition);
                }
            }
        }

        public void SpawnUnit(ArmyColor color, Vector2Int gridPos)
        {
            // make random level
            UnitLevel lvl = UnityEngine.Random.Range(0, 3) == 0 ? UnitLevel.One : UnityEngine.Random.Range(0, 3) == 1 ? UnitLevel.Two : UnitLevel.Three;

            //make random unittype

            UnitType ut = UnityEngine.Random.Range(0, 3) == 0 ? UnitType.Infantry : UnityEngine.Random.Range(0, 3) == 1 ? UnitType.Tank : UnitType.Artillery;

            UnitData data = unitCreator.CreateUnit(color, ut, lvl);

            Vector3 position = worldMap.gridManager.GridToWorldPostion(gridPos);
            position.z += -.1f;
            UnitUI unitUI = Instantiate(prefab, UnitCanvas.transform);
            unitUI.Initiliaze(data);

            RectTransform rect = unitUI.GetComponent<RectTransform>();

            rect.position = position;

            battleUnits.Add(gridPos, unitUI);
            unitUI.currentPos = gridPos;
        }

        UnitUI selectedUnit = null;
        public void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Vector2Int gridPos = worldMap.GetMouseGridPos();
                Vector3Int gp = (Vector3Int)gridPos;

                Vector3 worldPos = worldMap.gridManager.GridToWorldPostion(gridPos);

                if (worldMap.gridManager.GridBounds.Contains(gp))
                {
                    if (battleUnits.ContainsKey(gridPos))
                    {
                        if (selectedUnit != null)
                        {
                            selectedUnit.DisableOutline();
                        }

                        selectedUnit = battleUnits[gridPos];
                        selectedUnit.EnableOutline();
                    }
                    else
                    {
                        if (selectedUnit != null)
                        {
                            battleUnits.Remove(selectedUnit.currentPos);
                            battleUnits.Add(gridPos, selectedUnit);

                            selectedUnit.MoveToPosition(worldPos);
                            selectedUnit.currentPos = gridPos;

                            selectedUnit.DisableOutline();
                            selectedUnit = null;
                        }
                    }
                }


            }
        }



        private void OnApplicationQuit()
        {
            // destroy all units

            Clear();

        }

        public void Clear()
        {
#if UNITY_EDITOR

            foreach (var item in battleUnits)
            {
                DestroyImmediate(item.Value.gameObject);
            }

#else
            foreach (var item in battleUnits)
            {
                Destroy(item.Value.gameObject);
            }
#endif
            battleUnits.Clear();
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

        if (GUILayout.Button("Spawn units"))
        {
            exampleScript.SpawnUnits();
        }


    }
}
#endif

