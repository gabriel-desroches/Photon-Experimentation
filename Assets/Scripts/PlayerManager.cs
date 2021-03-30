using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    [Tooltip("The Beams GameObject to control")]
    [SerializeField]
    private GameObject beams;

    [Tooltip("The current Health of our player")]
    public float Health = 1f;


    //True, when the user is firing
    bool IsFiring;

    void Awake()
    {
        if (beams == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> Beams Reference.", this);
        }
        else
        {
            beams.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            this.ProcessInputs();
            if (Health <= 0f)
            {
                GameManager.Instance.LeaveRoom();
            }
        }

        // trigger Beams active state
        if (beams != null && IsFiring != beams.activeInHierarchy)
        {
            beams.SetActive(IsFiring);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine)
        {
            return;
        }
        // We are only interested in Beamers
        // we should be using tags but for the sake of distribution, let's simply check by name.
        if (!other.name.Contains("Beam"))
        {
            return;
        }
        Health -= 0.1f;
    }

    void OnTriggerStay(Collider other)
    {
        // we dont' do anything if we are not the local player.
        if (!photonView.IsMine)
        {
            return;
        }
        // We are only interested in Beamers
        // we should be using tags but for the sake of distribution, let's simply check by name.
        if (!other.name.Contains("Beam"))
        {
            return;
        }
        // we slowly affect health when beam is constantly hitting us, so player has to move to prevent death.
        Health -= 0.1f * Time.deltaTime;
    }

    void ProcessInputs()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!IsFiring)
            {
                IsFiring = true;
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            if (IsFiring)
            {
                IsFiring = false;
            }
        }
    }

}
