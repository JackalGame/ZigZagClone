using System.Collections;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] GameObject roadPrefab;

    [SerializeField] Vector3 lastPos;
    [SerializeField] float offset = 0.71f;

    private int roadCount = 0;


    public void CreateNewRoadPart()
    {
        Vector3 spawnPos = Vector3.zero;
        float chance = Random.Range(0, 100);

        if(chance < 50) 
        {
            spawnPos = new Vector3(lastPos.x + offset, lastPos.y, lastPos.z + offset);
        }
        else
        {
            spawnPos = new Vector3(lastPos.x - offset, lastPos.y, lastPos.z + offset);
        }

        GameObject road = Instantiate(roadPrefab, spawnPos, Quaternion.Euler(0, 45, 0));
        lastPos = road.transform.position;

        roadCount++;
        if(roadCount % 5 == 0)
        {
            int crystalSpawnChance = Random.Range(1, 21);
            if(crystalSpawnChance < 19)
            {
                road.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                road.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }
}
