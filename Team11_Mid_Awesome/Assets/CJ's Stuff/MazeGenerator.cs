using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private MazeCell _mazeCellPrefab; 
    [SerializeField] private int _mazeWidth;
    [SerializeField] private int _mazeDepth;
    [SerializeField] private GameObject _playerPrefab; // Reference to player prefab
    [SerializeField] private GameObject _exitTriggerPrefab; // Reference to exit trigger prefab


    private MazeCell[,] _mazeGrid;
    private List<MazeCell> outerWalls = new List<MazeCell>();
    private MazeCell _entranceCell;
    private MazeCell _exitCell;

    void Start()
    {
        _mazeGrid = new MazeCell[_mazeWidth, _mazeDepth];
        GenerateMazeGrid();
        GenerateMaze(null, _mazeGrid[0, 0]); 
        SetEntranceAndExit();
        PlacePlayerAtEntrance();
    }

    private void GenerateMazeGrid()
    {
        for (int x = 0; x < _mazeWidth; x++) 
        {
            for (int z = 0; z < _mazeDepth; z++)
            {
                _mazeGrid[x, z] = Instantiate(_mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity);
                
                // Identify outer walls
                if (x == 0 || x == _mazeWidth - 1 || z == 0 || z == _mazeDepth - 1)
                {
                    outerWalls.Add(_mazeGrid[x, z]);
                }
            }
        }
    }

    private void SetEntranceAndExit()
    {
        if (outerWalls.Count < 2) return;

        // Randomly select entrance
        _entranceCell = SelectRandomOuterWall();

        // Ensure the exit is not adjacent to the entrance
        do
        {
            _exitCell = SelectRandomOuterWall();
        } while (AreAdjacent(_entranceCell, _exitCell));

        // Clear walls for entrance and exit
        ClearWallForEntrance(_entranceCell);
        ClearWallForExit(_exitCell);

        // Place the exit trigger
        PlaceExitTrigger();
    }

    private void PlaceExitTrigger()
    {
        if (_exitTriggerPrefab != null && _exitCell != null)
        {
            // Position the trigger at the exit cell's location
            Vector3 triggerPosition = _exitCell.transform.position;
            GameObject exitTrigger = Instantiate(_exitTriggerPrefab, triggerPosition, Quaternion.identity);

            // Adjust the trigger orientation based on its wall
            AlignTriggerToWall(exitTrigger, _exitCell);
        }
    }

    private void AlignTriggerToWall(GameObject trigger, MazeCell cell)
    {
        // Adjust the trigger's rotation and position based on which wall is open
        if (cell.transform.position.x == 0)
        {
            // Align to the left wall
            trigger.transform.position += Vector3.left * 0.5f; // Offset to the left
            trigger.transform.rotation = Quaternion.Euler(0, 90, 0); // Rotate to face the player
        }
        else if (cell.transform.position.x == _mazeWidth - 1)
        {
            // Align to the right wall
            trigger.transform.position += Vector3.right * 0.5f;
            trigger.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if (cell.transform.position.z == 0)
        {
            // Align to the back wall
            trigger.transform.position += Vector3.back * 0.5f;
            trigger.transform.rotation = Quaternion.Euler(0, 0, 0); // No rotation needed
        }
        else if (cell.transform.position.z == _mazeDepth - 1)
        {
            // Align to the front wall
            trigger.transform.position += Vector3.forward * 0.5f;
            trigger.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private MazeCell SelectRandomOuterWall()
    {
        int index = Random.Range(0, outerWalls.Count);
        return outerWalls[index];
    }

    private bool AreAdjacent(MazeCell cell1, MazeCell cell2)
    {
        int x1 = (int)cell1.transform.position.x;
        int z1 = (int)cell1.transform.position.z;
        int x2 = (int)cell2.transform.position.x;
        int z2 = (int)cell2.transform.position.z;

        return (x1 == x2 && Mathf.Abs(z1 - z2) == 1) || (z1 == z2 && Mathf.Abs(x1 - x2) == 1);
    }

    private void ClearWallForEntrance(MazeCell entranceCell)
    {
        if (entranceCell.transform.position.x == 0) entranceCell.ClearLeftWall();
        if (entranceCell.transform.position.x == _mazeWidth - 1) entranceCell.ClearRightWall();
        if (entranceCell.transform.position.z == 0) entranceCell.ClearBackWall();
        if (entranceCell.transform.position.z == _mazeDepth - 1) entranceCell.ClearFrontWall();
    }

    private void ClearWallForExit(MazeCell exitCell)
    {
        if (exitCell.transform.position.x == 0) exitCell.ClearLeftWall();
        if (exitCell.transform.position.x == _mazeWidth - 1) exitCell.ClearRightWall();
        if (exitCell.transform.position.z == 0) exitCell.ClearBackWall();
        if (exitCell.transform.position.z == _mazeDepth - 1) exitCell.ClearFrontWall();
    }

    private void PlacePlayerAtEntrance()
    {
        // Check if a player already exists in the scene
        if (GameObject.FindWithTag("Player") == null)
        {
            if (_playerPrefab != null && _entranceCell != null)
            {
                Vector3 playerStartPosition = _entranceCell.transform.position;
                Instantiate(_playerPrefab, playerStartPosition, Quaternion.identity);
            }
        }
    }


    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell) 
    {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);

        MazeCell nextCell;

        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null) 
            {
                GenerateMaze(currentCell, nextCell);
            } 
        } while (nextCell != null);
    }

    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);
        
        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        int x = (int)currentCell.transform.position.x;
        int z = (int)currentCell.transform.position.z;

        if (x + 1 < _mazeWidth)
        {
            var cellToRight = _mazeGrid[x + 1, z];

            if (!cellToRight.IsVisited)
            {
                yield return cellToRight;
            }
        }

        if (x - 1 >= 0)
        {
            var cellToLeft = _mazeGrid[x - 1, z];

            if (!cellToLeft.IsVisited)
            {
                yield return cellToLeft;
            }
        }

        if (z + 1 < _mazeDepth)
        {
            var cellToFront = _mazeGrid[x, z + 1];

            if (!cellToFront.IsVisited)
            {
                yield return cellToFront;
            }
        }

        if (z - 1 >= 0 )
        {
            var cellToBack = _mazeGrid[x, z - 1];

            if (!cellToBack.IsVisited)
            {
                yield return cellToBack;
            }
        }
    }

    private void ClearWalls(MazeCell previousCell, MazeCell currentCell) 
    {
        if (previousCell == null) return;

        if (previousCell.transform.position.x < currentCell.transform.position.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
        }
        else if (previousCell.transform.position.x > currentCell.transform.position.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
        }
        else if (previousCell.transform.position.z < currentCell.transform.position.z)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
        }
        else if (previousCell.transform.position.z > currentCell.transform.position.z)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
        } 
    }

    void Update()
    {
        
    }
}
