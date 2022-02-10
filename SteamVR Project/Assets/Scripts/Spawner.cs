using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private BaseObject prefab;
    [SerializeField]
    private Transform point1, point2;

    public float minDistance = .5f, minAmount = 0, checkDelay = .1f;
    private float timer = 0;

    private List<BaseObject> inactiveObjects = new List<BaseObject>();
    private List<BaseObject> activeObjects = new List<BaseObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if ((timer > checkDelay || activeObjects.Count < minAmount) && IsSpaceAvailable())
        {
            GameObject obj;
            BaseObject baseObj;

            //Re-Use old objects
            if (inactiveObjects.Count > 0)
            {
                obj = inactiveObjects[0].gameObject;
                obj.SetActive(true);

                baseObj = obj.GetComponent<BaseObject>();

                inactiveObjects.Remove(baseObj);
                activeObjects.Add(baseObj);
            }
            else
            {
                obj = Instantiate(prefab.gameObject, transform);

                baseObj = obj.GetComponent<BaseObject>();

                baseObj.ResetEvent.AddListener(ChildObjReset);
                activeObjects.Add(baseObj);
            }

            //Set Position
            if (point1 != null && point2 != null)
            {
                Vector3 pos1 = point1.position;
                Vector3 pos2 = point2.position;

                float x = (pos2.x - pos1.x) * Random.value;
                float y = (pos2.y - pos1.y) * Random.value;
                float z = (pos2.z - pos1.z) * Random.value;
                Vector3 spawnPos = point1.position + new Vector3(x,y,z);
                obj.transform.position = spawnPos;
            }

            

            timer = 0;
        }
    }

    private bool IsSpaceAvailable()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<BaseObject>() != null)
            {
                Transform obj = transform.GetChild(i);
                if (obj.gameObject.activeSelf && (obj.position - transform.position).magnitude < minDistance) return false;
            } 
        }
        return true;
    }

    private void ChildObjReset(BaseObject obj)
    {
        if (activeObjects.Contains(obj)) activeObjects.Remove(obj);
        inactiveObjects.Add(obj);
    }
}
