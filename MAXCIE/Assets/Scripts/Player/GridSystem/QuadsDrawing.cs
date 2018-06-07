using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuadsDrawing {
    /// <summary>
    /// Lista contendo os quadrantes que compões a magia;
    /// </summary>
    [SerializeField] List<Quad> quadsList;

    bool isTrueSpell;

    /// <summary>
    /// Gets a value indicating whether this instance is true spell.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is true spell; otherwise, <c>false</c>.
    /// </value>
    public bool IsTrueSpell
    {
        get { return isTrueSpell; }
        protected set { isTrueSpell = value; }
    }
    /// <summary>
    /// encapsulamento da propriedade "quads.Count" da lista "quads";
    /// </summary>
    public int SpellLength
    {
        get { return quadsList.Count; }
    }

    public QuadsDrawing()
    {
        quadsList = new List<Quad>();
        isTrueSpell = false;
    }

    /// <summary>
    /// adiciona um quadrante para a lista de quadrantes do "spell"
    /// </summary>
    /// <param name="quad">o quadrante a ser adicionado</param>
    public void AddQuad(Quad quad)
    {
        quadsList.Add(quad);
    }

    /// <summary>
    /// retorna um quadrante da lista de quadrantes que compõe um "spell"
    /// </summary>
    /// <param name="i">índice do quadrante escolhido</param>
    /// <returns>quadrante escolhido</returns>
    public Quad GetSpellQuad(int i)
    {
        return quadsList[i];
    }   

    /// <summary>
    /// Chama o método "Clear()" da lista "quads",
    /// sendo apenas um emcapsulamento da lista; 
    /// </summary>
    public void EraseSpell()
    {
        quadsList.Clear();
    }

    public string ReturnQuadsID()
    {
        string spellId = "";
        foreach (Quad quad in quadsList)
        {
            spellId += " /" + quad.ID;
        }
        return spellId;
    }

    /// <summary>
    /// Retorna true se o quadrante está presente na magia,
    /// percorrendo a lista comparando cada um de seus quadrandes com o quadrante dado;
    /// </summary>
    /// <param name="quad">quadrante a ser comparado</param>
    /// <returns>bool com base se o quadrante está presente na magia</returns>
    public bool IsQuadInSpell(Quad quad)
    {
        for (int i = 0; i < quadsList.Count; i++)
        {
            if (quad == quadsList[i]) return true;
        }
        return false;
    }

    public void ConvertToTrueSpell()
    {
        if (IsTrueSpell) Debug.Log("deu merda, converteu um ja convertido");
        else
        {

        }
    }

    /// <summary>
    /// Compara um "spell" com outro "spell",
    /// e retorna a porcentagem de similaridade entre os dois
    /// </summary>
    /// <param name="x">"spell" 2</param>
    /// <param name="y">"spell" 1</param>
    /// <returns>porcentagem de similaridade</returns>
    public static float CompareSpells(QuadsDrawing x, QuadsDrawing y)
    {
        int similarQuads = 0;
        int smallerSize;
        int largestSize;
        bool yIsLarger;

        x.ConvertToTrueSpell();
        y.ConvertToTrueSpell();

        //Debug.Log(x.ReturnQuadsID() + "nova spell");
        //Debug.Log(y.ReturnQuadsID() + "spell book");

        if (x.SpellLength <= y.SpellLength)
        {
            smallerSize = x.SpellLength;
            largestSize = y.SpellLength;
            yIsLarger = true;
        }
        else
        {
            smallerSize = y.SpellLength;
            largestSize = x.SpellLength;
            yIsLarger = false;
        }
        for (int a = 0; a < smallerSize; a++)
        {
            if (yIsLarger)
            {
                if (y.IsQuadInSpell(x.GetSpellQuad(a))) similarQuads++;
            }
            else
            {
                if (x.IsQuadInSpell(y.GetSpellQuad(a))) similarQuads++;
            }
        }
        return (100 * similarQuads) / largestSize;
    }

}
