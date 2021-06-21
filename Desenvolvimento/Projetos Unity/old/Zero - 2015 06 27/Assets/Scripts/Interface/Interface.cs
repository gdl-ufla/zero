using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Interface : MonoBehaviour
{
	public GameObject painelConfig;
	public GameObject painelMissoes;
	public GameObject painelDebug;
	public GameObject painelRealizacoes;
	public GameObject painelPlacar;

	void Awake()
	{
		FecharConfig();
		FecharMissoes();
		FecharDebug();
		FecharRealizacoes();
		FecharPlacar();
	}

	//
	public void TelaJogo()
	{
		Application.LoadLevel(Constantes.telaJogo);
	}

	public void TelaMenu()
	{
		GerJogo.Limpar();
		Application.LoadLevel(Constantes.telaMenu);
	}

	//
	public void AbrirRealizacoes()
	{
		if (painelRealizacoes)
		{
			painelRealizacoes.SetActive(true);
			MostrarRealizacoes.VerificarCor();
		}
	}
	
	public void FecharRealizacoes()
	{
		if (painelRealizacoes)
			painelRealizacoes.SetActive(false);
	}

	//
	public void AbrirPlacar()
	{
		if (painelPlacar)
			painelPlacar.SetActive(true);
	}
	
	public void FecharPlacar()
	{
		if (painelPlacar)
			painelPlacar.SetActive(false);
	}

	//
	public void AbrirConfig()
	{
		if (painelConfig)
			painelConfig.SetActive(true);
	}

	public void FecharConfig()
	{
		if (painelConfig)
			painelConfig.SetActive(false);
	}

	//
	public void AbrirMissoes()
	{
		if (painelMissoes)
			painelMissoes.SetActive(true);
	}

	public void FecharMissoes()
	{
		if (painelMissoes)
			painelMissoes.SetActive(false);
	}

	//
	public void AbrirDebug()
	{
		if (painelDebug)
			painelDebug.SetActive(true);
	}

	public void FecharDebug()
	{
		if (painelDebug)
			painelDebug.SetActive(false);
	}
}

