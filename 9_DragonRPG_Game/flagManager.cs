using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flagManager : MonoBehaviour
{
    [SerializeField] FieldManager fieldManager;
    [SerializeField] eventTriggerManager eTM;
    public bool isBillboard;
    public int billboardNum;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D()
    {
        /*
        fieldManager.saveStart();
        eTM.touchedSaveFlag();
        */
        //fieldManager.isSave = true;
        if (!isBillboard)
        {
            fieldManager.saveEnter();
        }
        else
        {
            //ŠÅ”Â‚Ì‰æ‘œ‚ð•\Ž¦‚·‚é
            fieldManager.billboardEnter(billboardNum);
        }
    }
    public void OnTriggerExit2D()
    {
        /*
        fieldManager.saveStart();
        eTM.touchedSaveFlag();
        */
        //fieldManager.isSave = false;
        if (!isBillboard)
        {
            fieldManager.saveExit();
        }
        else
        {
            //ŠÅ”Â‚Ì‰æ‘œ‚ð•\Ž¦‚·‚é
            fieldManager.billboardExit(billboardNum);
        }
    }
}
