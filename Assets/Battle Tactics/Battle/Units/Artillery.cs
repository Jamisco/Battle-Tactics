using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Battle_Tactics.Battle.Units
{
    internal class Artillery : UnitData
    {
        public const int Attack = 15;
        public const int Health = 50;
        public const int Readiness = 100;
        public const int Movement = 1;

        float[] multiplier = { 1.30f, 1.5f, 2f };
        public Artillery(UnitLevel ut, Sprite image)
        {
            float mp = multiplier[(int)ut];

            Image = image;
            UnitName = "Tank" + "(" + ((int)ut).ToString() + ")";
            // generate a random number for the unitId
            UnitId = UnityEngine.Random.Range(1, 500).ToString() + " Tank";

            AtkPoints = (int)Math.Floor(Attack * mp);
            HealthPoints = (int)Math.Floor(Health * mp);
            ReadyPoints = (int)Math.Floor(Readiness * mp);
            MovementPoints = (int)Math.Floor(Movement * mp);

        }
    }
}
