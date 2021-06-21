using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Armazenador
{
	static string divisor = "|";

	/* Estrutura do arquivo
	 * tempoTotalDeJogo|pontos|quantidadeDeObjetosNoCenario|
	 * valorBloco0|tipoBloco0|posicaoGrade0|
	 * valorBloco1|tipoBloco1|posicaoGrade1...<qtdvariavel>...|
	 * quantidadeDeMissoesCompletas|indiceMissao0|dataMissao0|
	 * indiceMissao1|DataMissao11...<qtdvariavel>...
	 */
	public static void SalvarDados()
	{
		string dados = CriarStringSalvar();
		//Debug.Log ("Dados: "+dados);
		PlayerPrefs.SetString(Dados.nomeArquivo, dados);
	}

	public static void CarregarDados()
	{
		string carregar = CarregarStringDeArquivo();
		if (string.IsNullOrEmpty(carregar))
		{
			return;
		}

		//Debug.Log ("Dados Carregados: "+carregar);

		string [] dados = carregar.Split(divisor[0]);

		Dados.tempoTotalDeJogo	= ulong.Parse(dados[0]);
		Dados.pontosAtuais		= int.Parse(dados[1]);

		int objts = int.Parse(dados[2]) * 3;

		int indiceAtual = 3;
		objts += indiceAtual;

		while (indiceAtual < objts)
		{
			int valor = int.Parse(dados[indiceAtual]);
			int tipo = int.Parse(dados[indiceAtual + 1]);
			int pos = int.Parse(dados[indiceAtual + 2]);

			//Debug.Log ("Bloco: "+valor+", "+tipo+", "+pos);

			GerJogo.AdicionarNaGrade(tipo, valor, pos);
			indiceAtual += 3;
		}

		objts = int.Parse(dados[indiceAtual]) * 2;
		objts += indiceAtual;

		List<MissaoCompleta> missoesCompletas =
			new List<MissaoCompleta>();
		while (indiceAtual < objts)
		{
			int ind = int.Parse(dados[indiceAtual]);
			long data = long.Parse(dados[indiceAtual + 1]);


			missoesCompletas.Add(new MissaoCompleta(
				ind, System.DateTime.FromFileTime(data)));

			indiceAtual += 2;
		}

		GerJogo.missoesCompletas = missoesCompletas;
	}

	static string CarregarStringDeArquivo()
	{
		return PlayerPrefs.GetString(Dados.nomeArquivo);
	}

	static string CriarStringSalvar()
	{
		string dados = "" + Dados.tempoTotalDeJogo;
		
		dados += divisor + Dados.pontosAtuais;
		
		dados += divisor + GerJogo.objetos.Count;
		foreach(GerBotao bloco in GerJogo.objetos)
		{
			dados += divisor + bloco.valor + divisor + 
				((int)bloco.tipo) + divisor + bloco.posicaoGrade;
		}
		
		dados += divisor + GerJogo.missoesCompletas.Count;
		foreach(MissaoCompleta m in GerJogo.missoesCompletas)
		{
			dados += divisor + m.indice + divisor +
				m.dataCompleto.ToFileTime().ToString();
		}

		return dados;
	}
}

