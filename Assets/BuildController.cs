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

    // Start is called before the first frame update
    void Start()
    {
        selectedBlockType = 5;
        gamePlane = new Plane(Vector3.up, Vector3.up);

        PopulateList();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Utility.GetMousePosInWorldSpace(gamePlane, camera);
            mousePosition += Vector3.one * 0.5f;
            print("put block: " + selectedBlockType + ", at: " + mousePosition);
            SetBlock(mousePosition, Quaternion.LookRotation(Vector3.forward, Vector3.up), selectedBlockType);
        }

        CameraMovement();
    }

    void PopulateList()
    {
        blocksList.ResetList();
        for (int i = 0; i < itemsContainer.items.Length; i++)
        {
            int aasdas = i;
            Item item = itemsContainer.items[i];
            if (item != null && item.blockMesh != null)
            {
                GameObject content = blocksList.CreateContent();
                content.GetComponentInChildren<Button>().onClick.AddListener(delegate { SelectBlockType(i); });
                Text text = content.GetComponentInChildren<Text>();
                text.text = item.itemName + ", " + i;
            }
        }
    }

    public void SelectBlockType(int blockType)
    {
        selectedBlockType = blockType;
    }

    void CameraMovement()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        camera.transform.position += movement * Time.deltaTime * 60 * 0.4f;
    }

    public void SetBlock(Vector3 position, Quaternion rotation, int blockType)
    {
        Block block = GenerateBlock(blockType);
        techWorldMono.SetElement(position, block, rotation);
    }
    Block GenerateBlock(int blockType)
    {
        Block block = new Block();
        block.blockType = (byte)blockType;
        return block;
    }
}
