using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Battle_Tactics.Battle
{
    public abstract class UnitData
    {
        public Sprite Image { get; set; }
        public string UnitName { get; set; }
        public string UnitId { get; set; }
        public int AtkPoints { get; set; }
        public int HealthPoints { get; set; }
        public int ReadyPoints { get; set; }
        public int MovementPoints { get; set; }

        

        public void AttackEnemy()
        {

        }

        public void DefendFromEnemy()
        {

        }

        public void NewTurn()
        {

        }
    }
}
