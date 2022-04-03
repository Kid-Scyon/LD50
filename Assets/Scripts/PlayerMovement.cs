using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera playerCam;
    [SerializeField] CinemachineVirtualCamera buildCam;

    [SerializeField] float moveSpeed = 10f;
    bool isBuildMode = false;
    public bool IsBuildMode { get { return isBuildMode; } }

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Up
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        //Down
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }

        //Left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        //Right
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }


        //Build Mode Camera Switching
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isBuildMode)
            {
                isBuildMode = false;
                buildCam.enabled = false;
                playerCam.enabled = true;
            }

            else
            {
                isBuildMode = true;
                buildCam.enabled = true;
                playerCam.enabled = false;
            }
        }


        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            gm.curTowerSelect = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gm.curTowerSelect = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gm.curTowerSelect = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            gm.curTowerSelect = 0;
        }
    }

}
