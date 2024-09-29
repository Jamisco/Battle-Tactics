using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Battle_Tactics.Battle
{
    public class CamMovement : MonoBehaviour
    {
        public float dragSpeed = 2f;        // Speed of dragging
        public float scrollSpeed = 10f;     // Speed of zooming
        public float minZoom = 5f;          // Minimum zoom level (for orthographic size)
        public float maxZoom = 20f;         // Maximum zoom level (for orthographic size)

        private Vector3 dragOrigin;

        void Update()
        {
            HandleDrag();
            HandleZoom();
        }

        // Method to handle camera dragging with mouse
        void HandleDrag()
        {
            if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
            {
                dragOrigin = Input.mousePosition;
                return;
            }

            if (Input.GetMouseButton(0)) // Holding the left mouse button
            {
                Vector3 difference = Camera.main.ScreenToWorldPoint(dragOrigin) - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                difference.z = 0; // Keep the z-axis steady (for 2D)

                transform.position += difference; // Move the camera
                dragOrigin = Input.mousePosition; // Update drag origin
            }
        }

        // Method to handle zoom with scroll wheel
        void HandleZoom()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel"); // Get scroll input

            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - scroll * scrollSpeed, minZoom, maxZoom);
        }
    }
}
