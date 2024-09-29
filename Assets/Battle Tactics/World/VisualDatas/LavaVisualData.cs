using GridMapMaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Battle_Tactics.World.VisualDatas
{
    internal class LavaVisualData : ShapeVisualData
    {
        public LavaVisualData(Material mat)
        {
            material = mat;
            shader = mat.shader;
        }

        public override void SetMaterialPropertyBlock()
        {
            if (PropertyBlock == null)
            {
                PropertyBlock = new MaterialPropertyBlock();
            }
        }

        public override int GetVisualHash()
        {
            return material.GetHashCode();
        }

        public override ShapeVisualData DeepCopy()
        {
            return new LavaVisualData(material);
        }

    }
}
