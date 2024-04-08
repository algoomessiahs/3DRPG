using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;

namespace RPG.Controller
{
    public class PlayerController : MonoBehaviour
    {
        void Update()
        {
            if(GetComponent<Health>().IsDead()) return;

            if(MoveCombat()) return;
            if(Move()) return;
        }

        public bool Move()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);

            if (hasHit)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    GetComponent<Move>().StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        public bool MoveCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));

            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if(target == null) continue;

                if(!GetComponent<Fighter>().CanAttack(target.gameObject)) continue;

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }
    }
}
