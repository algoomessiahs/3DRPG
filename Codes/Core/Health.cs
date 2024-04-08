using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        bool isDead = false;

        public float health = 70f;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            Debug.Log(health);

            if(health == 0)
            {
                if(isDead) {return;}
                isDead = true;
                GetComponent<Animator>().SetTrigger("IsDead");

                GetComponent<ActionPlan>().CancelCurrentAction();
            }
        }
    }
}