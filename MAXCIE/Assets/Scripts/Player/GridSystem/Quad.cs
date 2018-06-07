using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Quad
{
    public string ID
    {
        get { return xCord + "," + yCord; }
    }

    [SerializeField] int xCord;
    [SerializeField] int yCord;

    public Quad(int x, int y)
    {
        xCord = x;
        yCord = y;
    }

    /// <summary>
    /// overload de operador "==", pergunta se as coordenadas dos dois quads são iguais;
    /// </summary>
    /// <param name="a"> quad 1</param>
    /// <param name="b">quad 2</param>
    /// <returns>retorna bool ué</returns>
    public static bool operator ==(Quad a, Quad b)
    {
        if (a.xCord == b.xCord && a.yCord == b.yCord) return true;
        else return false;
    }
    /// <summary>
    /// overload de operador "!=", pergunta se as coordenadas dos dois quads são diferentes;
    /// </summary>
    /// <param name="a"> quad 1</param>
    /// <param name="b">quad 2</param>
    /// <returns>retorna bool ué</returns>
    public static bool operator !=(Quad a, Quad b)
    {
        if (a.xCord != b.xCord || a.yCord != b.yCord) return true;
        else return false;
    }
    /// <summary>
    /// funções que o visual studio recomenda, tomar MUITO cuidado ao usar, 
    /// alterações são provavelmente necessarias;
    /// </summary>
    /// <param name="obj">objeto a ser comparado com</param>
    /// <returns>bool dependendo se o objeto for igual</returns>
    public override bool Equals(object obj)
    {
        if (!(obj is Quad))
        {
            return false;
        }

        var quad = (Quad)obj;
        return xCord == quad.xCord && yCord == quad.yCord;
    }
    /// <summary>
    /// funções que o visual studio recomenda sobrescrever, tomar MUITO cuidado ao usar, 
    /// alterações são provavelmente necessarias;
    /// essa eu não faço ideia doq seja :p
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        var hashCode = 1929966999;
        hashCode = hashCode * -1521134295 + base.GetHashCode();
        hashCode = hashCode * -1521134295 + xCord.GetHashCode();
        hashCode = hashCode * -1521134295 + yCord.GetHashCode();
        return hashCode;
    }
}