using UnityEngine;
using System.Collections;

public class MissaoCompleta
{
	public System.DateTime dataCompleto = System.DateTime.Now;
	public int indice = 0;

	public MissaoCompleta(int ind, System.DateTime data)
	{
		dataCompleto = data;
		indice = ind;
	}
}

