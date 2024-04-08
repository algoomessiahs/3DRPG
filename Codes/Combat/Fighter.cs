using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        public float range = 2f;

        public float timeBetweenAttacks = 2f;

        public float damage = 20f;

        Health target;

        float lastTimeSinceAttack = Mathf.Infinity;

        private void Update()
        {
            lastTimeSinceAttack += Time.deltaTime;

            if(target == null) return;
            if(target.IsDead()) return;

            if(!GetInRange())
            {
                GetComponent<Move>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Move>().Cancel();
                AttackAnimation();
            }
        }

        private void AttackAnimation()
        {
            transform.LookAt(target.transform);

            if(lastTimeSinceAttack > timeBetweenAttacks)
            {
                GetComponent<Animator>().SetTrigger("IsAttacking");
                lastTimeSinceAttack = 0;
            }
        }

        // It's an animation event!
        public void Hit()
        {
            if(target == null) return;

            target.TakeDamage(damage);
        }

        private bool GetInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < range;
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionPlan>().StartAction(this);
            GetComponent<Move>().Cancel();
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            target = null;
        }
        
        public bool CanAttack(GameObject combatTarget)
        {
            if(combatTarget == null) return false;

            Health testTarget = combatTarget.GetComponent<Health>();
            return testTarget != null && !testTarget.IsDead();
        }
    }
}
