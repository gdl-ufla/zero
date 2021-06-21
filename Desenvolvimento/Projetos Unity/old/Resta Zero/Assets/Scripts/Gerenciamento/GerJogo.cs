using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GerJogo : MonoBehaviour
{
	public GameObject botaoBase;

	public int quantidadeInicial = 3;

	public float distanciaPorcentagem = 0.42f;

	static List<GerBotao> objetos = new List<GerBotao>();
	static GameObject botaoBaseEstatico;
	static Transform transformEstatico;
	static float distanciaJuntar = 0;
	static float ladoBotao = 90;
	static float espacoEntreBotoes = 10;

	void Awake()
	{
		botaoBaseEstatico = botaoBase;
		transformEstatico = transform;

		for(int i = 0; i < quantidadeInicial; i++)
		{
			CriarBotao();
		}

		ladoBotao = botaoBase.GetComponent<RectTransform>().sizeDelta.x;
		espacoEntreBotoes = ladoBotao * 0.1f;
		distanciaJuntar = distanciaPorcentagem * ladoBotao;

		Debug.Log ("Distância para juntar objetos: "+distanciaJuntar);
	}

	public void AdicionarBotao()
	{
		CriarBotao();
	}

	public void AdicionarBotao(int valor)
	{
		GameObject botao = (GameObject) Instantiate(
			botaoBaseEstatico, Vector3.zero, Quaternion.identity);
		
		botao.transform.SetParent(transformEstatico, false);

		botao.GetComponent<GerBotao>().Mudar(valor);
		
		objetos.Add(botao.GetComponent<GerBotao>());
	}

	public void AdicionarBotaoMulti()
	{
		GameObject botao = (GameObject) Instantiate(
			botaoBaseEstatico, Vector3.zero, Quaternion.identity);
		
		botao.transform.SetParent(transformEstatico, false);
		
		botao.GetComponent<GerBotao>().tipo	= Tipos.Botao.Multiplicador;
		botao.GetComponent<GerBotao>().valor = 2;
		botao.GetComponent<GerBotao>().Alterar();
		
		objetos.Add(botao.GetComponent<GerBotao>());
	}

	static void CriarBotao()
	{
		GameObject botao = (GameObject) Instantiate(
			botaoBaseEstatico, Vector3.zero, Quaternion.identity);

		botao.transform.SetParent(transformEstatico, false);

		objetos.Add(botao.GetComponent<GerBotao>());
	}

	public static void SoltarObjeto(GerBotao ativo)
	{
		foreach(GerBotao obj in objetos)
		{
			if (obj != ativo)
			{
				float distancia = Vector3.Distance(
					obj.transform.position, ativo.transform.position);

				if (distancia <= distanciaJuntar)
				{
					JuntarObjetos(obj, ativo);
					break;
				}
				else
				{
					Debug.Log ("Distância: " + distancia);
				}
			}
		}
	}

	static void JuntarObjetos(GerBotao parado, GerBotao juntado)
	{
		int novoValor = 0;
		int pontos = 0;

		if (parado.tipo == Tipos.Botao.Multiplicador ||
		    juntado.tipo == Tipos.Botao.Multiplicador)
		{
			novoValor = parado.valor * juntado.valor;
		}
		else
		{
			novoValor = parado.valor + juntado.valor;
		}

		if (novoValor == 0)
		{
			pontos = Mathf.Abs(parado.valor);
			objetos.Remove(parado);
			parado.Zerou();
		}
		else
		{
			parado.Mudar(novoValor);
		}

		objetos.Remove(juntado);
		juntado.Destruir();

		//Cria funçao para mostrar pontos ganhos.
		Dados.pontosAtuais += pontos;

		Debug.Log ("Juntou objetos. Pontos: "+pontos);

		CriarBotao();
	}
}

