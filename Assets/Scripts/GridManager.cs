using UnityEngine;
using TMPro;

public class GridManager : MonoBehaviour
{
    public int gridSizeX = 10;
    public int gridSizeY = 10;
    public Transform tilePrefab;
    public float fontSizeMultiplier = 0.5f;
    public Color alternateColor1 = new Color(243f / 255f, 253f / 255f, 207f / 255f);
    public Color alternateColor2 = new Color(158f / 255f, 220f / 255f, 221f / 255f);

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
        for (int y = 0; y < gridSizeY; y++)
        {
            bool startWithAlternate = (gridSizeY - 1 - y) % 2 == 1; // Check if row is even (counting from the bottom)

            for (int x = 0; x < gridSizeX; x++)
            {
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
                        float baseFontSize = 10f;
                        textComponent.fontSize = baseFontSize * tileSize.x * fontSizeMultiplier;
                        textComponent.alignment = TextAlignmentOptions.Center;
                        textComponent.enableAutoSizing = true;
                        textComponent.fontSizeMin = 5f;
                        textComponent.fontSizeMax = 20f;
                        // Log rendering order details
                        Renderer tileRenderer = tile.GetComponent<Renderer>();
                    }
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
