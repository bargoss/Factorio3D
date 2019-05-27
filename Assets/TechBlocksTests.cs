using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechBlocksTests : MonoBehaviour
{
    public TechnicalGoInfo turretInfo;

    TechWorldMono techWorldMono;
    Conveyor spawner;
    // Start is called before the first frame update
    void Start()
    {
        techWorldMono = GetComponent<TechWorldMono>();

        //Invoke("TestStuff", 0.3f);
        Invoke("ConveyorTest", 0.1f);
    }
    bool a = true;
    private void FixedUpdate()
    {
        if(Time.time > 0.31f)
        {
            MeshItems();
            if (spawner.CanTake(1))
            {
                if (a)
                {
                    spawner.Take(1);
                    a = !a;
                }
                else
                {
                    spawner.Take(2);
                    a = !a;
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
    void ConveyorTest()
    {
        print("1,0,0 direction: " + new Vector3Int(1, 0, 0).ToDirection());

        {
            Block conveyorBlock = AddBlock(new Vector3(1.1f, 1.1f, 1.1f), 5, new Vector3Int(1, 0, 0));
            Conveyor conveyor = (Conveyor)(conveyorBlock.technicalBlock);
            conveyor.Take(1);
            spawner = conveyor;
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
            fabricator.SwitchRecipe(2,techWorldMono.itemsContainer);
        }
        {
            Block conveyorBlock = AddBlock(new Vector3(5.1f, 1.1f, 1.1f), 6, new Vector3Int(1, 0, 0));
        }
        {
            Block conveyorBlock = AddBlock(new Vector3(6.1f, 1.1f, 1.1f), 5, new Vector3Int(0, 1, 0));
        }
        {
            Vector3 position = new Vector3(6.1f, 2.1f, 1.1f);
            Block turretBlock = new Block();
            turretBlock.blockType = 255;
            techWorldMono.SetElement(position, Quaternion.identity, turretBlock, turretInfo);
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

        MeshWorld();
    }
    Block AddBlock(Vector3 position, byte blockType, Vector3 lookDirection)
    {
        Block block = new Block();
        block.blockType = blockType;
        techWorldMono.SetElement(position, block, Quaternion.LookRotation(lookDirection));
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
