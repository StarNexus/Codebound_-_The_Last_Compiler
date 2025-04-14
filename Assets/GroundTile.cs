using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Ground Tile", menuName = "Tiles/Ground Tile")]
public class GroundTile : Tile
{
    [Header("Tileset Sprites")]
    public Sprite[] tilesetSprites; // Array of sprites from the tileset

    private void OnEnable()
    {
        // Automatically load all sprites from the tileset if the array is empty
        if (tilesetSprites == null || tilesetSprites.Length == 0)
        {
            string path = AssetDatabase.GetAssetPath(this); // Get the path of the GroundTile asset
            Object[] loadedSprites = AssetDatabase.LoadAllAssetsAtPath(path);

            tilesetSprites = System.Array.FindAll(loadedSprites, obj => obj is Sprite) as Sprite[];
        }
    }

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        base.RefreshTile(position, tilemap);

        // Automatically set the Tilemap GameObject's tag to "Ground"
        GameObject tileGameObject = tilemap.GetComponent<Tilemap>().gameObject;
        if (tileGameObject != null)
        {
            tileGameObject.tag = "Ground";
        }
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);

        // Randomly select a sprite from the tilesetSprites array
        if (tilesetSprites != null && tilesetSprites.Length > 0)
        {
            tileData.sprite = tilesetSprites[Random.Range(0, tilesetSprites.Length)];
        }

        // Set the collider type (optional)
        tileData.colliderType = Tile.ColliderType.Sprite;
    }
}
