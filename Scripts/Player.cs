using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject myLaser;
    [SerializeField] float fMoveSpeed = 8f;
    [SerializeField] float fPadding = 0.5f;
    [SerializeField] float m_fFireSpeed = 10f;
    float m_fPosXMin = 0;
    float m_fPosYMin = 0;
    float m_fPosXMax = 0;
    float m_fPosYMax = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }
   

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * fMoveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * fMoveSpeed;
        var newPosX = Mathf.Clamp(transform.position.x + deltaX, m_fPosXMin, m_fPosXMax);
        var newPosY = Mathf.Clamp(transform.position.y + deltaY, m_fPosYMin, m_fPosYMax);
        transform.position = new Vector2(newPosX, newPosY);
    }

    private void Fire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            GameObject laser = Instantiate(
                myLaser,
                transform.position,
                Quaternion.identity) as GameObject;    // Quaternion.identity means no rotation
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, m_fFireSpeed);
        }
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        m_fPosXMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + fPadding;
        m_fPosXMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - fPadding;
        m_fPosYMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + fPadding;
        m_fPosYMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - fPadding;
    }

}
