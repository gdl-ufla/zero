using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Interface : MonoBehaviour
{
	public GameObject painelConfig;
	public GameObject painelMissoes;
	public GameObject painelDebug;

	void Awake()
	{
		FecharConfig();
		FecharMissoes();
		FecharDebug();
	}

	//
	public void AbrirConfig()
	{
		painelConfig.SetActive(true);
	}

	public void FecharConfig()
	{
		painelConfig.SetActive(false);
	}

	//
	public void AbrirMissoes()
	{
		painelMissoes.SetActive(true);
	}

	public void FecharMissoes()
	{
		painelMissoes.SetActive(false);
	}

	//
	public void AbrirDebug()
	{
		painelDebug.SetActive(true);
	}

	public void FecharDebug()
	{
		painelDebug.SetActive(false);
	}
}

