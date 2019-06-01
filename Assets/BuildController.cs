﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    public Camera camera;
    public ItemsContainer itemsContainer;
    public TechWorldMono techWorldMono;

    int selectedBlockType;
    Plane gamePlane;

    // Start is called before the first frame update
    void Start()
    {
        selectedBlockType = 5;
        gamePlane = new Plane(Vector3.up, Vector3.up);
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


    public void SelectBlock(int selectedBlockType)
    {
        this.selectedBlockType = selectedBlockType;
    }
}
