using Assets.World.VisualDatas;
using GridMapMaker;
using System;
using UnityEngine;


namespace Assets.Battle_Tactics.World.VisualDatas
{
    internal class HighlightVisualData : ShapeVisualData
    {
        public HighlightVisualData(Material mat)
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
            return new HighlightVisualData(material);
        }
    }
}
