using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TowerDefence
{
    public class Tower : MonoBehaviour
    {
        AudioManager audioManager;
        SphereCollider sphereCol;

        // Enemy Info
        public Transform target;
        TowerLaser laser;

        // Enemies within tower range
        private List<GameObject> _enemies = new List<GameObject>();
        private List<Enemy> _enemy = new List<Enemy>();
        
        [Header("Required Fields")]
        public TowerData tD;
        public Transform firePoint;
        public Transform rotatingObj;
        public GameObject bullet;
        public GameObject towerRange;

        [Header("Tower Attack Type")]
        public bool Catcher;
        public bool Laser;
        public bool poisonTower;
        public bool canAtkFlying;

        [Header("Audio")]
        public AudioSource shotSound;

        float lastfired;          // The value of Time.time at the last firing moment

        // for Catcher tower 
        void Start()
        {
            audioManager = AudioManager.instance;
            shotSound = GetComponent<AudioSource>();
            sphereCol = GetComponent<SphereCollider>();
            sphereCol.radius = tD.range;
            HideRangeUI();

            target = null;
            
            if (Laser)
            {
                laser = bullet.GetComponent<TowerLaser>();
                bullet.SetActive(false);
            }
            else { bullet.SetActive(true); }
        }

        #region Enemy Within Tower Range Functions

        private void OnTriggerEnter(Collider other)
        {
            if (canAtkFlying)
            {
                if (other.tag.Equals(StringData.enemyTag) || other.tag.Equals(StringData.flyingEnemyTag))
                {
                    Enemy e = other.gameObject.GetComponent<Enemy>();

                    if (e.isDead == false && e.isPoisoned == false)
                    {
                        _enemies.Add(other.gameObject);
                        _enemy.Add(e);
                    }
                }
            }
            else
            {
                if (other.tag.Equals(StringData.enemyTag))
                {
                    Enemy e = other.gameObject.GetComponent<Enemy>();

                    if (e.isDead == false && e.isPoisoned == false)
                    {
                        _enemies.Add(other.gameObject);
                        _enemy.Add(e);
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (canAtkFlying)
            {
                if (other.tag.Equals(StringData.enemyTag) || other.tag.Equals(StringData.flyingEnemyTag))
                {
                    RemoveEnemiesFromList(other);
                }
            }
            else
            {
                if (other.tag.Equals(StringData.enemyTag))
                {
                    RemoveEnemiesFromList(other);
                }
            }
        }

        private void RemoveEnemiesFromList(Collider other)
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                if (other.gameObject.Equals(_enemies[i]))
                {
                    _enemies.RemoveAt(i);
                    _enemy.RemoveAt(i);
                }
            }
        }
        
        #endregion

        void Update()
        {
            if (_enemies.Count > 0)
            {
                target = _enemies[0].transform;
                RemoveDeadOrPoisonedEnemies();
            }
            else { target = null; }

            if (target != null)
                RotateTowerTowardsTarget();

            TowerShooting();

            shotSound.volume = audioManager.soundBar.value;
        }
        
        private void TowerShooting()
        {
            if (target != null && Catcher == false && Laser == false)
            {
                FireTowerBullet();
            }

            if (target != null && Catcher == false && Laser)
            {
                FireTowerLaser();
            }
            else if (target == null && Catcher == false && Laser)
            {
                StopTowerLaser();
            }
        }

        private void StopTowerLaser()
        {
            bullet.SetActive(false);
            shotSound.Stop();
        }

        private void FireTowerLaser()
        {
            bullet.SetActive(true);
            shotSound.Play();

            if (laser != null)
            {
                laser.target = target;
                laser.tower = this;
            }
        }

        private void FireTowerBullet()
        {
            if (Time.time - lastfired > 1 / tD.fireRate)
            {
                lastfired = Time.time;

                GameObject b = Instantiate(bullet, firePoint.position, bullet.transform.rotation) as GameObject;
                TowerBullet _twrBullet = b.GetComponent<TowerBullet>();
                _twrBullet.isHit = false;
                shotSound.Play();

                if (_twrBullet != null)
                {
                    _twrBullet.target = target;
                    _twrBullet.tower = this;
                }
            }
        }

        private void RotateTowerTowardsTarget()
        {
            Vector3 dir = target.transform.position - rotatingObj.transform.position;
            dir.y = 0;
            Quaternion rot = Quaternion.LookRotation(dir);
            rotatingObj.transform.rotation = Quaternion.Slerp(rotatingObj.transform.rotation, rot, 10f * Time.deltaTime);
        }

        private void RemoveDeadOrPoisonedEnemies()
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                if (_enemy[i].isDead || _enemy[i].isPoisoned)
                {
                    _enemies.RemoveAt(i);
                    _enemy.RemoveAt(i);
                }
            }
        }

        public void ShowRangeUI()
        {
            towerRange.SetActive(true);
            towerRange.transform.localScale = Vector3.one * tD.range * 2;
        }

        public void HideRangeUI()
        {
            towerRange.SetActive(false);
        }
        
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(gameObject.transform.position, tD.range);
        }
    }
}
