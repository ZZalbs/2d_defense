using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class aStar : MonoBehaviour // 최단경로 탐색 알고리즘
{
    public GridMake gridCode;
    public Node start, end; //start, end 구현예정
    public Node[,] gridArray;


    public void FindPath(Node startNode,Node finalNode) // 경로 탐색
    {
        List<Node> openSet = new List<Node>();
        List<Node> closedSet = new List<Node>();
        openSet.Add(startNode);
        
        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if ( (openSet[i].fCost < currentNode.fCost) || (openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost) )
                {
                    currentNode = openSet[i];
                }
            }
            //Debug.Log("now : " + currentNode.gridX + "," + currentNode.gridY + ","+currentNode.walkable);

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            //도착지점에 오면 종료


            if (currentNode == end)
            {
                return;
            }

            if (currentNode != start)
            {
            }

            foreach (Node neighbour in gridCode.GetNeighbours(currentNode))  // neighbor : 현 노드의 이웃
            {
                //이동불가 노드 이거나 이미 검색한 노드 제외
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }
                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour); // 왔던 거리 + 갈 거리
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour; // g코 갱신
                    neighbour.hCost = GetDistance(neighbour, finalNode);
                    neighbour.parent = currentNode; // 부모 Node 갱신

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
    }

    //웨이포인트 생성(엔드 포인트부터 처음으로 다시 돌아오는 방식으로)
    public List<Node> RetracePath(Node startNode,Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        while(currentNode!=startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        
        path.Add(startNode);
        path.Reverse();
        return path;
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
        
        return (dstX + dstY) * 10;
    }

}