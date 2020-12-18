using UnityEngine;
using System.Collections;

namespace TowerDefence
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyMovement : MonoBehaviour
    {
        private PlayerStats playerStats;
        private Enemy enemy;

        private Transform target;
        private int waypointIndex = 0;

        public int route;
        private Transform[] waypoints;

        bool stopMove;

        public float attackSpeed = 1;
        float timer;

        void Start()
        {
            playerStats = PlayerStats.instance;
            enemy = GetComponent<Enemy>();
            if (route.Equals(0))
            {
                waypoints = Waypoints.points;
            }
            else if (route.Equals(1))
            {
                waypoints = Waypoints.points01;
            }
            else if (route.Equals(2))
            {
                waypoints = Waypoints.points02;
            }
            else if (route.Equals(10))
            {
                waypoints = Waypoints.points03;
            }
            else if (route.Equals(11))
            {
                waypoints = Waypoints.points04;
            }
            target = waypoints[0];
            timer = attackSpeed;
        }

        void Update()
        {
            if (enemy.isDead)
                return;

            if (Vector3.Distance(target.position, transform.position) < 0.5f)
            {
                GetNextWaypoint();
            }

            if (stopMove == true)
                return;

            Vector3 dir = target.position - transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), enemy.rotSpeed * Time.deltaTime);

            transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

            if (GameItemManager.freezeEnemies)
                enemy.speed = 0;
            else if (enemy.slowDuration <= 0)
                enemy.speed = enemy.startSpeed;

            enemy.slowDuration -= Time.deltaTime;
        }

        private void GetNextWaypoint()
        {
            if (waypointIndex >= waypoints.Length - 1)
            {
                EndPath();
                return;
            }

            waypointIndex++;
            target = waypoints[waypointIndex];
        }

        private void EndPath()
        {
            stopMove = true;
            enemy.anim.SetBool(StringData.idle, true);
            enemy.speed = 0;

            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timer = attackSpeed;
                enemy.anim.SetTrigger(StringData.attack);
                playerStats.UpdateHealth(enemy.attackDamage);
            }
        }
    }
}
