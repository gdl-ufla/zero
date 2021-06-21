using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Realizacao
{
	// Tipo
	// placar X, fundir +X -X, bloco X, naomultiplicar X,
	// varios X Y Z..., maiorque X, resposta X

	public enum Tipo {
		Fundir = 0, Placar = 1, BlocoNormal = 2, BlocoMaiorQue = 3,
		BlocoSemMultiplicar = 4, VariosBlocos = 5, Resposta = 6
	}

	public int indice = 0;
	public string titulo = "Missão";
	public string descricao = "Descrição";
	public Tipo tipo = Tipo.Fundir;
	public int [] objetivos;
	public int pontos = 0;


	public System.DateTime 	dataCompleto	= System.DateTime.Now;
	public bool				completa		= false;

	public bool Verificar(Tipo tipo, int [] valores)
	{
		if (completa)
		{
			return false;
		}

		if (this.tipo == tipo)
		{
			switch(tipo){
			case Tipo.Fundir:
				return VerificarFundir(valores);
			case Tipo.BlocoSemMultiplicar:
				return VerificarBlocoSemMultiplicar(valores);
			case Tipo.BlocoMaiorQue:
				return VerificarBlocoMaiorQue(valores);
			case Tipo.BlocoNormal:
				return VerificarBlocoNormal(valores);
			case Tipo.VariosBlocos:
				return VerificarVariosBlocos(valores);
			case Tipo.Resposta:
				return VerificarResposta(valores);
			default:
				return VerificarPlacar(valores);
			}
		}

		return false;
	}

	bool VerificarResposta(int [] valores)
	{
		if (valores.Length == 1 && valores[0] == 42)
		{
			dataCompleto = System.DateTime.Now;
			completa = true;
		}
		
		return completa;
	}

	bool VerificarVariosBlocos(int [] valores)
	{
		if (valores.Length != objetivos.Length)
		{
			return false;
		}

		List<int> listaValores = new List<int>();
		for(int i = 0; i < valores.Length; i++){
			listaValores.Add(valores[i]);
		}
		List<int> listaObjetivos = new List<int>();
		for(int i = 0; i < objetivos.Length; i++){
			listaObjetivos.Add(objetivos[i]);
		}

		while(listaObjetivos.Count > 0)
		{
			int objetivo = listaObjetivos[0];
			listaObjetivos.RemoveAt(0);
			if (listaValores.Remove(objetivo) == false)
			{
				return false;
			}
		}

		completa = true;
		
		return completa;
	}

	bool VerificarBlocoSemMultiplicar(int [] valores)
	{
		if (valores.Length >= 1 && valores[0] == objetivos[0])
		{
			completa = true;
		}
		
		return completa;
	}

	bool VerificarBlocoMaiorQue(int [] valores)
	{
		if (valores.Length >= 1 && valores[0] > objetivos[0])
		{
			completa = true;
		}
		
		return completa;
	}

	bool VerificarBlocoNormal(int [] valores)
	{
		if (valores.Length >= 1 && valores[0] == objetivos[0])
		{
			completa = true;
		}
		
		return completa;
	}

	bool VerificarPlacar(int [] valores)
	{
		if (valores.Length >= 1 && valores[0] >= objetivos[0])
		{
			completa = true;
		}
		
		return completa;
	}

	bool VerificarFundir(int [] valores)
	{
		if (valores.Length >= 2 && 
			((valores[0] == objetivos[0] && 
		      valores[1] == objetivos[1])   ||
		     (valores[1] == objetivos[0] &&
		 	  valores[0] == objetivos[1])))
		{
			completa = true;
		}

		return completa;
	}

	public Realizacao(
		int indice, string titulo, string descricao,
		string tipo, int [] objetivos, int pontos)
	{
		this.indice = indice;
		this.titulo = titulo;
		this.descricao = descricao;
		this.tipo = PegarTipo(tipo);
		this.objetivos = objetivos;
		this.pontos = pontos;

		//Imprimir();
	}

	Tipo PegarTipo(string t)
	{
		switch(t.ToLower()[0]){
		case 'f': return Tipo.Fundir;
		case 'b': return Tipo.BlocoNormal;
		case 'n': return Tipo.BlocoSemMultiplicar;
		case 'm': return Tipo.BlocoMaiorQue;
		case 'v': return Tipo.VariosBlocos;
		case 'r': return Tipo.Resposta;
		default:  return Tipo.Placar;
		}
	}

	public void Imprimir()
	{
		string mens = 
			"Realização "+indice+
			": "+titulo+
			"; Descrição: "+descricao+
			"; Tipo: "+tipo+
			"; Objetivos:";

		foreach(long obj in objetivos)
		{
			mens += " " + obj;
		}

		mens += "; Completa: "+completa;

		Utilidade.DebugMensagem(mens);
	}
}

