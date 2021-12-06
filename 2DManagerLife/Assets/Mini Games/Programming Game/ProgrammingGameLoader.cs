using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

public class ProgrammingGameLoader : MonoBehaviour
{
    public int LevelIndex;
    public Tilemap MoveMap;
    private string EnvironmentMapFile;
    private string MoveMapFile;

    private void Awake()
    {
        MoveMapFile = $"MoveMapKey{LevelIndex}.json";
    }

    private void Update()
    {
        /*if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.L))
        {
            LoadTilemap(MoveMapFile);
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.K))
        {
            SaveTilemap(MoveMapFile);
        }*/
    }

    public void LoadTilemap(string fileName)
    {
        string json = File.ReadAllText(Path.Combine(Application.dataPath, "Mini Games", "Programming Game", "Level Data", fileName));
        LevelData levelData = JsonUtility.FromJson<LevelData>(json);

        MoveMap.ClearAllTiles();
        for (int i = 0; i < levelData.Tiles.Count; i++)
        {
            MoveMap.SetTile(levelData.Position[i], levelData.Tiles[i]);
        }
    }

    public void SaveTilemap(string fileName)
    {
        BoundsInt bounds = MoveMap.cellBounds;
        LevelData levelData = new LevelData();

        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                TileBase tile = MoveMap.GetTile(new Vector3Int(x, y, 0));

                if (tile != null)
                {
                    levelData.Tiles.Add(tile);
                    levelData.Position.Add(new Vector3Int(x, y, 0));
                }
            }
        }

        string json = JsonUtility.ToJson(levelData, true);
        File.WriteAllText(Path.Combine(Application.dataPath, "Mini Games", "Programming Game", "Level Data", fileName), json);
    }


    class LevelData
    {
        public List<TileBase> Tiles = new List<TileBase>();
        public List<Vector3Int> Position = new List<Vector3Int>();
    }
}
