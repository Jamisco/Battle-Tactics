using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Battle_Tactics.Battle.Units
{
    public class Tank : UnitData
    {
        public const int Attack = 25;
        public const int Health = 250;
        public const int Readiness = 100;
        public const int Movement = 5;

        float[] multiplier = { 1.0f, 1.35f, 1.7f };
        public Tank(UnitLevel ut, Sprite image)
        {
            float mp = multiplier[(int)ut];

            Image = image;
            UnitName = "Tank" + "(" + ((int)ut + 1).ToString() + ")";
            // generate a random number for the unitId
            UnitId = UnityEngine.Random.Range(1, 500).ToString() + " Tank";

            AtkPoints = (int)Math.Floor(Attack * mp);
            HealthPoints = (int)Math.Floor(Health * mp);
            ReadyPoints = (int)Math.Floor(Readiness * mp);
            MovementPoints = (int)Math.Floor(Movement * mp);

        }
    }
}
