using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera playerCam;
    [SerializeField] CinemachineVirtualCamera buildCam;
    [SerializeField] ParticleSystem lazor;
    [SerializeField] List<Button> buildButtons;

    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float rotateSpeed = 10f;
    bool isBuildMode = false;
    public bool IsBuildMode { get { return isBuildMode; } }

    GameManager gm;
    GameObject tileList;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        tileList = gm.tileList;
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

        //Turn Left
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0, Space.World);
        }

        //Turn Right
        else if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0, Space.World);
        }


        //Build Mode Camera Switching
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(isBuildMode)
            {
                //Toggle the buttons and spheres for building
                ToggleBuildSpheres(false);
                toggleButtons(false);

                //Cleart he selection on leaving build mode
                gm.curTowerSelect = 0;
                isBuildMode = false;

                //Switch cams
                buildCam.enabled = false;
                playerCam.enabled = true;
            }

            else
            {
                //Toggle the buttons and spheres for building
                ToggleBuildSpheres(true);
                toggleButtons(true);

                isBuildMode = true;

                //Switch cams
                buildCam.enabled = true;
                playerCam.enabled = false;
            }
        }

        /*
        //Tower Select
        //TEMPORARY! Replace with UI buttons later
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
        */

        if(Input.GetMouseButtonDown(0) && !isBuildMode)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        lazor.Play();
    }

    private void ToggleBuildSpheres(bool enableSpheres)
    {
        foreach(Transform child in tileList.transform)
        {
            if(child.GetChild(0).GetComponent<MeshRenderer>() != null)
            {
                child.GetChild(0).GetComponent<MeshRenderer>().enabled = enableSpheres;
            }
        }
    }

    void toggleButtons(bool toggle)
    {
        foreach(Button b in buildButtons)
        {
            b.gameObject.SetActive(toggle);
        }
    }
}
