using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ContentListControl : MonoBehaviour
{
    public ItemsContainer itemsContainer;
    public GameObject contentsParent;

    public GameObject contentPrefab;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetList()
    {
        ClearList();
        //PopulateList();
    }
    /*
    void PopulateList()
    {
        foreach(Item item in itemsContainer.items)
        {
            if(item != null && item.blockMesh != null)
            {
                CreateContent(item);
            }
        }
    }
    */
    public GameObject CreateContent()
    {
        GameObject content = Instantiate(contentPrefab,contentsParent.transform);
        //Text text = content.GetComponentInChildren<Text>();
        //text.text = item.itemName;
        //content.GetComponentInChildren<Button>().onClick.AddListener(delegate{ SomeMethodName(SomeObject); });
        return content;
    }
    void ClearList()
    {
        Transform[] contents = contentsParent.GetComponentsInChildren<Transform>();
        for(int i = 0; i < contents.Length; i++)
        {
            if(i > 0)
            {
                Destroy(contents[i].gameObject);
            }
        }
    }
}
