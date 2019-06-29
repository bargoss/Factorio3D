using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechBlocksTests : MonoBehaviour
{
    public TechnicalGoInfo turretInfo;

    TechWorldMono techWorldMono;
    List<Pipe> spawners;
    // Start is called before the first frame update
    void Start()
    {
        techWorldMono = GetComponent<TechWorldMono>();

        spawners = new List<Pipe>();
        //Invoke("TestStuff", 0.3f);
        Invoke("ConveyorTests", 0.1f);
        Invoke("MeshWorld", 0.15f);
    }
    bool a = true;
    private void FixedUpdate()
    {
        a = !a;
        if (Time.time > 1.31f)
        {
            //MeshItems();
            if (true)
            {
                Block block = techWorldMono.GetElement(Vector3.one * 1.5f + Vector3.up * 0);
                
                if (block.technicalBlock is Pipe)
                {
                    TechnicalBlock spawner = block.technicalBlock;
                    if (spawner.CanTake(1, spawner.ForwardDirection))
                    {
                        if (a)
                        {
                            spawner.Take(1, spawner.ForwardDirection);
                            //if (first) a = !a;
                        }
                        else
                        {
                            spawner.Take(2, spawner.ForwardDirection);
                            //if (first) a = !a;
                        }
                        a = !a;
                    }
                }
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
    void ConveyorTests()
    {
        //SplittingTest();
        //ConveyorTest(1.1f, 1.1f, true);
        return;

        for (float y = 1.1f; y < 5; y += 1)
        for(float z = 1.1f; z < 60; z += 1)
        {
            ConveyorTest(z,y,false);
        }
    }
    void SplittingTest()
    {
        {
            Block conveyorBlock = AddBlock(new Vector3(1.1f, 1.1f, 1.1f), 5, new Vector3Int(1, 0, 0));
            Pipe conveyor = (Pipe)(conveyorBlock.technicalBlock);
            conveyor.Take(1, Vector3Int.zero);
            spawners.Add(conveyor);
        }
        {
            Block conveyorBlock = AddBlock(new Vector3(2.1f, 1.1f, 1.1f), 5, new Vector3Int(1, 0, 0));
        }
        {
            Block conveyorBlock = AddBlock(new Vector3(3.1f, 1.1f, 1.1f), 5, new Vector3Int(1, 0, 0));
        }
        {
            Block fabricatorBlock = AddBlock(new Vector3(4.1f, 1.1f, 1.1f), 4, new Vector3Int(1, 0, 0));
            Assembler fabricator = (Assembler)(fabricatorBlock.technicalBlock);
            fabricator.SwitchRecipe(2, techWorldMono.itemsContainer);
        }
        {
            Block inserterBlock = AddBlock(new Vector3(5.1f, 1.1f, 1.1f), 6, new Vector3Int(1, 0, 0));
        }
        {
            Block conveyorBlock = AddBlock(new Vector3(6.1f, 1.1f, 1.1f), 5, new Vector3Int(0, 1, 0));
            Block conveyorBlock2 = AddBlock(new Vector3(6.1f, 2.1f, 1.1f), 5, new Vector3Int(0, 1, 0));
        }
        {
            Block conveyorBlock = AddBlock(new Vector3(7.1f, 1.1f, 1.1f), 6, new Vector3Int(1, 0, 0));
        }
    }
    void ConveyorTest(float z, float y, bool turret)
    {
        print("1,0,0 direction: " + new Vector3Int(1, 0, 0).ToDirection());

        {
            Block conveyorBlock = AddBlock(new Vector3(1.1f, y, z), 5, new Vector3Int(1, 0, 0));
            Pipe conveyor = (Pipe)(conveyorBlock.technicalBlock);
            conveyor.Take(1, Vector3Int.zero);
            spawners.Add(conveyor);
        }
        {
            Block conveyorBlock = AddBlock(new Vector3(2.1f, y, z), 5, new Vector3Int(1, 0, 0));
        }
        {
            Block conveyorBlock = AddBlock(new Vector3(3.1f,y, z), 5, new Vector3Int(1, 0, 0));
        }
        {
            Block fabricatorBlock = AddBlock(new Vector3(4.1f, y, z), 4, new Vector3Int(1, 0, 0));
            Assembler fabricator = (Assembler)(fabricatorBlock.technicalBlock);
            fabricator.SwitchRecipe(2,techWorldMono.itemsContainer);
        }
        {
            Block conveyorBlock = AddBlock(new Vector3(5.1f,y,z), 6, new Vector3Int(1, 0, 0));
        }
        {
            //Block conveyorBlock = AddBlock(new Vector3(6.1f,y, z), 5, new Vector3Int(0, 1, 0));
            Block splitterBlock = AddBlock(new Vector3(6.1f, y, z), 8, new Vector3Int(1, 0, 0), new Vector3Int(0, 0, 1));
        }
        {
            Block conveyorBlock = AddBlock(new Vector3(6.1f, 1f + y, z), 5, new Vector3Int(0, 1, 0));
        }
        {
            if (turret)
            {
                Vector3 position = new Vector3(6.1f, 2f + y, z);
                Block turretBlock = new Block();
                turretBlock.blockType = 255;
                //techWorldMono.SetElement(position, Quaternion.identity, turretBlock, turretInfo);
            }
        }
        {
            Block conveyorBlock = AddBlock(new Vector3(7.1f, y, z), 5, new Vector3Int(1, 0, 0));
        }
        {
            Block conveyorBlock = AddBlock(new Vector3(8.1f, y, z), 5, new Vector3Int(0, 1, 0));
        }
        {
            if (turret)
            {
                Vector3 position = new Vector3(8.1f, 1f + y, z);
                Block turretBlock = new Block();
                turretBlock.blockType = 255;
                //techWorldMono.SetElement(position, Quaternion.identity, turretBlock, turretInfo);
            }
        }
        /*
        {
            Block conveyorBlock = AddBlock(new Vector3(6.1f, 2.1f, 1.1f), 5, new Vector3Int(-1, 0, 0));
        }
        {
            Block conveyorBlock = AddBlock(new Vector3(5.1f, 2.1f, 1.1f), 5, new Vector3Int(-1, 0, 0));
        }
        {
            Vector3 position = new Vector3(4.1f, 2.1f, 1.1f);
            Block turretBlock = new Block();
            turretBlock.blockType = 255;
            techWorldMono.SetElement(position, turretBlock, Matrix4x4.TRS(position, Quaternion.identity,Vector3.one), turretInfo);
        }
        */
        //Block fabricatorBlock = AddBlock(new Vector3(2.1f, 1.1f, 1.1f), 4, new Vector3Int(1, 0, 0).ToDirection());
        //Fabricator fabricator = (Fabricator)fabricatorBlock.technicalBlock;
        //fabricator.SwitchRecipe(1,techWorldMono.itemsContainer);
    }
    Block AddBlock(Vector3 position, byte blockType, Vector3 lookDirection)
    {
        return AddBlock(position, blockType, lookDirection, Vector3.up);
    }
    Block AddBlock(Vector3 position, byte blockType, Vector3 lookDirection, Vector3 upDirection)
    {
        Block block = new Block();
        block.blockType = blockType;
        //techWorldMono.SetElement(position, block, Quaternion.LookRotation(lookDirection, upDirection));
        return techWorldMono.GetElement(position);
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
