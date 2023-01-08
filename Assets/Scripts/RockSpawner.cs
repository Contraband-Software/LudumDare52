using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;

namespace Architecture
{
    [
        RequireComponent(typeof(Grid)),
        DisallowMultipleComponent
    ]
    public class RockSpawner : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] Tilemap groundTilemap;
        [SerializeField] Tilemap rockTilemap;
        [SerializeField] Tilemap wheatTileMap;
        [SerializeField] Tile[] rockTiles;
        [SerializeField] GameObject rockGameObject;

        [Header("Settings")]
        [SerializeField, Range(0, 1)] float spawnChancePerTileRow = 0.96f;

        void Awake()
        {
            BoundsInt bounds = groundTilemap.cellBounds;
            Debug.Log(groundTilemap.origin);

            //for (int y = bounds.y; y < bounds.y + bounds.size.y; y++)
            //{
            //    for (int x = bounds.x; x < bounds.x + bounds.size.x; x++)
            //    {
            //        if (Random.value > 1 - spawnChancePerTileRow)
            //        {
            //            PlaceRock(rockTilemap, new Vector3Int(x, y));
            //            x++;
            //            y++;
            //        }
            //    }
            //}

            for (int y = groundTilemap.origin.y; y < groundTilemap.size.y; y++)
            {
                for (int x = groundTilemap.origin.x; x < groundTilemap.size.x; x++)
                {
                    if (Random.value > 1 - spawnChancePerTileRow)
                    {
                        PlaceRock(rockTilemap, new Vector3Int(x, y));
                        x++;
                        y++;
                    }
                }
            }
        }

        private void PlaceRock(Tilemap tilemap, Vector3Int position)
        {
            for (int i = 0; i < rockTiles.Length; i++) {
                Vector3Int pos = position + new Vector3Int(i % 2, -1 * Mathf.FloorToInt(i / 2.0f));
                tilemap.SetTile(pos, rockTiles[i]);
                wheatTileMap.SetTile(pos, null);
            }

            GameObject gObject = Instantiate(rockGameObject, rockTilemap.transform);
            gObject.transform.localPosition = Backend.Utilities.Mult_CWise((Vector3)position - new Vector3(0, 1, 0), tilemap.cellSize);
        }
    }
}