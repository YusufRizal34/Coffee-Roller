using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

public class Chest : MonoBehaviour
{
    public float msToWait = 5000.0f;
    private Text chestTimer;
    private Button chestButton;
    private ulong lastChestOpen;
    
    private void Start()
    {
        chestButton = GetComponent<Button>();
        lastChestOpen = ulong.Parse(PlayerPrefs.GetString("LastChestOpen"));
        chestTimer = GetComponentInChildren<Text>();

        if (!IsChestReady())
        {
            chestButton.interactable = false;
        }
    }

    private void Update()
    {
        if (!chestButton.IsInteractable())
        {
            if (IsChestReady())
            {
                chestButton.interactable = true;
                return;
            }
            //pengaturan waktu
            ulong diff = ((ulong)DateTime.Now.Ticks - lastChestOpen);
            ulong m = diff / TimeSpan.TicksPerMillisecond;
            float secondsLeft = (float)(msToWait - m) / 1000.0f;

            string r = "";
            //jam
            r += ((int)secondsLeft / 3600).ToString() + "h ";
            secondsLeft -= ((int)secondsLeft / 3600) * 3600;

            //menit
            r += ((int)secondsLeft / 60).ToString("00") + "m ";

            //detik
            r += (secondsLeft % 60).ToString("00") + "s";

            chestTimer.text = r;
        }
    }

    public void ChestClick()
    {
        lastChestOpen = (ulong)DateTime.Now.Ticks;
        PlayerPrefs.SetString("LastChestOpen", lastChestOpen.ToString());
        GameManager.Instance.addCoin(250);
        GameManager.Instance.updateCoin();
        chestButton.interactable = false;
    }

    private bool IsChestReady()
    {
        ulong diff = ((ulong)DateTime.Now.Ticks - lastChestOpen);
        ulong m = diff / TimeSpan.TicksPerMillisecond;
        float secondsLeft = (float)(msToWait - m) / 1000.0f;

        if (secondsLeft < 0)
        {
            chestTimer.text = "COLLECT";

            return true;
        }
        else
        {
            return false;
        }
    }
}
