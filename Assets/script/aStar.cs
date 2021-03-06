using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class aStar : MonoBehaviour
{
    public GridMake gridCode;
    public Node start, end; //start, end 구현예정
    public Node[,] gridArray;

    //private void Start()
    //{
    //    gridCode = GetComponent<GridMake>();
    //}

    public void FindPath(Node startNode,Node finalNode)
    {
        List<Node> openSet = new List<Node>();
        List<Node> closedSet = new List<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            //OPEN에 fCOST가 가장 작은 노드를 찾기 -> 제일 가까운거를 본인으로
            for (int i = 1; i < openSet.Count; i++)
            {
                if ( (openSet[i].fCost < currentNode.fCost) || (openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost) )
                {
                    currentNode = openSet[i];
                }
            }
            Debug.Log(currentNode);

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            Debug.Log(currentNode);
            //도착지점에 오면 종료


            if (currentNode == end)
            {
                return;
            }

            if (currentNode != start)
            {
                // currentNode.ChangeColor = Color.Lerp(Color.cyan, Color.white, 0.2f);
            }
            Debug.Log(currentNode.gridX+ ","+currentNode.gridY+ "," + currentNode.start+ "," + currentNode.end + "," + currentNode.walkable);
            //이웃 노드를 검색

            foreach (Node neighbour in gridCode.GetNeighbours(currentNode))  // neighbor : 현 노드의 이웃
            {
                    
                //이동불가 노드 이거나 이미 검색한 노드 제외
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour); // 왔던 거리 + 갈 거리
                                      Debug.Log(currentNode.gridX+","+currentNode.gridY);
                                      Debug.Log(neighbour.gridX + "," + neighbour.gridY);
                //
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour; // g코 갱신
                    
                    
                    
                    // 수정 ㄱㄱㄱㄱㄱ
                    neighbour.hCost = GetDistance(neighbour, finalNode);

                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                        //if (neighbour.walkable && !neighbour.end)
                            //neighbour.ChangeColor = Color.Lerp(Color.green, Color.white, 0.2f);
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
            Debug.Log("("+currentNode.gridX+","+currentNode.gridY+")");
        }
        
        path.Add(startNode);
        path.Reverse();
        return path;
    }



    //노드간의 가로세로 거리 계산
    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
        
        return (dstX + dstY) * 10;
    }


}