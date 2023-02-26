using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class p1Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletForceAmount = 10;
    [SerializeField] private GameObject fireObject;
    [SerializeField] private GameObject originalFireObject;

    private p1Movement _p1Movement;
    private Vector2 _p1Direction;

    [SerializeField] private Transform pUp;
    [SerializeField] private Transform pDown;
    [SerializeField] private Transform pLeft;
    [SerializeField] private Transform pRight;
    private void Start()
    {
        _p1Movement = FindObjectOfType<p1Movement>();
    }

    private void Update()
    {
        float xDirection = _p1Movement.GetPlayerDirection().x;
        float yDirection = _p1Movement.GetPlayerDirection().y;
        Debug.Log("X = " + xDirection + " " + "Y = " + yDirection);
        ShootBullet();
    }

    private void FixedUpdate()
    {

    }

    Vector2 GetShootingPosition()
    {
        return pDown.position;
    }

    Vector2 GetShootingDirection()
    {
        return Vector2.down;
    }

    void ShootBullet()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject _bulletOBJ = Instantiate(bullet, GetShootingPosition(), quaternion.identity) as GameObject;
            Rigidbody2D _bulletRB = _bulletOBJ.GetComponent<Rigidbody2D>();
            b1 b1Script = _bulletOBJ.GetComponent<b1>();
            _bulletRB.velocity = GetShootingDirection();
        }
    }
}
