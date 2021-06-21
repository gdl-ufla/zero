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

		// Cancela o carregamento, caso o arquivo não seja
		// encontrado, ou de erros.
		if (texto == null){
			return missoes;
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

		//Debug.Log ("Quantidade: "+quantidade);

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

			Missao missao = new Missao(
				indice, titulo, info, tipo, objetivos);

			//missao.Imprimir();

			missoes.Add(missao);
		}

		return missoes;
	}
}
