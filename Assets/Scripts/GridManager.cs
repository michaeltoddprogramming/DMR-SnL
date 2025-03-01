using UnityEngine;
using TMPro;

public class GridManager : MonoBehaviour
{
    public int gridSizeX = 10;
    public int gridSizeY = 10;
    public Transform tilePrefab;

    private Vector2 tileSize;

    void Start()
    {
        CalculateTileSize();
        GenerateGrid();
    }

    void CalculateTileSize()
    {
        Renderer tileRenderer = tilePrefab.GetComponent<Renderer>();
        if (tileRenderer != null)
        {
            tileSize = new Vector2(tileRenderer.bounds.size.x, tileRenderer.bounds.size.y);
        }
        else
        {
            Debug.LogError("Tile prefab does not have a Renderer component.");
        }
    }

    void GenerateGrid()
    {
        int tileCount = 0;
        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                tileCount++;
                int number = GetTileNumber(x, y);
                Vector3 position = new Vector3(x * tileSize.x, (gridSizeY - 1 - y) * tileSize.y, 0);
                Debug.Log($"Tile {number} at (x={x}, y={y}) should be at world position {position}");
                Transform tile = Instantiate(tilePrefab, position, Quaternion.identity);
                tile.position = position; // Explicitly set position after instantiation
                Debug.Log($"Tile {number} position before parenting: {tile.position}");
                tile.parent = transform;
                Debug.Log($"Tile {number} final world position: {tile.TransformPoint(Vector3.zero)}"); // Log world position

                Transform numberTextTransform = tile.Find("NumberText/NumberText");
                if (numberTextTransform != null)
                {
                    TMP_Text textComponent = numberTextTransform.GetComponent<TMP_Text>();
                    if (textComponent != null)
                    {
                        textComponent.text = number.ToString();
                        float baseFontSize = 10f;
                        textComponent.fontSize = baseFontSize * tileSize.x / tilePrefab.localScale.x;
                    }
                    else
                    {
                        Debug.LogError($"TMP_Text component not found on NumberText for tile {number} at position ({x}, {y}).");
                    }
                }
                else
                {
                    Debug.LogError($"NumberText child not found in tile {number} at position ({x}, {y}).");
                }
            }
        }
        Debug.Log($"Total tiles generated: {tileCount}");
    }

    int GetTileNumber(int x, int y)
    {
        int row = gridSizeY - 1 - y;
        int number;
        if (row % 2 == 0)
        {
            number = row * gridSizeX + x + 1;
        }
        else
        {
            number = row * gridSizeX + (gridSizeX - 1 - x) + 1;
        }
        return number;
    }
}