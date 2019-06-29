using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FloatEvent : UnityEvent<float> { }

public class BuildController : MonoBehaviour
{
    public Camera camera;
    public ItemsContainer itemsContainer;
    public TechWorldMono techWorldMono;
    public ContentListControl blocksList;

    int selectedBlockType;
    Plane gamePlane;

    public FloatEvent selectBlockTypeEvent = new FloatEvent();

    Quaternion[] rotations;
    int targetRotationIndex = 0;
    Quaternion TargetRotation
    {
        get
        {
            return rotations[targetRotationIndex];
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        rotations = new Quaternion[4];
        rotations[0] = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        rotations[1] = Quaternion.LookRotation(Vector3.left, Vector3.up);
        rotations[2] = Quaternion.LookRotation(Vector3.back, Vector3.up);
        rotations[3] = Quaternion.LookRotation(Vector3.right, Vector3.up);

        selectedBlockType = 5;
        gamePlane = new Plane(Vector3.up, Vector3.up);

        PopulateList();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Utility.GetMousePosInWorldSpace(gamePlane, camera);
        Vector3 mousePositionActual = mousePosition + Vector3.one * 0.5f;
        if (Input.GetMouseButton(0))
        {
            //print("put block: " + selectedBlockType + ", at: " + mousePositionActual);
            SetBlock(mousePositionActual, TargetRotation, selectedBlockType);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            targetRotationIndex++;
            targetRotationIndex = targetRotationIndex % 4;
        }

        MouseHoverStuff(mousePositionActual);

        CameraMovement();
    }

    void PopulateList()
    {
        blocksList.ResetList();
        for (int i = 0; i < itemsContainer.items.Length; i++)
        {
            Item item = itemsContainer.items[i];
            if (item != null && item.blockModel != 0)
            {
                int aasdas = i;
                GameObject content = blocksList.CreateContent();
                content.GetComponentInChildren<Button>().onClick.AddListener(delegate { SelectBlockType(aasdas); });
                Text text = content.GetComponentInChildren<Text>();
                text.text = item.itemName + ", " + i;
            }
        }
    }

    public void SelectBlockType(int blockType)
    {
        print("selecting: " + blockType);
        selectedBlockType = blockType;
    }
    void MouseHoverStuff(Vector3 mousePos)
    {
        Vector3 blockPos = mousePos.ToVector3Int();
        Item item = itemsContainer.items[selectedBlockType];
        Quaternion rotation = rotations[targetRotationIndex];
        Matrix4x4[] matrices = new Matrix4x4[1];
        matrices[0] = Matrix4x4.TRS(blockPos, rotation, Vector3.one * 0.5f);
        Mesh itemMesh = itemsContainer.models[item.blockModel].mesh;
        Material itemMaterial = itemsContainer.models[item.blockModel].material;
        Graphics.DrawMeshInstanced(itemMesh, 0, itemMaterial, matrices);
    }
    void CameraMovement()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        camera.transform.position += movement * Time.deltaTime * 60 * 0.4f;
    }

    public void SetBlock(Vector3 position, Quaternion rotation, int blockType)
    {
        Block block = GenerateBlock(blockType);
        techWorldMono.SetElement(position, block);
    }
    Block GenerateBlock(int blockType)
    {
        Block block = new Block();
        //block.blockType = (byte)blockType;
        InitializeTechBlock(ref block, rotations[targetRotationIndex], itemsContainer.items[blockType].techBlockType);

        return block;
    }
    void InitializeTechBlock(ref Block block, Quaternion rotation, Item.TechBlockType techBlockType)
    {
        switch (techBlockType)
        {
            case Item.TechBlockType.NonTechnical:
                break;
            case Item.TechBlockType.Pipe:
                block.technicalBlock = new Pipe(rotation);
                break;
            case Item.TechBlockType.Assembler:
                block.technicalBlock = new Assembler();
                break;
            case Item.TechBlockType.Splitter:
                block.technicalBlock = new Splitter(rotation);
                break;
            case Item.TechBlockType.PipeJunction:
                block.technicalBlock = new PipeJunction(rotation);
                break;
        }
    }
    /*
     */
}
