using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    protected class PlayableGridCell
    {
        public Bounds cellBounds;
        public bool isOccupied;
    };
    public static EnemySpawner Instance { get; private set; }

    public Transform player;

    public uint EnemyCounter = 5;
    public GameObject EnemyPrefab;
    public float PlayableGridCellSize = 2.2f;
    public int PlayerSafeCells = 1;

    PlayableGridCell[,] PlayableAreaGrid = null;
    private bool IsSpawningFinished = false;
    private float time;
    public float timeBetweenSpawns;

    public int healthOfAsteroids = 20;
    public int damageDealOfAsteroids = 15;

    public Vector3 minPoint;
    public Vector3 maxPoint;

    public bool ShowDebugDraw = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        time = Time.time;
    }

    public void Spawn(uint counter)
    {
        CreatePlayableGrid();
        MarkPlayerSafeArea();
        SpawnNewAsteroids(counter);
    }

    private void Start()
    {
        Spawn(5);
    }

    private void MarkPlayerSafeArea()
    {
        Vector2Int playerGridPos = GetCellCoordinates(player.transform.position);

        //The player ship's pivot is located in grid cell (playerGridPos.x,playerGridPos.y)
        //Define a square, that is with a side of PlayerSafeCells + 1;
        //We will mark all cells in that square as Occupied, so that no asteroids can spawn there
        int minX = Mathf.Max(0, playerGridPos.x - PlayerSafeCells);
        int maxX = Mathf.Min(playerGridPos.x + PlayerSafeCells, PlayableAreaGrid.GetLength(0) - 1);
        int minY = Mathf.Max(0, playerGridPos.y - PlayerSafeCells);
        int maxY = Mathf.Min(playerGridPos.y + PlayerSafeCells, PlayableAreaGrid.GetLength(1) - 1);

        for (int xIter = minX; xIter <= maxX; ++xIter)
        {
            for (int yIter = minY; yIter <= maxY; ++yIter)
            {
                PlayableAreaGrid[xIter, yIter].isOccupied = true;
            }
        }
    }

    private Vector3 GetPlayingFieldExtents()
    {
        return maxPoint - minPoint;
    }

    private void CreatePlayableGrid()
    {
        Vector3 playingFieldExtents = GetPlayingFieldExtents();
        Vector3 cellExtents = new Vector3(PlayableGridCellSize, 0.1f, PlayableGridCellSize);
        float halfCellSize = PlayableGridCellSize * 0.5f;

        //Determine size of the grid, based on the cell size and the playable area size
        int xSize = (int)Mathf.Floor(playingFieldExtents.x / PlayableGridCellSize);
        int zSize = (int)Mathf.Floor(playingFieldExtents.z / PlayableGridCellSize);
        PlayableAreaGrid = new PlayableGridCell[xSize, zSize];

        for (int i = 0; i < xSize; ++i)
        {
            for (int j = 0; j < zSize; ++j)
            {
                Vector3 cellOffset = new Vector3(i, 0, j) * PlayableGridCellSize + new Vector3(halfCellSize, playingFieldExtents.y, halfCellSize);
                Vector3 cellCenter = -playingFieldExtents * 0.5f + cellOffset;
                PlayableAreaGrid[i, j] = new PlayableGridCell
                {
                    cellBounds = new Bounds(cellCenter, cellExtents),
                    isOccupied = false
                };
            }
        }
    }

    private void SpawnNewAsteroids(uint enemyCounter)
    {
        List<Vector3> asteroidPositions = FindFreePositions(enemyCounter);
        for (int i = 0; i < asteroidPositions.Count; ++i)
        {
            GameObject asteroid = Instantiate(EnemyPrefab, asteroidPositions[i], Quaternion.identity);
        }
    }

    public List<Vector3> FindFreePositions(uint requestedPositionsCnt)
    {
        //Create a list of all guaranteedly unoccupied cells
        List<PlayableGridCell> freeCells = new List<PlayableGridCell>();

        for (int i = 0; i < PlayableAreaGrid.GetLength(0); ++i)
        {
            for (int j = 0; j < PlayableAreaGrid.GetLength(1); ++j)
            {
                if (PlayableAreaGrid[i, j].isOccupied == false)
                {
                    freeCells.Add(PlayableAreaGrid[i, j]);
                }
            }
        }

        var result = new List<Vector3>();
        for (uint i = 0; i < requestedPositionsCnt; ++i)
        {
            if (freeCells.Count > 0)
            {
                int chosenCellIndex = Random.Range(0, freeCells.Count);
                PlayableGridCell chosenCell = freeCells[chosenCellIndex];
                result.Add(chosenCell.cellBounds.center);
                freeCells.RemoveAt(chosenCellIndex);
            }
        }

        return result;
    }

    enum Axis
    {
        X = 0,
        Y = 1,
        Z = 2
    };

    private Vector2Int GetCellCoordinates(Vector3 position)
    {
        return new Vector2Int(GetCellCoordinate(position, Axis.X, Axis.X), GetCellCoordinate(position, Axis.Z, Axis.Y));
    }

    private int GetCellCoordinate(Vector3 position, Axis worldAxis, Axis gridAxis)
    {
        //Convert from world space to Grid space
        //-PlayingFieldExtents/2 is at 0,0
        //+PlayingFieldExtents/2 is at grid.Length, grid.Length
        int originCellIndex = PlayableAreaGrid.GetLength((int)gridAxis) / 2; //(0,0,0) in world space is in the middle of the grid
        int resultingCoordinate = (int)(Mathf.Floor((position[(int)worldAxis] / PlayableGridCellSize))) + originCellIndex;
        resultingCoordinate = Mathf.Clamp(resultingCoordinate, 0, PlayableAreaGrid.GetLength((int)gridAxis));
        return (int)resultingCoordinate;
    }

    void OnDrawGizmos()
    {
        //Used to draw some deubg display, so we can visualize what is on the grid, and the player safe area
        if (!ShowDebugDraw || PlayableAreaGrid == null) return;
        for (int i = 0; i < PlayableAreaGrid.GetLength(0); ++i)
        {
            for (int j = 0; j < PlayableAreaGrid.GetLength(1); ++j)
            {
                PlayableGridCell cell = PlayableAreaGrid[i, j];
                Bounds cellBounds = cell.cellBounds;
                Gizmos.color = cell.isOccupied ? Color.red : Color.green;
                Gizmos.DrawCube(cellBounds.center, cellBounds.size - new Vector3(0.05f, 0, 0.05f));
            }
        }
    }
}
