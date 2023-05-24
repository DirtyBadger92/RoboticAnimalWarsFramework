using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbilityShape", menuName = "TurnBasedTools/Ability/Shapes/Create DirectionalAbilityShape", order = 1)]
public class DirectionalAbilityShape : AbilityShape
{
    [SerializeField]
    bool m_bOnlyMyEnemies;

    public override List<ILevelCell> GetCellList(GridUnit InCaster, ILevelCell InCell, int InRange, bool bAllowBlocked, GameTeam m_EffectedTeam)
    {
        List<ILevelCell> cells = new List<ILevelCell>();

        if (InCell && InRange > 0)
        {
            cells.AddRange(GetCellsInDirection(InCell, InRange, Vector2Int.up, bAllowBlocked, m_EffectedTeam));
            cells.AddRange(GetCellsInDirection(InCell, InRange, Vector2Int.down, bAllowBlocked, m_EffectedTeam));
            cells.AddRange(GetCellsInDirection(InCell, InRange, Vector2Int.right, bAllowBlocked, m_EffectedTeam));
            cells.AddRange(GetCellsInDirection(InCell, InRange, Vector2Int.left, bAllowBlocked, m_EffectedTeam));
        }

        if (m_bOnlyMyEnemies)
        {
            List<ILevelCell> enemyCells = new List<ILevelCell>();
            foreach (var currCell in cells)
            {
                GridUnit unitOnCell = currCell.GetUnitOnCell();
                if (unitOnCell)
                {
                    GameTeam affinityToCaster = GameManager.GetTeamAffinity(InCaster.GetTeam(), unitOnCell.GetTeam());
                    if (affinityToCaster == GameTeam.Hostile)
                    {
                        enemyCells.Add(currCell);
                    }
                }
            }

            return enemyCells;
        }
        else
        {
            return cells;
        }
    }

    List<ILevelCell> GetCellsInDirection(ILevelCell StartCell, int InRange, Vector2Int Direction, bool bAllowBlocked, GameTeam m_EffectedTeam)
    {
        List<ILevelCell> cells = new List<ILevelCell>();

        if (InRange > 0)
        {
            ILevelCell curserCell = StartCell;

            for (int i = 0; i < InRange; i++)
            {
                Vector2Int nextCellPos = curserCell.GetIndex() + Direction;
                curserCell = GetCellAtPosition(nextCellPos);

                if (curserCell == null || (curserCell.IsBlocked() && !bAllowBlocked))
                {
                    break;
                }

                GridObject gridObj = curserCell.GetObjectOnCell();
                if (gridObj)
                {
                    if (m_EffectedTeam == GameTeam.None)
                    {
                        break;
                    }

                    GameTeam objAffinity = GameManager.GetTeamAffinity(gridObj.GetTeam(), StartCell.GetCellTeam());
                    if (objAffinity == GameTeam.Friendly && m_EffectedTeam == GameTeam.Hostile)
                    {
                        break;
                    }
                }

                cells.Add(curserCell);
            }
        }

        return cells;
    }

    private ILevelCell GetCellAtPosition(Vector2Int position)
    {
        // Pobierz referencję do poziomu lub siatki poziomu
        ILevelGrid levelGrid = GetComponentInParent<ILevelGrid>();

        // Sprawdź, czy poziom lub siatka poziomu istnieje
        if (levelGrid != null)
        {
            // Pobierz komórkę na podstawie pozycji
            ILevelCell cell = levelGrid.GetCellAtPosition(position);
            return cell;
        }

        return null;
    }
}