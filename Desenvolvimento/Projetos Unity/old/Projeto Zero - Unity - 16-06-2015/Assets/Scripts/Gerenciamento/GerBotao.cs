using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Gerenciador do botão
public class GerBotao : MonoBehaviour 
{
	// Atributos públicos
	public Tipos.Botao	tipo 		= Tipos.Botao.Multiplicador;
	public int			valor 		= 0;
	public float 		tempoSumir	= 1f;
	public float 		duracaoBrilho = 0.25f;
	public GerRastro	rastro;

	[HideInInspector]
	public int posicaoGrade = 0;

	// Atributos privados
	Color 	cor 	= Constantes.corZero;
	Image	imagem 	= null;
	Text	texto	= null;
	float 	tempo	= 0;
	float 	alfa	= 1;
	bool	sumir 	= false;

	bool 	brilhar 	= false;
	bool	desbrilhar 	= false;
	float 	tempoLerp 	= 0;

	GerRastro rastrinho;
	Outline brilho;

	Transform paiRastro;

	// Inicialização, chamada quando o objeto é criado.
	void Awake()
	{
		//Inicializar();
	}

	public void Inicializar(
		Transform pai = null, int pos = 0, Transform paiRastro = null)
	{
		tipo 	= Utilidade.GerarTipoBotao();
		valor 	= Utilidade.PegarValor(tipo);
		cor 	= Utilidade.CorPorTipo(tipo);
		this.paiRastro = paiRastro;
		
		//Debug.Log ("Botão criado. Tipo: "+tipo+"; "+
		//           "Valor: "+valor+"; "+" Cor: "+cor);

		transform.SetParent(pai, false);
		posicaoGrade = pos;

		imagem = GetComponent<Image>();
		texto = GetComponentInChildren<Text>();
		brilho = GetComponent<Outline>();
		brilho.enabled = false;

		rastrinho = Instantiate<GerRastro>(rastro);
		//rastrinho.Inicializar(transform, cor, paiRastro);

		Alterar();
	}

	void Update()
	{
		if (brilhar)
		{
			imagem.color = Color.Lerp(
				cor, Constantes.corBrilho, tempoLerp);
			if (tempoLerp < 1)
			{
				tempoLerp += Time.deltaTime / duracaoBrilho;
			}
			else
			{
				brilhar = false;
				desbrilhar = true;
			}
		}
		else if (desbrilhar)
		{
			imagem.color = Color.Lerp(
				Constantes.corBrilho, cor, tempoLerp);
			if (tempoLerp < 1)
			{
				tempoLerp += Time.deltaTime / duracaoBrilho;
			}
			else
			{
				desbrilhar = false;
				brilho.enabled = false;
			}
		}

		if (sumir)
		{
			alfa = (tempo - Time.time) / tempoSumir;
			if (alfa < 0)
			{
				Destruir();
			}
			else
			{
				imagem.color = new Color(
					imagem.color.r,
					imagem.color.g,
					imagem.color.b,
					alfa);

				texto.color = new Color(
					texto.color.r,
					texto.color.g,
					texto.color.b,
					alfa);
			}
		}
	}

	public void Alterar()
	{
		cor 			= Utilidade.CorPorTipo(tipo);
		imagem.color 	= cor;
		texto.text 		= Utilidade.GerarTexto(tipo, valor);
		rastrinho.Inicializar(transform, cor, paiRastro);
	}

	public void Mudar(int novoValor, bool mul = false)
	{
		valor = novoValor;
		if (mul)
		{
			tipo = Tipos.Botao.Multiplicador;
		}
		else if (valor > 0)
		{
			tipo = Tipos.Botao.Positivo;
		}
		else
		{
			tipo = Tipos.Botao.Negativo;
		}

		Alterar();
	}

	public void Zerou()
	{
		tipo	= Tipos.Botao.Zero;
		valor 	= 0;
		Alterar();

		sumir = true;
		tempo = Time.time + tempoSumir;
	}

	public void Destruir()
	{
		rastrinho.Destruir();
		Destroy(gameObject);
	}

	public void Carregar()
	{
		//Debug.Log ("Começou a carregar");
		Transform pai = transform.parent;
		transform.SetParent(null);
		transform.SetParent(pai);

		transform.position = Input.mousePosition;
	}

	public void Mover()
	{
		transform.position = Input.mousePosition;

		// Usar este, caso o canvas esteja para a câmera
		/*
		Vector2 pos;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(
		myCanvas.transform as RectTransform, 
		Input.mousePosition, myCanvas.worldCamera, out pos);
		transform.position = myCanvas.transform.TransformPoint(pos);
		//*/
	}

	public void Soltar()
	{
		//Debug.Log ("Soltou");
		GerJogo.SoltarObjeto(GetComponent<GerBotao>());
	}

	public void Brilhar()
	{
		brilhar = true;
		desbrilhar = false;
		brilho.enabled = true;
		tempoLerp = 0;
		rastrinho.MudarCor(cor);
	}
}
