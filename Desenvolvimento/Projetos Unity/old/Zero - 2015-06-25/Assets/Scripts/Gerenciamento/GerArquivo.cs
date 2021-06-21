using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GerArquivo
{
	public static List<Missao> CarregarMissoes()
	{
		TextAsset texto = Resources.Load<TextAsset>(
			Dados.arquivoMissoes);

		List<Missao> missoes = new List<Missao>();

		if (texto == null){
			return missoes;
		}

		string textoCru = texto.text;
		textoCru = textoCru.Replace("\t","");
		
		string [] divisor = {"\r\n","\n\r","\n","\r"};
		string [] linhas = texto.text.Split(
			divisor, System.StringSplitOptions.None);
		
		int qtdPorMissao = 6;
		int quantidade = linhas.Length / qtdPorMissao;

		Dados.textoMissaoCompleta = linhas[0];

		for (int i = 0; i < quantidade; i++)
		{
			if (string.IsNullOrEmpty(linhas[i])){
				continue;
			}

			int ind		 		= i * qtdPorMissao + 1;

			string titulo 		= linhas[ind];
			string descricao	= linhas[ind + 1];
			string mensagem		= linhas[ind + 2];
			string tipo			= linhas[ind + 3];
			string [] entradas	= linhas[ind + 4].Split(" "[0]);
			string [] saidas	= linhas[ind + 5].Split(" "[0]);

			int [] entr = new int[entradas.Length];
			for(int j = 0; j < entradas.Length; j++){
				entr[j] = int.Parse(entradas[j]);
			}

			float [] sai = new float[saidas.Length];
			for(int j = 0; j < saidas.Length; j++){
				sai[j] = float.Parse(saidas[j]);
			}

			Missao missao = new Missao(
					titulo, descricao, mensagem, tipo, entr, sai);

			missoes.Add(missao);
		}

		//Crie um bloco multiplicador de <b><color=#36c054ff>x{0}>/color></b> ou maior para aumentar a chance para <b><color=#ffff00ff>{1:0}%</color></b>

		return missoes;
	}

	public static List<Realizacao> CarregarRealizacoes()
	{

		TextAsset texto = Resources.Load<TextAsset>(
			Dados.arquivoMissoes);

		List<Realizacao> realizacoes = new List<Realizacao>();

		// Cancela o carregamento, caso o arquivo não seja
		// encontrado, ou de erros.
		if (texto == null){
			return realizacoes;
		}

		// Transforma os \r e \n em /, pois em alguns formatos
		// de arquivo o salto de linha é "\r\n", e em outros é
		// diferente.
		string textoCru = texto.text;
		textoCru = textoCru.Replace("\t","");

		string [] divisor = {"/","\r\n","\n\r","\n","\r"};
		string [] linhas = texto.text.Split(
			divisor, System.StringSplitOptions.None);

		int qtdPorMissao = 5;
		int quantidade = linhas.Length / qtdPorMissao;

		//Utilidade.DebugMensagem ("Quantidade: "+quantidade);

		for (int i = 0; i < quantidade; i++)
		{
			int ind		 		= i * qtdPorMissao;
			int indice 			= int.Parse(linhas[ind]);
			string titulo 		= linhas[ind + 1];
			string info 		= linhas[ind + 2];
			char tipo 			= linhas[ind + 3][0];
			string [] objvs		= linhas[ind + 4].Split(" "[0]);

			long [] objetivos = new long[objvs.Length];
			for(int j = 0; j < objvs.Length; j++)
			{
				objetivos[j] = long.Parse(objvs[j]);
			}

			Realizacao realizacao = new Realizacao(
				indice, titulo, info, tipo, objetivos);

			//realizacao.Imprimir();

			realizacoes.Add(realizacao);
		}

		return realizacoes;
	}
}
