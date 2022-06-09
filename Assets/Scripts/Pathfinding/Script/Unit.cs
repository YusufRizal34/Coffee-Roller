using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{

	public Queue<Kursi> target = new Queue<Kursi>();
	public float speed = 5;
	Vector3[] path;
	int targetIndex;
	public bool isProsesUrut;
	Kursi currentTarget;
	public Transform tempatPelayan;
	public bool cekSeseorangRequest;

	void Start()
	{
/*		CariKursi();
        currentTarget = CompareDistance();
        PathRequestManager.RequestPath(this.transform.position, target[currentTarget].transform.position, OnPathFound);*/
    }

	void LateUpdate()
	{
        /*PathRequestManager.RequestPath(this.transform.position, target[currentTarget].transform.position, OnPathFound);*/
    }

	public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
	{
		if (pathSuccessful)
		{
			StopCoroutine("FollowPath");
			path = newPath;
			targetIndex = 0;
/*			float distanceFromTarget = Vector3.Distance(target.gameObject.transform.position, transform.position);

			if (distanceFromTarget > 0.5f)
			{
				
			}*/
			StartCoroutine("FollowPath");
		}
    }

	IEnumerator FollowPath()
	{
		Vector3 currentWaypoint = path[0];
		while (true)
		{
			if (transform.position == currentWaypoint)
			{
				targetIndex++;
				if (targetIndex >= path.Length)
				{
					targetIndex = 0;
					path = new Vector3[0];
					print("mbarang");
					yield break;
				}
				currentWaypoint = path[targetIndex];
            }

			transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
			yield return null;

		}
	}

	public void OnDrawGizmos()
	{
		if (path != null)
		{
			for (int i = targetIndex; i < path.Length; i++)
			{
				Gizmos.color = Color.black;
				Gizmos.DrawCube(path[i], Vector3.one);

				if (i == targetIndex)
				{
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else
				{
					Gizmos.DrawLine(path[i - 1], path[i]);
				}
			}
		}
	}

   /* private int CompareDistance()
    {
        int minDistance = -1;
        for (int i = 0; i < target.Count; i++)
        {
            if (target[i].IsSit == true)
            {
                target.Remove(target[i]);
                break;
            }

            if (minDistance < 0)
            {
                minDistance = i;
            }

            else if (Vector3.Distance(transform.position, target[i].transform.position) < Vector3.Distance(transform.position, target[minDistance].transform.position) && i != 0)
            {
                minDistance = i;
            }
           *//* print(minDistance);*//*
        }
        return minDistance;
    }*/

    private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Interactable")
		{
			var objects = other.gameObject.GetComponent<Kursi>();
            objects.InteraksiPelayan(gameObject);
            isProsesUrut = false;
			objects.IsServe = true;
			cekSeseorangRequest = false;
			TryProccessingNext();
        }
        else if (other.gameObject.tag == "TempatPelayan")
        {
            isProsesUrut = false;
            TryProccessingNext();
        }
    }

/*	void CariKursi()
    {
		Kursi[] kursi = Object.FindObjectsOfType<Kursi>();
		for(int i=0; i<kursi.Length; i++)
        {
			target.Add(kursi[i]);
        }
    }*/
	
	public void RequestNomerKursi(Kursi kursi)
    {
        cekSeseorangRequest = true;
        target.Enqueue(kursi);
		TryProccessingNext();
		/*        PathRequestManager.RequestPath(this.transform.position, kursi.transform.position, OnPathFound);*/
		print(target.Count);
	}

	public void TryProccessingNext()
    {
		 if (isProsesUrut == false && cekSeseorangRequest == true)
            {
			currentTarget = target.Peek();
			isProsesUrut = true;
            PathRequestManager.RequestPath(this.transform.position, currentTarget.transform.position, OnPathFound);
			target.Dequeue();
        }

        else if (isProsesUrut == false && cekSeseorangRequest == false)
        {
            isProsesUrut = true;
            PathRequestManager.RequestPath(this.transform.position, tempatPelayan.position, OnPathFound);
			isProsesUrut = false;
        }
    }
	public void Kembali()
    {
		print("aku g percoyo");
		cekSeseorangRequest = false;
		TryProccessingNext();
    }
}