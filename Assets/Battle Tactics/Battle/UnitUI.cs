using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Battle_Tactics.Battle
{
    public enum UnitLevel { One, Two, Three };
    public class UnitUI : MonoBehaviour
    {
        public Image image;
        public TMP_Text unitName;
        public TMP_Text unitId;

        public TMP_Text atkPoints;
        public TMP_Text healthPoints;
        public TMP_Text readyPoints;
        public TMP_Text movementPoints;

        public Outline UIOutline;

        public UnitLevel unitLvl;

        public UnitData data;
        public void Initiliaze(UnitData data)
        {
            this.data = data;
            this.name = data.UnitId;

            image.sprite = data.Image;
            unitName.text = data.UnitName;
            unitId.text = data.UnitId;

            atkPoints.text = data.AtkPoints.ToString();
            healthPoints.text = data.HealthPoints.ToString();
            readyPoints.text = data.ReadyPoints.ToString();
            movementPoints.text = data.MovementPoints.ToString();
        }

        public void UpdateValues()
        {
            atkPoints.text = data.AtkPoints.ToString();
            healthPoints.text = data.HealthPoints.ToString();
            readyPoints.text = data.ReadyPoints.ToString();
            movementPoints.text = data.MovementPoints.ToString();
        }

        public Vector2Int currentPos;

        public void MoveToPosition(Vector3 position)
        {
            RectTransform rect = transform.GetComponent<RectTransform>();

            rect.position = position;
        }

        public void EnableOutline()
        {
            UIOutline.enabled = true;
        }

        public void DisableOutline()
        {
            UIOutline.enabled = false;
        }
    }
}
