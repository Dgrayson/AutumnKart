using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class KartItem : MonoBehaviour
{

    public float itemNum = 0;

    public Image itemImage; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (itemNum != 0)
                UseItem(); 
        }
    }

    public void SelectItem()
    {
        itemNum = Random.Range(1, 4);
        ToggleUI(true); 
    }

    public void UseItem()
    {
        ToggleUI(false); 


        itemNum = 0;
    }

    private void ToggleUI(bool value)
    {
        switch (itemNum)
        {
            case 1:
                itemImage.gameObject.transform.Find("Acorn").GetComponent<Image>().enabled = value;
                break;
            case 2:
                itemImage.gameObject.transform.Find("Pumpkin").GetComponent<Image>().enabled = value;
                break;
            case 3:
                itemImage.gameObject.transform.Find("Feather").GetComponent<Image>().enabled = value;
                break;
            default:

                break;

        }

    }
}
