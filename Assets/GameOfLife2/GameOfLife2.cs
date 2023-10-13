using UnityEngine;

public class GameOfLife2 : GameOfLife
{
    public override void UpdateCell(cell cell)
    {
        if (cell.isAlive)
            cell.cellRenderer.color = new Color(cell.cellRenderer.color.r, cell.cellRenderer.color.g, cell.cellRenderer.color.b, 255);
    }
}
