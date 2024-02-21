using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour , IAction
    {
        [SerializeField] float weaponRange = 2f;  // player off set from the enemy
        [SerializeField] float timeBetweenAttacks = 1f; // Attack delay
        [SerializeField] float weaponDamage = 5f; // damage

        Health target; // enemy Health component
        Mover mover; // mover Component
        float timeSinceLastAttack = 0;

        private void Start()
        {
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime; // time since last attack

            // if target is not null move, Once in range cancel movement and attack
            if (target == null) return;
            if (target.IsDead()) return;

            // Move to target then attack
            if (!GetIsInRange())
            {
                // target not in range, move to target
                mover.MoveTo(target.transform.position);
            }
            else
            {
                //target in range, attack
                mover.Cancel(); // set target to null
                AttackBehaviour(); // Attack
            }

        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform.position); // look at target
            if (timeSinceLastAttack > timeBetweenAttacks) // attack delay
            {
                GetComponent<Animator>().SetBool("StopAttack", false);
                // Animation will trigger the Hit() function on impact
                GetComponent<Animator>().SetTrigger("Attack");
                timeSinceLastAttack = 0; // Reset attack delay timmer
            }
            
        }

        // Animation event
        // When punch impacts the target
        void Hit()
        {
            if (target == null) return;
            target.TakeDamage(weaponDamage);
        }

        // Return true if player is in range to attack
        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        // returns true if there is a target with Health componet and is not dead
        public bool CanAttack(CombatTarget combatTarget)
        {
            if (combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        // Called from PlayerController.cs
        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            target = null;
            GetComponent<Animator>().SetBool("StopAttack", true);
        }

    }
}
