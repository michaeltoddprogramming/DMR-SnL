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
                Transform tile = Instantiate(tilePrefab, position, Quaternion.identity);
                tile.position = position;
                tile.parent = transform;

                Transform numberTextTransform = tile.Find("NumberText");
                if (numberTextTransform != null)
                {
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