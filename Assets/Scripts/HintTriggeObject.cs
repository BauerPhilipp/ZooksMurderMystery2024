using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintTriggeObject : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamHint;
    [SerializeField] BoxCollider hintCollider;

    PlayerControlls playerControlls;
    // Start is called before the first frame update
    void Start()
    {
        playerControlls = FindObjectOfType<PlayerController>().PlayerControlls;
        virtualCamHint.Priority = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        hintCollider.enabled = false;
        StartCoroutine(CamZoom());
    }


    private IEnumerator CamZoom()
    {
        playerControlls.Disable();
        virtualCamHint.Priority = 100;
        yield return new WaitForSeconds(3f);
        virtualCamHint.Priority = 0;
        playerControlls.Enable();
    }



}
