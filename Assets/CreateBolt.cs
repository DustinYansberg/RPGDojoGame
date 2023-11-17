using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBolt : MonoBehaviour
{
    public GameObject boltPrefab;
    // Start is called before the first frame update
    
    public void CreateThebolt()
    {
        GameObject Bolt = Instantiate(boltPrefab, new Vector3(-3, -1, 0), Quaternion.identity);
    }
}
