using UnityEngine;
using System.Collections;

public class RealizacaoCompleta
{
	public System.DateTime dataCompleto = System.DateTime.Now;
	public int indice = 0;

	public RealizacaoCompleta(int ind, System.DateTime data)
	{
		dataCompleto = data;
		indice = ind;
	}
}

