using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DrawingGrid {
    static DrawingGrid uniqueGrid;

    int gridSize;

    DrawingGrid(int x)
    {
        gridSize = x;
        //if (x / 16 == y / 9) quadsSize = x / 16;
        //else if (x / 40 == y / 30) quadsSize = x / 40;
        //else Debug.Log("Resolução não encontrada, deu merda");
    }

    /// <summary>
    /// Pega o traçado que o mouse fez,
    /// escala ele no mesmo tamanha que um quadrado de "Spell", 
    /// centraliza ele, e checa por quais quadrantes ele passou;
    /// </summary>
    /// <returns>Retorna uma "Spell" onde o mouse passou sobre;</returns>
    public QuadsDrawing ReturnMouseQuad(List<Vector3> mouseTrajectory)
    {
        QuadsDrawing returnSpell = new QuadsDrawing();
        float originX, originY;
        float quadX, quadY;
        float mouseX, mouseY;
        float quadsSize;
        float medium;
        float largerX = mouseTrajectory.Max(v => v.x);
        float largerY = mouseTrajectory.Max(v => v.y);
        float smallerX = mouseTrajectory.Min(v => v.x);
        float smallerY = mouseTrajectory.Min(v => v.y);
        float sizeX = largerX - smallerX;
        float sizeY = largerY - smallerY;


        if (sizeX >= sizeY)
        {
            quadsSize = sizeX;
            medium = (largerY + smallerY) / 2;
            originX = smallerX;
            originY = medium - (quadsSize / 2);
        }
        else
        {
            quadsSize = sizeY;
            medium = (largerX + smallerX) / 2;
            originY = smallerY;
            originX = medium - (quadsSize / 2);
        }
        if (quadsSize <= 0) return new QuadsDrawing(); 

        for (int i = 0; i < mouseTrajectory.Count; i++)
        {
            mouseX = mouseTrajectory[i].x - originX;
            mouseY = mouseTrajectory[i].y - originY;
            quadX = (mouseX == quadsSize ? mouseX : (mouseX + (quadsSize/gridSize))) / (quadsSize/gridSize);
            quadY = (mouseY == quadsSize ? mouseY : (mouseY + (quadsSize/gridSize))) / (quadsSize/gridSize);

            Quad newQuad = new Quad((int)quadX, (int)quadY);

            if (!returnSpell.IsQuadInSpell(newQuad)) returnSpell.AddQuad(newQuad);
        }
        //Debug.Log("retornou spell");
        return returnSpell;
    }

    /// <summary>
    /// implementação do pattern "singleton"
    /// com o objetivo de garantir a presenca de apenas um opbjeto do tipo "Grid"
    /// durante a execução do programa, e fornecer um acesso global a esse objeto;
    /// </summary>
    /// <returns>retorna a unica instancia de "grid"</returns>
    public static DrawingGrid GetGrid(int x)
    {
        if (uniqueGrid == null) uniqueGrid = new DrawingGrid(x);
        return uniqueGrid;
    }

    /// <summary>
    /// implementação do pattern "singleton"
    /// com o objetivo de garantir a presenca de apenas um opbjeto do tipo "Grid"
    /// durante a execução do programa, e fornecer um acesso global a esse objeto;
    /// Overload do método, com a acriação de um grid padrão de tamanho 9;
    /// </summary>
    /// <returns>retorna a unica instancia de "grid"</returns>
    public static DrawingGrid GetGrid()
    {
        if (uniqueGrid == null) uniqueGrid = new DrawingGrid(9);
        return uniqueGrid;
    }
}

//Uso do método com ints
/*
public Spell ReturnMouseQuad(List<Vector3> mouseTrajectory)
    {
        Spell returnSpell = new Spell();
        int originX, originY;
        int quadX, quadY;
        int mouseX, mouseY;
        int quadsSize;
        int medium;
        int largerX = (int)mouseTrajectory.Max(v => v.x);
        int largerY = (int)mouseTrajectory.Max(v => v.y);
        int smallerX = (int)mouseTrajectory.Min(v => v.x);
        int smallerY = (int)mouseTrajectory.Min(v => v.y);
        int sizeX = largerX - smallerX;
        int sizeY = largerY - smallerY;


        if (sizeX >= sizeY)
        {
            quadsSize = sizeX;
            medium = (largerY + smallerY) / 2;
            originX = smallerX;
            originY = medium - (quadsSize / 2);
        }
        else
        {
            quadsSize = sizeY;
            medium = (largerX + smallerX) / 2;
            originY = smallerY;
            originX = medium - (quadsSize / 2);
        }
        if (quadsSize <= 0) return new Spell(); 
        if (originX < 0 || originY < 0) Debug.Log("origem menor que zero, fudeu");

        Debug.Log("quadSize: "+ quadsSize+"  origem x "+originX + " origem y "+ originY);


        for (int i = 0; i < mouseTrajectory.Count; i++)
        {
            mouseX = (int)mouseTrajectory[i].x - originX;
            mouseY = (int)mouseTrajectory[i].y - originY;
            quadX = (mouseX == quadsSize ? mouseX : (mouseX + (quadsSize/gridSize))) / (quadsSize/gridSize);
            quadY = (mouseY == quadsSize ? mouseY : (mouseY + (quadsSize/gridSize))) / (quadsSize/gridSize);

            Quad newQuad = new Quad(quadX, quadY);
            if (!returnSpell.IsQuadInSpell(newQuad)) Debug.Log("quad "+i+" "+newQuad.ID);
            if (!returnSpell.IsQuadInSpell(newQuad)) returnSpell.AddQuad(newQuad);
        }
        return returnSpell;
    }
*/
//protocolo atendimento :13052883
//segurosura.enviardocumentos.com.br