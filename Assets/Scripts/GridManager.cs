using UnityEngine;
using TMPro;

public class GridManager : MonoBehaviour
{
    public int gridSizeX = 10;
    public int gridSizeY = 10;
    public Transform tilePrefab;
    public float fontSizeMultiplier = 0.5f; // Fine-tune font size scaling (adjust as needed)

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
            Debug.Log($"Calculated tileSize: {tileSize}");
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
                tile.position = position;
                Debug.Log($"Tile {number} position before parenting: {tile.position}");
                tile.parent = transform;
                Debug.Log($"Tile {number} final world position: {tile.TransformPoint(Vector3.zero)}");

                Transform numberTextTransform = tile.Find("NumberText");
                if (numberTextTransform != null)
                {
                    Debug.Log($"NumberText for Tile {number} local position: {numberTextTransform.localPosition}");
                    Debug.Log($"NumberText for Tile {number} world position: {numberTextTransform.TransformPoint(Vector3.zero)}");
                    TMP_Text textComponent = numberTextTransform.GetComponent<TMP_Text>();
                    if (textComponent != null)
                    {
                        textComponent.text = number.ToString();
                        // Adjust font size to fit within the square
                        float baseFontSize = 10f;
                        textComponent.fontSize = baseFontSize * tileSize.x * fontSizeMultiplier;
                        // Ensure text alignment is centered
                        textComponent.alignment = TextAlignmentOptions.Center;
                        // Enable auto-sizing to fit within bounds
                        textComponent.enableAutoSizing = true;
                        textComponent.fontSizeMin = 5f;
                        textComponent.fontSizeMax = 20f;
                        // Log rendering order details
                        Renderer tileRenderer = tile.GetComponent<Renderer>();
                        if (tileRenderer != null)
                        {
                            Debug.Log($"Tile {number} SpriteRenderer sorting layer: {tileRenderer.sortingLayerName}, order: {tileRenderer.sortingOrder}");
                        }
                        Debug.Log($"NumberText for Tile {number} z-position: {numberTextTransform.position.z}");
                        Debug.Log($"NumberText for Tile {number} font size set to: {textComponent.fontSize}");
                        Debug.Log($"NumberText for Tile {number} bounds size: {textComponent.bounds.size}");
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