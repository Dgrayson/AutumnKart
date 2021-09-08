using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public int yRotDir, xRotDir;
    public float rotSpeed; 

    // Start is called before the first frame update
    void Start()
    {
        xRotDir = Random.Range(-2, 2); 
        yRotDir = Random.Range(-2, 2); 

        if(xRotDir == 0)
        {
            float num = Random.Range(-1, 1);

            if (num <= 0)
                xRotDir--;
            else
                xRotDir++; 
        }

        if (yRotDir == 0)
        {
            float num = Random.Range(-1, 1);

            if (num <= 0)
                yRotDir--;
            else
                yRotDir++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        RotateBox(); 
    }

    private void RotateBox()
    {
        float xAngle = xRotDir < 0 ? transform.rotation.x + (rotSpeed * -Time.deltaTime) : transform.rotation.x + (rotSpeed * Time.deltaTime);
        float yAngle = yRotDir < 0 ? transform.rotation.x + (rotSpeed * -Time.deltaTime) : transform.rotation.x + (rotSpeed * Time.deltaTime);

        transform.Rotate(new Vector3(transform.rotation.x, yAngle, transform.rotation.z)); 
    }

    private void OnTriggerEnter(Collider other)
    {
        KartItem itemComp = other.gameObject.GetComponent<KartItem>(); 

        if (itemComp != null && itemComp.itemNum == 0)
        {
            itemComp.SelectItem(); 
        }

        Destroy(gameObject); 
    }
}
