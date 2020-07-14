using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScr : MonoBehaviour
{
    public EnemyScr Target      { get; set; }
    public byte     SpeedBullet { get; set; }
    public bool     StartFlag   { get; set; }
    public float    Damage      { get; set; }


    protected float StartTime;
    protected float PauseTime;
    public Vector3 TempTarget;
    

    void Start()
    {
        transform.SetParent(GlobalData.GameTrans);
        SpeedBullet = 8;
        StartTime = Time.time;
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
            Destroy(gameObject, 0.01f);
            if(Target != null)
                Target.TakeDamage(Damage);
        }
        StartTime = Time.time;
    }
}
