using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    [SerializeField] public GameObject bgTilePrefab;
    [SerializeField] public Gem[] gems;
    [SerializeField] public Gem[,] allGems;
    public int width;
    public int height;
    public float gemSpeed;
    private MatchFinder _matchFind;

    private void Awake()
    {
        _matchFind = FindObjectOfType<MatchFinder>();
    }

    void Start()
    {
        allGems = new Gem[width, height];
        Setup();

    }

    private void Update()
    {
        _matchFind.FindAllMatches();
    }

    private void Setup()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 pos = new Vector2(x, y);
                GameObject bgTile = Instantiate(bgTilePrefab, pos, Quaternion.identity);
                bgTile.transform.parent = transform;
                bgTile.name = "BG Tile -" + x + ", " + y;
                int gemToUse = Random.Range(0, gems.Length);
                SpawnGem(new Vector2Int(x, y), gems[gemToUse]);
            }
        }
    }

    private void SpawnGem(Vector2Int pos, Gem gemToSpawn)
    {
        Gem gem = Instantiate(gemToSpawn, new Vector3(pos.x, pos.y, 0f), Quaternion.identity);
        gem.transform.parent = this.transform;
        gem.name = "Gem - " + pos.x + ", " + pos.y;
        allGems[pos.x, pos.y] = gem;
        
        gem.SetupGem(pos, this);
    }
}

