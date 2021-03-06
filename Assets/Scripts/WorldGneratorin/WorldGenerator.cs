using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WorldGenerator : MonoBehaviour
{
    public static readonly float BlockSize = 3;

    public List<WorldBlock> Blocks { get; private set; } = new List<WorldBlock>();
    public HashSet<Vector3Int> OccupiedSpaces { get; private set; } = new HashSet<Vector3Int>();
    [SerializeField]
    private bool dbugOccupied = false;

    [SerializeField]
    private List<GameObject> BlockTemplates;
    [SerializeField]
    private GameObject StartBlock;
    [SerializeField]
    private GameObject EndBlock;
    [SerializeField]
    private GameObject WallPrefab;
    [SerializeField]
    private List<GameObject> EnemyPrefabs;
    [SerializeField]
    private List<GameObject> PickupPrefabs;

    public int MaxDepth = 20;
    public float EnemySpawnChancePerRoom = 0.2f;
    public float PickupSpawnChance = 0.1f;

    private bool isEndBlockPlaced = false;

    private float LevelMultiplier => (1.0f + (((float)PlayerResources.Level-1.0f) * 0.1f));


    private bool cont = false;

    private void Start()
    {
        while (!isEndBlockPlaced)
        {
            Clear();
            Init();
        }
    }

    private void Init()
    {
        var go = Instantiate(StartBlock);
        go.transform.rotation = Quaternion.identity;
        go.transform.position = Vector3Int.zero;
        var block = go.GetComponent<WorldBlock>();
        block.SetOrigin(Vector3Int.zero);
        Blocks.Add(block);
        AppendBlockToOccupiedSpaces(block);

        PopulateBlockOpenings(block, 1);

        //StartCoroutine(PopulateBlockOpeningsCr(block, 1));
    }
    private void Clear()
    {
        isEndBlockPlaced = false;
        foreach (var b in Blocks)
        {
            Destroy(b.gameObject);
        }
        Blocks = new List<WorldBlock>();
        OccupiedSpaces = new HashSet<Vector3Int>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            cont = true;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Init();
        }
    }

    private void PopulateBlockOpenings(WorldBlock block, int depth)
    {
        for (int i = 0; i < block.Openings.Length; ++i)
        {
            if (block.Neighbors[i] != null){
                continue;
            }
            if (depth > MaxDepth*LevelMultiplier)
            {
                if (!isEndBlockPlaced)
                {
                    var eb =  AddBlock(EndBlock, block, i);
                    if (eb != null)
                    {
                        isEndBlockPlaced = true;
                        PopulateBlockOpenings(eb, depth + 1);
                        continue;
                    }
                }
                AddBlock(WallPrefab, block, i);
                continue;
            }

            var newBlock = AddBlock(BlockTemplates[Random.Range(0, BlockTemplates.Count)], block, i);
            if (newBlock == null)
            {
                //if (!OccupiedSpaces.Contains(block.Openings[i]))
                {
                    AddBlock(WallPrefab, block, i);
                }
                continue;
            }
            // Spawn an enemy
            if (depth > 4 && Random.value < EnemySpawnChancePerRoom*LevelMultiplier)
            {
                Instantiate(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Count)], newBlock.EnemySpawnPoint.position, Quaternion.identity);
            }

            // Add pickups
            foreach(var pickupSpawnPoint in newBlock.PickupSpawnPoints)
            {
                if (Random.value < PickupSpawnChance - (PickupSpawnChance*(1-LevelMultiplier)))
                {
                    Instantiate(PickupPrefabs[Random.Range(0, PickupPrefabs.Count)], pickupSpawnPoint.position, pickupSpawnPoint.rotation);
                }
            }

            PopulateBlockOpenings(newBlock, depth + 1);
        }
    }

    private IEnumerator PopulateBlockOpeningsCr(WorldBlock block, int depth)
    {
        if (depth <= 5)
        {
            for (int i = 0; i < block.Openings.Length; ++i)
            {
                if (block.Neighbors[i] != null)
                {
                    continue;
                }
                yield return new WaitUntil(() => cont);
                cont = false;
                var newBlock = AddBlock(BlockTemplates[Random.Range(0, BlockTemplates.Count)], block, i);
                yield return StartCoroutine(PopulateBlockOpeningsCr(newBlock, depth + 1));
            }
        }
    }

    private WorldBlock AddBlock(GameObject pref, WorldBlock neighbor, int openingIndex)
    {
        var go = Instantiate(pref);
        go.transform.rotation = Quaternion.identity;
        var block = go.GetComponent<WorldBlock>();

        // Take random index
        int newIndex = UnityEngine.Random.Range(0, block.Openings.Length);

        var newDir = block.Directions[newIndex];
        var neighborDir = neighbor.Directions[openingIndex];

        //var rot = Quaternion.FromToRotation(newDir, -neighborDir);
        var angle = Vector3.SignedAngle(newDir, -neighborDir, Vector3.up);
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.up);

        block.Rotate(rot);
        block.SetOrigin(neighbor.BlockOrigin + neighbor.Openings[openingIndex] - neighbor.Directions[openingIndex] - block.Openings[newIndex]);

        if (block.GetOccupiedSpaces().Any(x => OccupiedSpaces.Contains(x))){
            Destroy(go);
            return null;
        }

        AppendBlockToOccupiedSpaces(block);
        neighbor.Neighbors[openingIndex] = block;
        block.Neighbors[newIndex] = neighbor;
        Blocks.Add(block);
        return block;
    }

    private void AppendBlockToOccupiedSpaces(WorldBlock block)
    {
        foreach(var v in block.GetOccupiedSpaces()){
            OccupiedSpaces.Add(v);
        }
    }

    private void OnDrawGizmos()
    {
        if (dbugOccupied)
        {
            Gizmos.color = Color.red;
            foreach(var space in OccupiedSpaces)
            {
                var sf = new Vector3(space.x, space.y, space.z) * BlockSize;
                Gizmos.DrawLine(sf, 
                                sf + Vector3.one * BlockSize);
                Gizmos.DrawLine(sf + new Vector3(1, 0, 0)*BlockSize, 
                                sf + (Vector3.one - new Vector3(1, 0, 0)) * BlockSize);
                Gizmos.DrawLine(sf + new Vector3(0, 1, 0)*BlockSize, 
                                sf + (Vector3.one - new Vector3(0, 1, 0)) * BlockSize);
                Gizmos.DrawLine(sf + new Vector3(0, 0, 1)*BlockSize, 
                                sf + (Vector3.one - new Vector3(0, 0, 1)) * BlockSize);
            }
        }
    }

}
