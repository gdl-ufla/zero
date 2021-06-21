using UnityEngine;
using System.Collections;

public class Missao
{
	public enum Tipo {
		Zerar, Bloco, Placar, Multiplicador
	}

	string titulo	 	= "Título";
	string descrição 	= "Descrição";
	string mensagem		= "Completo";
	public Tipo tipo	= Tipo.Zerar;
	int nivelAtual		= 0;
	int [] entrada;
	float [] saida;
	int niveis;

	// se for menor que zero, nao passou de nivel
	public float Verificar(int valorEntrada)
	{
		if (nivelAtual < niveis - 1)
		{
			if (tipo == Tipo.Zerar &&
			    valorEntrada == entrada[nivelAtual + 1])
			{
				SubirDeNivel();
				return saida[nivelAtual];
			}
			else if (valorEntrada >= entrada[nivelAtual + 1])
			{
				SubirDeNivel();
				return saida[nivelAtual];
			}
		}
		return -1;
	}

	void SubirDeNivel()
	{
		nivelAtual++;
		switch(tipo){
		case Tipo.Zerar:
			Dados.valorBotaoNormalMax = (int) saida[nivelAtual];
			break;
		case Tipo.Bloco:
			Dados.tempoCriarBlocos = saida[nivelAtual];
			break;
		case Tipo.Placar:
			Dados.chanceCriarNovoBloco = saida[nivelAtual] / 100f;
			break;
		case Tipo.Multiplicador:
			Dados.chanceMultiplicador = saida[nivelAtual] / 100f;
			break;
		}
	}

	public string Titulo()
	{
		return titulo;
	}

	public string Descricao(int nv = -1)
	{
		if (nv < 0)
		{
			if (nivelAtual == niveis - 1)
			{
				return Dados.textoMissaoCompleta;
			}
			return string.Format(
				descrição,
				entrada[nivelAtual + 1], 
				saida[nivelAtual + 1]);
		}

		if (nv < niveis)
		{
			return string.Format(
				descrição,
				entrada[nv], 
				saida[nv]);
		}

		return descrição;
	}

	public string Mensagem()
	{
		return string.Format(mensagem, saida[nivelAtual]);
	}

	public int Nivel()
	{
		return nivelAtual;
	}

	// Retorna -1 se nao tiver proximo, e -2 se der erro
	public int Requisito(int nv = -1)
	{
		if (nv < 0)
		{
			if (nivelAtual == niveis - 1)
			{
				return -1;
			}
			return entrada[nivelAtual + 1];
		}

		if (nv < niveis)
		{
			return entrada[nv];
		}

		return -2;
	}

	public float ProximoValor(int nv = -1)
	{
		if (nv < 0)
		{
			if (nivelAtual == niveis - 1)
			{
				return -1;
			}
			return saida[nivelAtual + 1];
		}
		
		if (nv < niveis)
		{
			return saida[nv];
		}
		
		return -2;
	}

	public float ValorAtual()
	{
		return saida[nivelAtual];
	}

	public string StringValorAtual()
	{
		string sa;
		switch(tipo){
		case Tipo.Bloco: 
			sa = saida[nivelAtual].ToString("0.0");
			sa = sa.Replace(".",",");
			break;
		default:
			sa = saida[nivelAtual].ToString("0");
			break;
		}
		return sa;
	}

	public Missao(string titulo, string descricao, string mensagem,
	              string tipo, int [] entrada, float [] saida)
	{
		this.titulo = titulo;
		this.descrição = descricao;
		this.mensagem = mensagem;
		this.entrada = entrada;
		this.saida = saida;
		this.tipo = PegarTipo(tipo);
		this.niveis = entrada.Length;
		this.nivelAtual = 0;

		//Imprimir();
	}

	static Tipo PegarTipo(string ti)
	{
		char t = ti.ToLower()[0];
		switch (t){
		case 'b': return Tipo.Bloco;
		case 'p': return Tipo.Placar;
		case 'm': return Tipo.Multiplicador;
		default:  return Tipo.Zerar;
		}
	}

	public void Imprimir()
	{
		string sa = "";
		sa += "Missao: "+titulo+"; "+ descrição + "; "+ mensagem + "; "+tipo+"; ";
		for(int i = 0; i < entrada.Length; i++)
		{
			sa += entrada[i] + " ";
		}
		sa += "; "; 
		for(int i = 0; i < saida.Length; i++)
		{
			sa += saida[i] + " ";
		}

		Utilidade.DebugMensagem (sa);
	}
}

