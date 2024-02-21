using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeController : MonoBehaviour
{
    [SerializeField] Transform player;
    private NavMeshAgent navMesh;


    // Start is called before the first frame update
    private void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
        UpdateAnimator();
    }

    private void MoveToPlayer()
    {
        
        navMesh.SetDestination(player.position);

    }

    private void UpdateAnimator()
    {
        Vector3 velocity = navMesh.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("Speed", speed);

        GetComponent<Animator>().SetFloat("Direction", localVelocity.x);
    }
}
