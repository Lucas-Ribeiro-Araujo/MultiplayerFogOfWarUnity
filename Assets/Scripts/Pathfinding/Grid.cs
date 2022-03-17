using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlowDev.Pathfinding
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] bool displayGizmos;
        public LayerMask unwalkableMask;
        public Vector2 gridWorldSize;
        public float nodeRadius;
        Node[,] grid;

        float nodeDiameter;
        int gridSizeX, gridSizeY;

        private void Awake()
        {
            nodeDiameter = nodeRadius * 2;
            gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
            gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
            CreateGrid();
        }

        public int MaxSize
        {
            get { return gridSizeX * gridSizeY; }
        }

        private void CreateGrid()
        {
            grid = new Node[gridSizeX, gridSizeY];
            Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                    bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                    grid[x, y] = new Node(walkable, worldPoint, x, y);
                }
            }
        }

        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;

                    int checkX = node.gridX + x;
                    int checkY = node.gridY + y;

                    if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    {
                        neighbours.Add(grid[checkX, checkY]);
                    }
                }
            }

            return neighbours;
        }

        public Node NodeFromWorldPoint(Vector3 point)
        {
            float percentX = (point.x + gridWorldSize.x / 2) / gridWorldSize.x;
            float percentY = (point.z + gridWorldSize.y / 2) / gridWorldSize.y;
            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
            int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

            return grid[x, y];
        }

        public Node[] NodesFromWorldPointRadius(Vector3 point, float radius)
        {
            List<Node> nodesInBoundingBox = new List<Node>();

            Node centerNode = NodeFromWorldPoint(point);
            int radiusNodeCount = Mathf.RoundToInt(radius * nodeRadius);
            Debug.Log("radius node count = " + radiusNodeCount);
            int startPositionX = Math.Clamp(centerNode.gridX - radiusNodeCount, 0, gridSizeX);
            int startPositionY = Math.Clamp(centerNode.gridY - radiusNodeCount, 0, gridSizeY);


            for (int x = startPositionX; x <= startPositionX + radiusNodeCount * 2; x++)
            {
                for (int y = startPositionY; y <= startPositionY + radiusNodeCount * 2; y++)
                {
                    if (y > gridSizeY || x > gridSizeX) break;
                    Debug.Log("node " + x + "," + y + " is now visible");
                    nodesInBoundingBox.Add(grid[x, y]);
                }
            }

            foreach (Node n in nodesInBoundingBox)
            {
                n.visible = true;
                Debug.Log("node " + n.gridX + "," + n.gridY + " is now visible");
            }

            return new Node[0];
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position + (Vector3.up * 0.5f), new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

            if (grid != null && displayGizmos)
            {
                foreach (Node n in grid)
                {
                    //Gizmos.color = n.walkable ? Color.white : Color.red;
                    Gizmos.color = n.visible ? Color.white : Color.gray;
                    Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
                }
            }
        }
    }
}