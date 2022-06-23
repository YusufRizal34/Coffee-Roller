using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kursi : MonoBehaviour, IInteractable
{
    public Pelanggan pelanggan;
    public GameObject FloatingTextPrefab;
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
            ShowFloatingText();
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
/*            if (FloatingTextPrefab)
            {
                ShowFloatingText();
            }*/

            pelayan.GetComponent<Unit>().Kembali();
        }
    }

    void ShowFloatingText()
    {
        print("iki floating text");
        Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity);
        StartCoroutine("FloatingTextTenggelam");
    }

    IEnumerator FloatingTextTenggelam()
    {
        yield return new WaitForSeconds(5);
    }

}
