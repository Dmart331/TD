using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore.HelloAR;

public class ArFunctions : MonoBehaviour
{

    public List<GameObject> instancedObjects = null;

    //Instancing of ArFunctions
    private static ArFunctions instance;
    public static ArFunctions Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ArFunctions>();
#if UNITY_EDITOR
            if (FindObjectsOfType<ArFunctions>().Length > 1)
            {
                Debug.LogError("There is more than one AR Functions in the scene");
            }
#endif
            }
            return instance;
        }
    }

    public void PutInList(GameObject go)
    {
        instancedObjects.Add(go);
    }

    public void DestroyObjects()
    {
       foreach(GameObject g in instancedObjects)
        {
            Destroy(g);
        }
        instancedObjects.Clear();
    }

}
