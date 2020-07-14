using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBullet : BulletScr
{

    public float RangeDamage = 2;
    public float Scorch = 1;
    public float SlowDown = 2;
    private ParticleSystem ParSys;
    
    void Start()
    {
        transform.SetParent(GlobalData.GameTrans);
        SpeedBullet = 3;
        StartTime = Time.time;
        ParSys = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (Target != null)
            TempTarget = Target.transform.position;
        Vector3 diff = TempTarget - transform.position;
        diff.Normalize();
        transform.position += diff * (Time.time - StartTime) * SpeedBullet;
        if (Vector3.Magnitude(TempTarget - transform.position) < 0.3f)
        {
            transform.position = TempTarget;
            int BeginX = Mathf.CeilToInt((transform.position.x - RangeDamage < 0) ? 0 : transform.position.x - RangeDamage);
            int BeginY = Mathf.CeilToInt((transform.position.z - RangeDamage < 0) ? 0 : transform.position.z - RangeDamage);
            int EndX   = Mathf.CeilToInt((transform.position.x + RangeDamage > GlobalData.LevelSize) ? GlobalData.LevelSize : transform.position.x + RangeDamage);
            int EndY   = Mathf.CeilToInt((transform.position.z + RangeDamage > GlobalData.LevelSize) ? GlobalData.LevelSize : transform.position.z + RangeDamage);
            for (int i = BeginX; i < EndX; i++)
                for (int j = BeginY; j < EndY; j++)
                {
                    foreach (EnemyScr enemy in GlobalData.EnemyMatrix[j, i])
                        if (enemy != null)
                        {
                            enemy.TakeDamage(Damage * ((RangeDamage - Vector3.Magnitude(enemy.transform.position - transform.position)) / RangeDamage));
                            enemy.SetSlowDown(SlowDown);
                        }
                }

            Instantiate(Resources.Load("Object/GAME/Towers/FrostMage/PostEffectBull") as GameObject, new Vector3(transform.position.x, 0.03f, transform.position.z), Quaternion.Euler(90,0,0)).GetComponent<MageBullet>();
            ParSys.Stop();
            Destroy(this);
            Destroy(gameObject, 2f);
        }
        StartTime = Time.time;
    }
}
