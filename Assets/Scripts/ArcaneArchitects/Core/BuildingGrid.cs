using System;
using ArcaneArchitects.Input;
using UnityEngine;

namespace ArcaneArchitects.Core
{
    public class BuildingGrid : MonoBehaviour
    {
        [SerializeField] private PlayerController controller;
        [SerializeField] private new Camera camera;
        [SerializeField] private Transform cursor;

        [SerializeField] private int gridSize = 4;
        [SerializeField] private int cellSize = 1;

        private void Update()
        {
            var mouseScreenPos = controller.CursorPos;
            var readyToProjectPos = new Vector3(mouseScreenPos.x, mouseScreenPos.y, camera.transform.position.y);
            var worldPos = camera.ScreenToWorldPoint(readyToProjectPos);
            worldPos.y = transform.position.y;
            
            cursor.position = worldPos;
            
            Debug.Log(WorldToGridSpace(worldPos));
        }

        private GridCoord WorldToGridSpace(Vector3 pos)
        {
            var p = transform.position;
            var relativePos = p - pos;
            return new GridCoord(Mathf.FloorToInt(relativePos.x), Mathf.FloorToInt(relativePos.z));
        }

        private Vector3 GridToWorldSpace(GridCoord coord)
        {
            var p = transform.position;
            var offset = gridSize / 2f - cellSize / 2f;
            return new Vector3(p.x + (offset - coord.X * cellSize), p.y, p.z + (offset - coord.Y * cellSize));
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            var offset = gridSize / 2f - cellSize / 2f;
            var t = transform.position;
            for (var x = 0; x < gridSize; x++)
            {
                for (var z = 0; z < gridSize; z++)
                {
                    var pos = new Vector3(t.x + (offset - x * cellSize), t.y, t.z + (offset - z * cellSize));
                    Gizmos.DrawWireCube(pos, cellSize * Vector3.one);
                }
            }
        }
    }
}
