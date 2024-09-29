using GridMapMaker;
using System;
using UnityEngine;

namespace Assets.Worldmap.VisualDatas
{
    [Serializable]
    public class LandVisualData : ShapeVisualData
    {
        public LandVisualProps landVProps;
        public LandVisualData(LandVisualProps visualProps, Shader shader)
        {
            landVProps = visualProps;
            this.shader = shader;
            SetMaterialPropertyBlock();
            VisualHash = GetVisualHash();
        }
        public override void SetMaterialPropertyBlock()
        {
            if (PropertyBlock == null)
            {
                PropertyBlock = new MaterialPropertyBlock();
            }

            PropertyBlock.Clear();

            PropertyBlock = landVProps.GetPropertyBlock();
        }

        public override int GetVisualHash()
        {
            return landVProps.GetHashCode();
        }
        public override ShapeVisualData DeepCopy()
        {
            return new LandVisualData(landVProps, shader);
        }
    }
        
    [SerializeField]
    public struct LandVisualProps
    {
        public float Temperature { get; set; }
        public float Rain { get; set; }

        public MaterialPropertyBlock GetPropertyBlock()
        {
            MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();

            propertyBlock.SetFloat("_Temperature", Temperature);
            propertyBlock.SetFloat("_Rain", Rain);

            return propertyBlock;
        }
    }
}
