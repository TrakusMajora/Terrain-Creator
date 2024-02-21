using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {

        private NavMeshAgent navMesh;


        // Start is called before the first frame update
        private void Start()
        {
            
            navMesh = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination)
        {
            // Tell action Scheduler we are walking now
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }


        // Move to 
        public void MoveTo(Vector3 destination)
        {
            navMesh.destination = destination;
            navMesh.isStopped = false;
        }

        // Stop moving
        public void Cancel()
        {
            navMesh.isStopped = true;

        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMesh.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            GetComponent<Animator>().SetFloat("Speed", localVelocity.z);
            GetComponent<Animator>().SetFloat("Direction", localVelocity.x);
        }

        // Animation event
        // When left foot hits the ground while moving
        void FootL()
        {

        }

        // Animation event
        // When Right foot hits the ground while moving
        void FootR()
        {

        }
    }
}
