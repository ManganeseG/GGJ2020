using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemSpawn : MonoBehaviour
{
    private GameManager gm;
    public float xScaleMultiplier = 1;
    public float zScaleMultiplier = 1;

    private Vector3 randomPos;

    public GameObject Ball;

    public float CdBeforeNextSpawn;
    private float cdBeforeNextSpawn;


    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        cdBeforeNextSpawn = CdBeforeNextSpawn;     
    }
    
    void Update()
    {
        cdBeforeNextSpawn -= Time.deltaTime;
        if(cdBeforeNextSpawn <= 0f && gm.TimerValue > 0f)
        {
            randomPos.x = Random.Range(-transform.localScale.x * xScaleMultiplier, transform.localScale.x * xScaleMultiplier);
            randomPos.y = .25f;
            randomPos.z = Random.Range(-transform.localScale.z * zScaleMultiplier, transform.localScale.z * zScaleMultiplier);

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPos, out hit, 1f, NavMesh.AllAreas))
            {
                Instantiate(Ball, hit.position, transform.rotation);
            }
            else
            {
                NavMesh.FindClosestEdge(randomPos, out hit, NavMesh.AllAreas);
                Instantiate(Ball, hit.position, transform.rotation);
            }
            cdBeforeNextSpawn = CdBeforeNextSpawn;
        }

    }
}
