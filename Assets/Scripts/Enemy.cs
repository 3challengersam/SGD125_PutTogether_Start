using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    Transform target;
    NavMeshAgent agent;
    int currentNode;
    int previousNode;
    public enum EnemyState
    {
        patrol,
        chase
    };

    EnemyState enemystate = EnemyState.patrol;

    // Start is called before the first frame update
    void Start()
    {
       // player transform
        agent = GetComponent<NavMeshAgent>();
        currentNode = Random.Range(0, GameManager.gm.nodes.Length);
        previousNode = currentNode;

        
    }

    // Update is called once per frame
    void Update()
    {
       switch(enemystate)
        {
            case EnemyState.patrol: Patrol(); break;
            case EnemyState.chase: Chase(); break;
            default: break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "node")
        {
            currentNode = Random.Range(0, GameManager.gm.nodes.Length);
            while(currentNode == previousNode)
            {
                currentNode = Random.Range(0, GameManager.gm.nodes.Length);
            }
            previousNode = currentNode;
        }

        if(other.tag == "Player")
        {
            enemystate = EnemyState.chase;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            enemystate = EnemyState.patrol;
        }
    }

    void Patrol()
    {
        agent.destination = GameManager.gm.nodes[currentNode].position;
    }

    void Chase()
    {

        agent.destination = (GameManager.gm.player.transform.position);

    }
}
