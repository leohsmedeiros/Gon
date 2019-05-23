using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gon
{
    public class ColliderOnScreenCorners : MonoBehaviour
    {

        public float colDepth = 4f;
        public float zPosition = 0f;

        public bool topCorner = true;
        public bool rightCorner = true;
        public bool bottomCorner = true;
        public bool leftCorner = true;

        private Vector2 screenSize;
        private Vector3 cameraPos;


        private void AdjustScaleAndPosition (Transform cornerTransform)
        {
            if (cornerTransform.name.Equals("TopCorner"))
            {
                cornerTransform.localScale = new Vector3(screenSize.x * 2, colDepth, colDepth);
                cornerTransform.position = new Vector3(cameraPos.x, cameraPos.y + screenSize.y + (cornerTransform.localScale.y * 0.5f), zPosition);
            }
            else if (cornerTransform.name.Equals("RightCorner"))
            {
                cornerTransform.localScale = new Vector3(colDepth, screenSize.y * 2, colDepth);
                cornerTransform.position = new Vector3(cameraPos.x + screenSize.x + (cornerTransform.localScale.x * 0.5f), cameraPos.y, zPosition);
            }
            else if (cornerTransform.name.Equals("BottomCorner"))
            {
                cornerTransform.localScale = new Vector3(screenSize.x * 2, colDepth, colDepth);
                cornerTransform.position = new Vector3(cameraPos.x, cameraPos.y - screenSize.y - (cornerTransform.localScale.y * 0.5f), zPosition);
            }
            else if (cornerTransform.name.Equals("LeftCorner"))
            {
                cornerTransform.localScale = new Vector3(colDepth, screenSize.y * 2, colDepth);
                cornerTransform.position = new Vector3(cameraPos.x - screenSize.x - (cornerTransform.localScale.x * 0.5f), cameraPos.y, zPosition);
            }
        }

        // Use this for initialization
        void Start()
        {
            bool[] cornersEnable = new bool[] { topCorner, rightCorner, bottomCorner, leftCorner };
            string[] cornersName = new string[] { "TopCorner", "RightCorner", "BottomCorner", "LeftCorner" };

            //Generate world space point information for position and scale calculations
            cameraPos = Camera.main.transform.position;
            screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
            screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;

            for (int i=0; i< cornersEnable.Length; i++)
            {
                if (cornersEnable[i])
                {
                    Transform cornerTransform = new GameObject().transform;
                    cornerTransform.name = cornersName[i];
                    cornerTransform.gameObject.AddComponent<BoxCollider2D>();
                    cornerTransform.parent = transform;
                    AdjustScaleAndPosition(cornerTransform);
                }
            }
        }
    }

}