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
    public enum ArmyColor
    {
        Green,
        Yellow
    }

    [Serializable]
    public class UnitCreator : MonoBehaviour
    {
        public ArmyData greenArmy;
        public ArmyData yellowArmy;

        [Serializable]
        public struct ArmyData
        {
            public ArmyColor armyColor;
            public FullUnitData Infantry;
            public FullUnitData Tank;
            public FullUnitData Artillery;
        }

        [Serializable]
        public struct FullUnitData
        {
            public UnitType unitType;
            public Sprite[] Images;
        }

        public UnitData CreateUnit(ArmyColor ac, UnitType ut, UnitLevel lvl)
        {
            UnitData unitData = null;
            Sprite sprite;

            ArmyData d;

            if(ac == ArmyColor.Green)
            {
                d = greenArmy;
            }
            else
            {
                d = yellowArmy;
            }

            switch (ut)
            {
                case UnitType.Infantry:
                    sprite = d.Infantry.Images[(int)lvl];

                    unitData = new Infantry(lvl, sprite);

                    break;
                case UnitType.Tank:
                    sprite = d.Tank.Images[(int)lvl];

                    unitData = new Tank(lvl, sprite);
                    break;
                case UnitType.Artillery:
                    sprite = d.Artillery.Images[(int)lvl];

                    unitData = new Artillery(lvl, sprite);
                    break;
            }

            return unitData;

        }
    }
}
