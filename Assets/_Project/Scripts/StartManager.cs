﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour {

    public GameObject Resource1Object;
    public Collider PlaneCol;

    public float SpawnTimeRessorce1;
    private bool _shouldSpawn;

    private float maxX;
    private float minX;
    private float maxZ;
    private float minZ;

    // Use this for initialization
    void Start () {


        maxX = PlaneCol.bounds.max.x;
        minX = PlaneCol.bounds.min.x;

        maxZ = PlaneCol.bounds.max.z;
        minZ = PlaneCol.bounds.min.z;

        StartCoroutine(SpawnResource());
    }


    public IEnumerator SpawnResource()
    {


        GameObject tempRess;
        Vector3 targetPos;

        _shouldSpawn = true;

        while (_shouldSpawn)
        {
            targetPos = GetRandomLocation_Ressource1();
            targetPos.y = 16;

            tempRess = Instantiate(Resource1Object, targetPos, Resource1Object.transform.rotation);


            tempRess.GetComponentInChildren<ParticleSystem>().Play();
            //tempRess.transform.DOMoveY(0.656f, 2f).SetEase(Ease.Linear);

            yield return new WaitForSeconds(SpawnTimeRessorce1);


        }
    }

    public Vector3 GetRandomLocation_Ressource1()
    {
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);

        return new Vector3(x, 0, z);
    }
}
