using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width;
    public int height;
    public int borderSize;

    public GameObject tilePrefab;
    public GameObject[] gamePiecePrefabs;
    public Transform dotParent;

    private Tile[,] m_allTiles;
    private GamePiece[,] m_allGamePieces;
    private Tile m_clickedTile;
    private Tile m_targetTile;






    private void Awake()
    {
        m_allTiles = new Tile[width, height];
        m_allGamePieces = new GamePiece[width, height];
        
    }
    private void Start()
    {
        SetUpTiles();
        SetUpCamera();
        FillRandom();
    }

    private void SetUpTiles()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3(i, j, 0), Quaternion.identity,transform); // as GameObject?
                tile.name = "Tile " + i + "," + j;

                m_allTiles[i, j] = tile.GetComponent<Tile>();
                m_allTiles[i, j].Initialize(i, j, this);
            }
        }
    }

    private void SetUpCamera()
    {
        Camera.main.transform.position = new Vector3((float)(width-1) / 2, (float)(height-1) / 2, -10f);

        float aspectRatio = (float)Screen.width / (float)Screen.height;
        float verticalSize = (float)height / 2f + (float)borderSize;
        float horizontalSize = ((float)width / 2f + borderSize) / aspectRatio;

        Camera.main.orthographicSize = (verticalSize > horizontalSize) ? verticalSize : horizontalSize;
    }

    private GameObject GetRandomGamePiece()
    {
        int random = Random.Range(0, gamePiecePrefabs.Length);

        if (gamePiecePrefabs[random] == null)
        {
            Debug.LogError("Board: " + random + " does not contain a valid GamePiece");
        }

        return gamePiecePrefabs[random];
    }

    private void PlaceGamePiece(GamePiece gamePiece, int x, int y)
    {
        if(gamePiece == null)
        {
            Debug.LogError("Game piece does not contain a valid GamePiece");
            return;
        }

        gamePiece.transform.position = new Vector3(x, y, 0);
        gamePiece.SetCoordinate(x, y);

    }

    private void FillRandom()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject randomPiece = Instantiate(GetRandomGamePiece(), Vector3.zero, Quaternion.identity,dotParent);
                if(randomPiece != null)
                {
                    PlaceGamePiece(randomPiece.GetComponent<GamePiece>(), i, j);

                }
            }
        }
    }
   
    public void ClickTile(Tile tile)
    {
        if(m_clickedTile == null)
        {
            m_clickedTile = tile;
            print(m_clickedTile.name);
        }
    }

    public void DragToTile(Tile tile)
    {
        if (m_clickedTile != null)
        {
            m_targetTile = tile;

        }
    }

    public void ReleaseTile()
    {
        if(m_clickedTile != null && m_targetTile != null)
        {
            SwicthTiles(m_clickedTile, m_targetTile);
        }
    }

    private void SwicthTiles(Tile clickedTile, Tile targetTile)
    {
        m_clickedTile = null;
        m_targetTile = null;
    }

    

}
