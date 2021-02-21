using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPart : MonoBehaviour
{
    [Tooltip("In Seconds")] [SerializeField] float waitToDestroy = 2f;


    private void OnCollisionEnter(Collision collision)
    {
        if (FindObjectOfType<GameManager>().gameStarted)
        {
            StartCoroutine(DestroyRoadPart());
        }
    }

    IEnumerator DestroyRoadPart()
    {
        yield return new WaitForSeconds(waitToDestroy);
        Destroy(gameObject);
    }
}
