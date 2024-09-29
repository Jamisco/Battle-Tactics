using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Battle_Tactics.Battle
{
    public class Infantry : UnitData
    {
        public const int Attack = 10;
        public const int Health = 100;
        public const int Readiness = 100;
        public const int Movement = 2;

        float[] multiplier = { 1.0f, 1.3f, 1.6f };
        public Infantry(UnitLevel ut, Sprite image)
        {
            float mp = multiplier[(int)ut];

            Image = image;
            UnitName = "Infantry" + "(" + ((int)ut + 1).ToString() + ")";
            // generate a random number for the unitId
            UnitId = UnityEngine.Random.Range(1, 500).ToString() + " Infantry";

            AtkPoints = (int)Math.Ceiling(Attack * mp);
            HealthPoints = (int)Math.Ceiling(Health * mp);
            ReadyPoints = (int)Math.Ceiling(Readiness * mp);
            MovementPoints = (int)Math.Ceiling(Movement * mp);

        }
    }
}
