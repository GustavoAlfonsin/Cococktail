using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nuevo Coctel", menuName = "Cocteles")]
public class Receta : ScriptableObject
{
	public string nombre;
	public float precio;
}
