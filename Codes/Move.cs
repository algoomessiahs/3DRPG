using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class Move : MonoBehaviour, IAction
    {
        void Update()
        {
            if(GetComponent<Health>().IsDead())
            {
                GetComponent<NavMeshAgent>().enabled = false;
            }
            
            UpdateAnimation();
        }

        public void MoveTo(Vector3 destination)
        {

            GetComponent<NavMeshAgent>().destination = destination;
            GetComponent<NavMeshAgent>().isStopped = false;
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionPlan>().StartAction(this);
            MoveTo(destination);
        }

        public void Cancel()
        {
            GetComponent<NavMeshAgent>().isStopped = true;
        }
        
        private void UpdateAnimation()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVeclocity = transform.InverseTransformDirection(velocity);

            float speed = localVeclocity.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
        }
    }
}