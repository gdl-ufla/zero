using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GerMensagens : MonoBehaviour
{
	public GameObject painelMensagemBase;
	public float tempoMostrar = 1.5f;
	public float tempoDesvanecer = 1.5f;

	public Sprite [] imagensMensagens;

	static float proximoTempo = 0;
	static bool desvanecendo = false;
	static bool mostrando = false;

	static float tempoMostrarEstatico = 1;
	static float tempoDesvanecerEstatico = 1;
	static float alfaGeral = 1;
	//static float alfaPainel = 1;
	static float alfaTexto = 1;
	static float alfaImagem = 1;

	static GameObject painelMensagemEstatico;
	static Image imagemPainelBaseEstatico;
	static Text textoPainelMensagemEstatico;
	static Image imagemPainelMensagemEstatico;
	static Sprite [] imagensMensagensEstatico;

	static List<string> mensagens = new List<string>();
	static List<Sprite> imagens = new List<Sprite>();

	void Start()
	{
		painelMensagemEstatico = painelMensagemBase;

		painelMensagemEstatico.SetActive(true);

		imagensMensagensEstatico = imagensMensagens;

		/*
		imagemPainelBaseEstatico = painelMensagemEstatico
			.GetComponent<Image>();
		alfaPainel = imagemPainelBaseEstatico.color.a;
		*/

		textoPainelMensagemEstatico = painelMensagemEstatico
			.GetComponentInChildren<Text>();
		alfaTexto = textoPainelMensagemEstatico.color.a;

		imagemPainelMensagemEstatico = painelMensagemEstatico.
			transform.GetComponentInChildren<Image>();
		alfaImagem = imagemPainelMensagemEstatico.color.a;

		tempoMostrarEstatico = tempoMostrar;
		tempoDesvanecerEstatico = tempoDesvanecer;

		painelMensagemEstatico.SetActive(false);
	}

	void Update()
	{
		VerificarProxima();
	}

	public static void AdicionarMensagem(string mensagem)
	{
		AdicionarMensagem(mensagem, null);
	}
	
	public static void AdicionarMensagem(string mensagem, Sprite imagem)
	{
		mensagens.Add(mensagem);
		imagens.Add(imagem);
	}

	public static void AdicionarMensagem(
		Mensagens mensagem, 
		MensagensImagens imagem = MensagensImagens.mensagem,
		int missao = 0)
	{
		string texto = "Mensagem";
		if (mensagem == Mensagens.missao)
		{
			texto = Dados.missoes[missao].titulo;
		}
		else
		{
			texto = Dados.textosMensagens[((int) mensagem) - 1];
		}

		AdicionarMensagem(texto, 
			imagensMensagensEstatico[(int) imagem]);

		textoPainelMensagemEstatico.color = new Color(
			Dados.coresMensagens[(int) imagem].r,
			Dados.coresMensagens[(int) imagem].g,
			Dados.coresMensagens[(int) imagem].b,
			textoPainelMensagemEstatico.color.a);

		imagemPainelMensagemEstatico.color = new Color(
			Dados.coresMensagens[(int) imagem].r,
			Dados.coresMensagens[(int) imagem].g,
			Dados.coresMensagens[(int) imagem].b,
			imagemPainelMensagemEstatico.color.a);
	}

	static void VerificarProxima()
	{
		if (Time.time > proximoTempo)
		{
			if (mostrando)
			{
				mostrando = false;
				desvanecendo = true;
				proximoTempo = Time.time + tempoDesvanecerEstatico;
			}
			else if (desvanecendo)
			{
				desvanecendo = false;
				painelMensagemEstatico.SetActive(false);
			}
			else if (mensagens.Count > 0)
			{
				MostrarProxima();
			}
		}
		else if (desvanecendo)
		{
			alfaGeral = (proximoTempo - Time.time) /
				tempoDesvanecerEstatico;
			AlterarAlfa();
		}
	}

	static void AlterarAlfa(float a = -1)
	{
		if (a >= 0)
		{
			alfaGeral = a;
		}

		if (alfaGeral >= 0)
		{
			/*
			imagemPainelBaseEstatico.color = new Color(
				imagemPainelBaseEstatico.color.r,
				imagemPainelBaseEstatico.color.g,
				imagemPainelBaseEstatico.color.b,
				alfaGeral * alfaPainel);
			*/
			
			textoPainelMensagemEstatico.color = new Color(
				textoPainelMensagemEstatico.color.r,
				textoPainelMensagemEstatico.color.g,
				textoPainelMensagemEstatico.color.b,
				alfaGeral * alfaTexto);
			
			imagemPainelMensagemEstatico.color = new Color(
				imagemPainelMensagemEstatico.color.r,
				imagemPainelMensagemEstatico.color.g,
				imagemPainelMensagemEstatico.color.b,
				alfaGeral * alfaImagem);
		}
	}

	static void MostrarProxima()
	{
		textoPainelMensagemEstatico.text = mensagens[0];
		imagemPainelMensagemEstatico.sprite = imagens[0];
		mensagens.RemoveAt(0);
		imagens.RemoveAt(0);
		
		AlterarAlfa(1);
		painelMensagemEstatico.SetActive(true);
		mostrando = true;
		proximoTempo = Time.time + tempoMostrarEstatico;
	}
}

