using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    /// <summary>
    /// Color to Object map
    /// </summary>
    [System.Serializable]
    public class ColorMap
    {
        public string _TileName;
        public List<GameObject> _TileObjects = new List<GameObject>();
        public Color32 _ReferenceColor;
    }

    /// <summary>
    /// Class to create a grid based level with provided pixel color data.
    /// </summary>
    public class LevelCreator : MonoBehaviour
    {
        [SerializeField] private float m_TileOffset = default; //Tile offset of the grid system
        [SerializeField] private float m_TileSize = default; //Tile size in the grid
        [SerializeField] private int m_GridSize = default; // Grid size, defines the pixels read to genereate level.
        [SerializeField] private GameObject m_GridParent = default;
        [SerializeField] private List<ColorMap> m_ColorMaps = new List<ColorMap>();
        [SerializeField] private List<Texture2D> m_LevelTextures = new List<Texture2D>();

        public static LevelCreator pInstance { get; private set; }
        private bool mIsReady = false;
        private GameObject[,] mTiles;

        void Awake()
        {
            if (pInstance == null)
                pInstance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            GenerateLevel();
        }

        public void GenerateLevel()
        {
            InstantiateGrid(m_LevelTextures[0]);
        }

        void InstantiateGrid(Texture2D levelTex)
        {
            Texture2D tempTex = new Texture2D(levelTex.width, levelTex.width, levelTex.format, levelTex.mipmapCount, false);
            Graphics.CopyTexture(levelTex, tempTex);

            mTiles = new GameObject[m_GridSize, m_GridSize];
            for (int i = 0; i < m_GridSize; i++)
            {
                for (int j = 0; j < m_GridSize; j++)
                {
                    Color32 pixelColor = tempTex.GetPixel(j, i);
                    List<GameObject> objects = GetTileObject(pixelColor);
                    if (objects != null && objects.Count > 0)
                    { 
                        GameObject mCurTile = objects[Random.Range(0, objects.Count)];

                        if (mCurTile != null)
                        {
                            mTiles[i, j] = Instantiate(mCurTile);
                            mTiles[i, j].gameObject.name = mCurTile.name;
                            mTiles[i, j].transform.SetParent(m_GridParent.transform);
                            mTiles[i, j].transform.position = new Vector3(j * m_TileSize + j * m_TileOffset, 0f, i * m_TileSize + i * m_TileOffset);
                            mTiles[i, j].transform.position -= new Vector3((m_GridSize * m_TileSize) / 2, -mCurTile.transform.localScale.y / 2, (m_GridSize * m_TileSize) / 2); //Offset
                        }
                    }
                }
            }

            mIsReady = true;
        }


        public bool IsReady()
        {
            return mIsReady;
        }

        /// <summary>
        /// Returns the object for the respective color based on color map
        /// </summary>
        /// <param name="color">Color for which object data is needed</param>
        /// <returns>Object matching the color data</returns>
        public List<GameObject> GetTileObject(Color32 color)
        {
            ColorMap tileMap = m_ColorMaps.Find(x => (color.r == x._ReferenceColor.r && color.g == x._ReferenceColor.g && color.b == x._ReferenceColor.b));
            if (tileMap != null)
                return tileMap._TileObjects;
            return null;
        }

        private void OnDestroy()
        {
            pInstance = null;
        }
    }
}
