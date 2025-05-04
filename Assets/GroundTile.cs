using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Ground Tile", menuName = "Tiles/Ground Tile")]
public class GroundTile : Tile
{
    [Header("Tileset Sprites")]
    public Sprite[] tilesetSprites; // Array of sprites from the tileset

#if UNITY_EDITOR
    private void OnEnable()
    {
        // This code only runs in the editor
        if (tilesetSprites == null || tilesetSprites.Length == 0)
        {
            string path = UnityEditor.AssetDatabase.GetAssetPath(this);
            Object[] loadedSprites = UnityEditor.AssetDatabase.LoadAllAssetsAtPath(path);
            tilesetSprites = System.Array.FindAll(loadedSprites, obj => obj is Sprite) as Sprite[];
        }
    }
#endif

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

        // Set the collider type
        tileData.colliderType = Tile.ColliderType.Sprite;
    }
}
