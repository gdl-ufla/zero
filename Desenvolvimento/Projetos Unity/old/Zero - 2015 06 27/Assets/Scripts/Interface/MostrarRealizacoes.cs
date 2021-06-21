using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MostrarRealizacoes : MonoBehaviour
{
	public float posicaoInicial = -60;
	public float tamanho = 100;
	public float distancia = 10;
	public float tamanhoPainel = 480;

	public RectTransform painel;

	public GameObject realizacao;

	string nomePontos = "txtPontos";
	string nomeTitulo = "txtTitulo";
	string nomeDescricao = "txtDescricao";
	string nomeImagem = "imgPontos";

	static Text [] textosDescricao;
	static Text [] textosTitulos;
	static Image [] imagensPontos;

	static bool pronto = false;

	public void Ativar()
	{
		float posAtual = posicaoInicial;
		float tamanhoTotal = 0;

		textosDescricao = new Text[Dados.realizacoes.Count];
		textosTitulos = new Text[Dados.realizacoes.Count];
		imagensPontos = new Image[Dados.realizacoes.Count];

		for(int i = 0; i < Dados.realizacoes.Count; i++)
		{
			GameObject reali = (GameObject) Instantiate(
				realizacao);
			reali.transform.SetParent(painel, false);
			reali.GetComponent<RectTransform>().localPosition =
				new Vector3(0, posAtual, 0);

			tamanhoTotal += tamanho + distancia;
			posAtual = posicaoInicial - tamanhoTotal;

			textosDescricao[i] = reali.transform.FindChild(nomeDescricao)
				.GetComponent<Text>();
			textosTitulos[i] = reali.transform.FindChild(nomeTitulo)
				.GetComponent<Text>();
			imagensPontos[i] = reali.transform.FindChild(nomeImagem)
				.GetComponent<Image>();

			reali.transform.FindChild(nomePontos)
				.GetComponent<Text>().text = "" + 
					Dados.realizacoes[i].pontos;
			textosTitulos[i].text = Dados.realizacoes[i].titulo;
			textosDescricao[i].text = Dados.realizacoes[i].descricao;

			if (Dados.realizacoes[i].completa)
			{
				textosTitulos[i].color = Constantes.corMultiplicador;
				textosDescricao[i].color = Constantes.corMultiplicador;
				imagensPontos[i].color = Constantes.corMultiplicador;
			}
		}

		painel.sizeDelta = new Vector2(
			painel.sizeDelta.x, tamanhoTotal - tamanhoPainel);

		pronto = true;
	}

	public static void VerificarCor()
	{
		if (pronto)
		{
			for(int i = 0; i < Dados.realizacoes.Count; i++)
			{
				if (Dados.realizacoes[i].completa)
				{
					textosTitulos[i].color =
						Constantes.corMultiplicador;
					textosDescricao[i].color =
						Constantes.corMultiplicador;
					imagensPontos[i].color =
						Constantes.corMultiplicador;
				}
			}
		}
	}
}

