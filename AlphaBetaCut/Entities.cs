using System;
using System.Collections.Generic;

namespace AlphaBetaCut
{
    public struct Pos
    {
        public int X { get; }
        public int Y { get; }

        public Pos(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }

    public enum CellStatus
    {
        Empty = 0,
        Black = 1,
        White = 10,
    }

    public static class CellStatusHelper
    {
        public static CellStatus Not(CellStatus s)
        {
            switch (s)
            {
                case CellStatus.Black:
                    return CellStatus.White;
                case CellStatus.White:
                    return CellStatus.Black;
                case CellStatus.Empty:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(s), s, null);
            }
            throw new Exception("cellstatus do not valid to not");
        }
    }

    public class ABTreeNode
    {
        private readonly ABTreeNode[] _abTreeNodeChildren;

        public ABTreeNode(ABTreeItem abTreeItem)
        {
            _abTreeNodeChildren = new ABTreeNode[Configs.CHILD_COUNT];
            ABTreeItem = abTreeItem;
        }

        public ABTreeItem ABTreeItem { get; }

        public ABTreeNode this[int index]
        {
            get { return _abTreeNodeChildren[index]; }
            set { _abTreeNodeChildren[index] = value; }
        }

        public override string ToString()
        {
            return ABTreeItem.ToString();
        }

        public static void ShowABCut(ABTreeNode node)
        {
            if (node == null)
            {
                return;
            }
            node.ABTreeItem.ShowAbCut();
            foreach (var child in node._abTreeNodeChildren)
            {
                ShowABCut(child);
            }
        }

        public static implicit operator bool(ABTreeNode node)
        {
            return node != null;
        }
    }
}
