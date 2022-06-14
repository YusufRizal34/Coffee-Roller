using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kursi : MonoBehaviour, IInteractable
{
    public Pelanggan pelanggan;
    public bool isserve; // mengecek apakah pelayan sudah mengantarkan makanan
    public bool IsServe
    {
        get { return isserve; }
        set { isserve = value; }
    }

    public bool issit; // mengecek pelanggan di meja
    public bool IsSit 
    { 
        get { return issit; }
        set { issit = value; } 
    }

    public void InteraksiPelanggan()
    {
        /*IsSit = !IsSit;*/
        
        if(IsSit == true && IsServe == false)
        {
            Unit pelayan = FindObjectOfType<Unit>();
            pelayan.GetComponent<Unit>().RequestNomerKursi(this);
        }
        else if(IsSit == true && IsServe == true)
        {
            pelanggan.habisMakan();
        }

    }
    public void InteraksiPelayan(GameObject pelayan)
    {
        if (IsSit == true && IsServe == false)
        {
      /*      pelayan.GetComponent<Unit>().RequestNomerKursi(this);*/
            /*PathRequestManager.RequestPath(pelayan.transform.position, this.transform.position, pelayan.GetComponent<Unit>().OnPathFound);*/
        }
        else if(IsSit == true && IsServe == true)
        {
            pelayan.GetComponent<Unit>().Kembali();
        }
    }

}
