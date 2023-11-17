using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyBolt : MonoBehaviour
{
    
    public Transform boltTransform;
    public GameObject enemyGO;
    public GameObject playerGO;
    

    public void DestroyTheBolt()
    {
        GameObject bolt = GameObject.Find("bolt(Clone)");
        Destroy(bolt);
    }

    public void TransformBolt()
    {
        enemyGO = GameObject.Find("Enemy(Clone)");
        playerGO = GameObject.Find("Player(Clone)");

        GameObject bolt = GameObject.Find("bolt(Clone)");
        
        boltTransform = bolt.GetComponent<Transform>();
        float startX = playerGO.transform.position.x;
        float startY = playerGO.transform.position.y;
        float startZ = playerGO.transform.position.z;

        Vector3 startPosition = new Vector3(startX+1.5f, startY-1.16f, startZ);
        
        
        
        float endX = enemyGO.transform.position.x;
        float endY = enemyGO.transform.position.y;
        float endZ = enemyGO.transform.position.z;
        Vector3 endPosition = new Vector3(endX-.5f, endY-.5f, endZ-1);

        
        StartCoroutine(SmoothMove(startPosition, endPosition, .4f));
    }

        IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            boltTransform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }
}
