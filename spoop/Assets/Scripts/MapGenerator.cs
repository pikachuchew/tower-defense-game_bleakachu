using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject MapTile;
    public Color pathColor;

    [SerializeField] private int mapWidth;
    [SerializeField] private int mapHeight;

    private List<GameObject> mapTiles = new List<GameObject>();
    private List<GameObject> pathTiles = new List<GameObject>();

    private bool reachedX = false;
    private bool reachedY = false;
    private GameObject currentTile;
    private int currentIndex;
    private int nextIndex;
    private void Start()
    {
        generateMap();
    }

    private List<GameObject> getTopEdgeTiles()
    {
        List<GameObject> edgeTiles = new List<GameObject>();

        for (int i= mapWidth*(mapHeight-1); i<mapWidth*mapHeight; i++)
        {
            edgeTiles.Add(mapTiles[i]);
        }

        return edgeTiles;
    }

    private List<GameObject> getBottomeEdgeTiles()
    {
        List<GameObject> edgeTiles = new List<GameObject>();

        for (int i=0; i< mapWidth; i++)
        {
            edgeTiles.Add(mapTiles[i]);
        }

        return edgeTiles;
    }

    private void MoveDown()
    {
        pathTiles.Add(currentTile);
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex-mapWidth;
        currentTile = mapTiles[nextIndex];
    }

    private void MoveRight()
    {
        pathTiles.Add(currentTile);
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex+1;
        currentTile = mapTiles[nextIndex];
    }

    private void MoveLeft()
    {
        pathTiles.Add(currentTile);
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex-1;
        currentTile = mapTiles[nextIndex];
    }
    private void generateMap()
    {
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                GameObject newTile = Instantiate(MapTile);

                mapTiles.Add(newTile);

                newTile.transform.position = new Vector2(x, y);
            }
        }
        List<GameObject> topEdgeTiles = getTopEdgeTiles();
        List<GameObject> bottomEdgeTiles = getBottomeEdgeTiles();

        GameObject startTile;
        GameObject endTile;

        int rand1 = Random.Range(0, mapWidth);
        int rand2 = Random.Range(0, mapWidth);

        startTile = topEdgeTiles[rand1];
        endTile = bottomEdgeTiles[rand2];

        currentTile = startTile;

        MoveDown();

        int loopCount = 0;


        while (!reachedX)
        {
            loopCount++;
            if (loopCount > 100)
            {
                break;
            }
            if (currentTile.transform.position.x > endTile.transform.position.x)
            {
                MoveLeft();
            }
            else if (currentTile.transform.position.x > endTile.transform.position.x)
            {
                MoveRight();
            }
            else
            {
                reachedX = true;
            }
        }
        while (reachedY == false)
        {
            if (currentTile.transform.position.y > endTile.transform.position.y)
            {
                MoveDown();
            }
            else
            {
                reachedY = true;
            }
        }
        pathTiles.Add(endTile);
        foreach(GameObject obj in pathTiles)
        {
            obj.GetComponent<SpriteRenderer>().color = pathColor;
        }
    }
}
