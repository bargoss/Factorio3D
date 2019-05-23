using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechBlocksTests : MonoBehaviour
{
    TechWorldMono techWorldMono;
    Conveyor spawner;
    // Start is called before the first frame update
    void Start()
    {
        techWorldMono = GetComponent<TechWorldMono>();

        //Invoke("TestStuff", 0.3f);
        Invoke("ConveyorTest", 0.1f);
    }
    private void Update()
    {
        if(Time.time > 0.31f)
        {
            MeshItems();
            if (spawner.CanTake(1))
            {
                spawner.Take(1);
            }
        }
    }
    /*
    void TestStuff()
    {
        //AddBlock(new Vector3(0.1f, 0.1f, 0.1f), 4);
        AddBlock(new Vector3(1.1f, 1.1f, 1.1f), 5,1);
        MeshWorld();
        //MeshWorld();
        print("here");
    }
    */
    void ConveyorTest()
    {
        print("1,0,0 direction: " + new Vector3Int(1, 0, 0).ToDirection());

        {
            Block conveyorBlock = AddBlock(new Vector3(1.1f, 1.1f, 1.1f), 5, new Vector3Int(1, 0, 0).ToDirection());
            Conveyor conveyor = (Conveyor)(conveyorBlock.technicalBlock);
            conveyor.Take(1);
            spawner = conveyor;
        }
        {
            Block conveyorBlock = AddBlock(new Vector3(2.1f, 1.1f, 1.1f), 5, new Vector3Int(1, 0, 0).ToDirection());
        }
        {
            Block conveyorBlock = AddBlock(new Vector3(3.1f, 1.1f, 1.1f), 5, new Vector3Int(1, 0, 0).ToDirection());
        }
        {
            Block fabricatorBlock = AddBlock(new Vector3(4.1f, 1.1f, 1.1f), 4, new Vector3Int(1, 0, 0).ToDirection());
            Fabricator fabricator = (Fabricator)(fabricatorBlock.technicalBlock);
            fabricator.SwitchRecipe(1,techWorldMono.itemsContainer);
        }
        {
            Block conveyorBlock = AddBlock(new Vector3(5.1f, 1.1f, 1.1f), 5, new Vector3Int(1, 0, 0).ToDirection());
        }
        {
            Block conveyorBlock = AddBlock(new Vector3(6.1f, 1.1f, 1.1f), 5, new Vector3Int(0, 1, 0).ToDirection());
        }
        {
            Block conveyorBlock = AddBlock(new Vector3(6.1f, 2.1f, 1.1f), 5, new Vector3Int(-1, 0, 0).ToDirection());
        }
        {
            Block conveyorBlock = AddBlock(new Vector3(5.1f, 2.1f, 1.1f), 5, new Vector3Int(-1, 0, 0).ToDirection());
        }
        //Block fabricatorBlock = AddBlock(new Vector3(2.1f, 1.1f, 1.1f), 4, new Vector3Int(1, 0, 0).ToDirection());
        //Fabricator fabricator = (Fabricator)fabricatorBlock.technicalBlock;
        //fabricator.SwitchRecipe(1,techWorldMono.itemsContainer);

        MeshWorld();
    }
    Block AddBlock(Vector3 position, byte blockType, byte lookDirection)
    {
        Block block = new Block();
        block.blockType = blockType;
        techWorldMono.SetBlock(position, block, lookDirection);
        return techWorldMono.GetBlock(position);
    }
    void MeshWorld()
    {
        techWorldMono.MeshAllChunks_Blocks();
    }
    void MeshItems()
    {
        techWorldMono.MeshAllChunks_Items();
    }
}
