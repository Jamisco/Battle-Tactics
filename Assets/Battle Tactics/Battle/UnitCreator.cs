using Assets.Battle_Tactics.Battle.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Battle_Tactics.Battle
{
    public enum UnitType
    {
        Infantry,
        Tank,
        Artillery
    }
    [Serializable]
    public class UnitCreator : MonoBehaviour
    {
        public FullUnitData Infantry;
        public FullUnitData Tank;
        public FullUnitData Artillery;

        [Serializable]
        public struct FullUnitData
        {
            public UnitType unitType;
            public Sprite[] Images;
        }

        public UnitData CreateUnit(UnitType ut, UnitLevel lvl)
        {
            UnitData unitData = null;
            Sprite sprite;

            switch (ut)
            {
                case UnitType.Infantry:
                    sprite = Infantry.Images[(int)lvl];

                    unitData = new Infantry(lvl, sprite);

                    break;
                case UnitType.Tank:
                    sprite = Infantry.Images[(int)lvl];

                    unitData = new Tank(lvl, sprite);
                    break;
                case UnitType.Artillery:
                    sprite = Infantry.Images[(int)lvl];

                    unitData = new Artillery(lvl, sprite);
                    break;
            }

            return unitData;

        }
    }
}
