using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class  Asiento
{
    public Transform posicion;
    public bool ocupado;
    public GameObject cliente;

    public Vector3 darPosicion()
    {
        return posicion.position;
    }
}
public class SpawnCliente : MonoBehaviour
{
    [SerializeField] private List<Asiento> posicionesEnLaBarra;
    [SerializeField] private List<Receta> listaRecetas;

    public GameObject clientePrefab;
    private float tiempoEntreSpawn;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= tiempoEntreSpawn)
        {
            crearNuevoCliente();
            timer = 0;
            tiempoEntreSpawn = UnityEngine.Random.Range(4,9);
        }
    }

    private void crearNuevoCliente()
    {
        if (todoOcupado())
        {
            Debug.Log("No fue posible acomodar a un nuevo cliente");
            return;
        }

        Asiento lugarLibre = posicionesEnLaBarra.FirstOrDefault(x => !x.ocupado);
        
        GameObject nuevoCliente = Instantiate(clientePrefab,lugarLibre.darPosicion(), Quaternion.identity);
        Receta coctelElegido = listaRecetas[UnityEngine.Random.Range(0, listaRecetas.Count)];
        nuevoCliente.GetComponent<Cliente>().inicializar(coctelElegido.nombre, lugarLibre.darPosicion());
        nuevoCliente.GetComponent<Cliente>().cuandoMeVoy += sacarDeLabarra;
        lugarLibre.cliente = nuevoCliente;
        lugarLibre.ocupado = true;

    }

    private void sacarDeLabarra(Vector3 pos)
    {
        foreach (Asiento silla in posicionesEnLaBarra) 
        {
            if (silla.darPosicion() == pos) 
            {
                silla.cliente = null;
                silla.ocupado = false;
                return;
            }
        }
    }

    private bool todoOcupado()
    {
        foreach (Asiento silla in posicionesEnLaBarra)
        {
            if (!silla.ocupado)
                return false;
        }
        return true;
    }
}
