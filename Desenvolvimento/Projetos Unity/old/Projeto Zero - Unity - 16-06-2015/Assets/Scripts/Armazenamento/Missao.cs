using UnityEngine;
using System.Collections;

public class Missao
{
	public int indice = 0;
	public string titulo = "Missão";
	public string info = "Informação";
	public char tipo = 'm';
	public long [] objetivos;

	bool completa = false;

	public Missao(
		int indice, string titulo, string info,
		char tipo, long [] objetivos)
	{
		this.indice = indice;
		this.titulo = titulo;
		this.info = info;
		this.tipo = tipo;
		this.objetivos = objetivos;
	}

	public void Imprimir()
	{
		Debug.Log ("Missão "+indice+": "+titulo+"; Info: "+info+
		           "; Tipo: "+tipo+"; Objetivos: "+objetivos+ 
		           "; Completa: "+completa);
	}
}

