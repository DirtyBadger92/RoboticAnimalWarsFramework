using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSelfAbilityShape", menuName = "TurnBasedTools/Ability/Shapes/Create SelfAbilityShape", order = 1)]
public class SelfAbilityShape : AbilityShape
{
    public override List<ILevelCell> GetCellList(GridUnit InCaster, ILevelCell InCell, int InRange, bool bAllowBlocked = true, GameTeam m_EffectedTeam = GameTeam.None)
    {
        List<ILevelCell> cellList = new List<ILevelCell>();

        Vector2 casterPosition = InCell.GetIndex();

        for (int i = -InRange; i <= InRange; i++)
        {
            int x = Mathf.RoundToInt(casterPosition.x) + i;
            int y = Mathf.RoundToInt(casterPosition.y);

            ILevelCell cell = GetCellAtPosition(new Vector2(x, y));

            if (cell != null && (bAllowBlocked || !cell.IsBlocked()) && (m_EffectedTeam == GameTeam.None || GetOccupyingUnit(cell).GetTeam() == m_EffectedTeam))
            {
                cellList.Add(cell);
            }
        }

        for (int i = -InRange; i <= InRange; i++)
        {
            int x = Mathf.RoundToInt(casterPosition.x);
            int y = Mathf.RoundToInt(casterPosition.y) + i;

            ILevelCell cell = GetCellAtPosition(new Vector2(x, y));

            if (cell != null && (bAllowBlocked || !cell.IsBlocked()) && (m_EffectedTeam == GameTeam.None || GetOccupyingUnit(cell).GetTeam() == m_EffectedTeam))
            {
                cellList.Add(cell);
            }
        }

        return cellList;
    }

    private ILevelCell GetCellAtPosition(Vector2 position)
    {
        // Implement the logic to retrieve the level cell at the given position
        // This method depends on your specific implementation
        // Return null or an appropriate level cell based on your implementation
        return null;
    }

    private GridUnit GetOccupyingUnit(ILevelCell cell)
    {
        // Implement the logic to retrieve the occupying unit of the cell
        // This method depends on your specific implementation
        // Return null or an appropriate grid unit based on your implementation
        return null;
    }
}
