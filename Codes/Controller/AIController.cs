using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

namespace RPG.Controller
{

    public class AIController : MonoBehaviour
    {
        public float chaseDistance = 7f;

        Fighter fighter;
        GameObject player;
        Health health;

        Vector3 guardPos;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
            guardPos = transform.position;
        }

        private void Update() 
        {
            if(health.IsDead()) return;

            if(InRange() && fighter.CanAttack(player))
            {
                fighter.Attack(player);
            }
            else
            {
                GetComponent<Move>().StartMoveAction(guardPos);
            }
        }

        private bool InRange()
        {
            float distanceTo = Vector3.Distance(player.transform.position, transform.position);
            
            return distanceTo < chaseDistance;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }

}