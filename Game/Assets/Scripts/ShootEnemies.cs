﻿/*
 * Copyright (c) 2017 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ShootEnemies : MonoBehaviour
{

    public List<GameObject> enemiesInRange;

    public DateTimeOffset? timestamp;
    private float lastShotTime;
    private MonsterData monsterData;
    private AblyManagerBehavior ablyManager;
    private ITargetingStrategy targetingStrategy;
    private GameManagerBehavior gameManager;

    // Use this for initialization
    void Start()
    {
        enemiesInRange = new List<GameObject>();
        lastShotTime = Time.time;
        monsterData = gameObject.GetComponentInChildren<MonsterData>();
        ablyManager = gameObject.GetComponentInChildren<AblyManagerBehavior>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
        SetTargetingStrategyByWave(gameManager.Wave);
    }

    void Update()
    {
        if (timestamp.HasValue)
        {
            DateTimeOffset? startTime = ablyManager.startTimeAbly;
            DateTimeOffset? msgTime = timestamp.GetValueOrDefault();
            TimeSpan? diffTime = msgTime - startTime;
            int ticksSince = ablyManager.ticksSinceStart;
            float timeFromTicks = ticksSince * (1000 * Time.fixedDeltaTime);
            if (timeFromTicks >= diffTime.Value.TotalMilliseconds + 1000)
            {
                timestamp = null;
            }
            else
            {
                return;
            }
        }

        GameObject target = targetingStrategy.SelectTarget(enemiesInRange);

        if (target != null)
        {
            if (Time.time - lastShotTime > monsterData.CurrentLevel.fireRate)
            {
                Shoot(target.GetComponent<Collider2D>());
                lastShotTime = Time.time;
            }

            Vector3 direction = gameObject.transform.position - target.transform.position;
            gameObject.transform.rotation = Quaternion.AngleAxis(
                Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg,
                Vector3.forward);
        }
    }

    private void SetTargetingStrategyByWave(int waveNumber)
    {
        if (waveNumber >= 3)
        {
            targetingStrategy = new HighestHealthTargetingStrategy();
        }
        else if (waveNumber >= 2)
        {
            targetingStrategy = new NearestEnemyTargetingStrategy(transform);
        }
        else if (waveNumber >= 1)
        {
            targetingStrategy = new LowestHealthTargetingStrategy();
        }
        else
        {
            targetingStrategy = new ClosestToGoalTargetingStrategy();
        }
    }



    private void OnEnemyDestroy(GameObject enemy)
    {
        enemiesInRange.Remove(enemy);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
            EnemyDestructionDelegate del =
                other.gameObject.GetComponent<EnemyDestructionDelegate>();
            del.enemyDelegate += OnEnemyDestroy;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
            EnemyDestructionDelegate del =
                other.gameObject.GetComponent<EnemyDestructionDelegate>();
            del.enemyDelegate -= OnEnemyDestroy;
        }
    }

    private void Shoot(Collider2D target)
    {
        GameObject bulletPrefab = monsterData.CurrentLevel.bullet;
        // 1 
        Vector3 startPosition = gameObject.transform.position;
        Vector3 targetPosition = target.transform.position;
        startPosition.z = bulletPrefab.transform.position.z;
        targetPosition.z = bulletPrefab.transform.position.z;

        // 2 
        GameObject newBullet = (GameObject) Instantiate(bulletPrefab);
        newBullet.transform.position = startPosition;
        BulletBehavior bulletComp = newBullet.GetComponent<BulletBehavior>();
        bulletComp.target = target.gameObject;
        bulletComp.startPosition = startPosition;
        bulletComp.targetPosition = targetPosition;

        // 3 
        Animator animator =
            monsterData.CurrentLevel.visualization.GetComponent<Animator>();
        animator.SetTrigger("fireShot");
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioSource.clip);
    }

}
