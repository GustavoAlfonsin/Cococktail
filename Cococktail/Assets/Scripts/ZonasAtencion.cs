using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonasAtencion : MonoBehaviour
{
    private bool ocupado;

    public bool Ocupado
    {
        get { return ocupado; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cliente"))
        {
            ocupado = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cliente"))
        {
            ocupado = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = (ocupado)? Color.red : Color.green;

        Gizmos.DrawCube(transform.position, new Vector3(2, 2.5f, 2));
    }
}
