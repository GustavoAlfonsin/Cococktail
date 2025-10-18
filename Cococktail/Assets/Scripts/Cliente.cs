using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cliente : MonoBehaviour
{
    private string _coctel;
    public string Coctel
    {
        get { return _coctel; }
    }
    public Vector3 posicion;
    public float paciencia;
    private float timer = 0;

    public Action<Vector3> cuandoMeVoy;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= paciencia)
        {
            Debug.Log("ME VOY");
            cuandoMeVoy?.Invoke(posicion);
            Destroy(gameObject, 2f);
        }
    }

    public void inicializar(string coctel, Vector3 pos)
    {
        timer = 0;
        _coctel = coctel;
        posicion = pos;
    }
}
