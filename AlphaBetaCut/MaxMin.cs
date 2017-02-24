using System;
using System.Collections.Generic;

namespace AlphaBetaCut
{
    public class MaxMin
    {
        private int _computeTimes;
        private int _abCut;
        private readonly ABTree _abTree;
        private readonly ABTreeNode _baseNode;

        public MaxMin(ABTree abtree)
        {
            _abTree = abtree;
            _baseNode = GenTreeNode(0, 0);
        }

        private ABTreeNode GenTreeNode(int layer, int index)
        {
            if (layer == _abTree.LayerCount)
            {
                return null;
            }
            var result = new ABTreeNode(_abTree.ABTreeLines[layer].ABTreeItems[index])
            {
                LeftChild = GenTreeNode(layer + 1, index*2),
                RightChild = GenTreeNode(layer + 1, index*2 + 1)
            };

            return result;
        }

        public List<ABTreeItem> FindBestPos()
        {
            _computeTimes = 0;
            _abCut = 0;
            var maxGoleABTreeItemList = new List<ABTreeItem>();
            var maxGole = int.MinValue;

            for (var i = 0; i < 2; i++)
            {
                var tempGole = -ComputeMaxMin(
                    _baseNode[i],
                    1,
                    int.MinValue,
                    -maxGole);
                if (tempGole == maxGole)
                {
                    maxGoleABTreeItemList.Add(_baseNode[i].ABTreeItem);
                }
                else if (tempGole > maxGole)
                {
                    maxGoleABTreeItemList.Clear();
                    maxGole = tempGole;
                    maxGoleABTreeItemList.Add(_baseNode[i].ABTreeItem);
                }
            }
            Configs.LogMsg($"搜索完成，共递归{_computeTimes}次，ab剪枝{_abCut}次");
            return maxGoleABTreeItemList;
        }

        private int ComputeMaxMin(ABTreeNode treeNode, int deep, int alpha, int beta)
        {
            _computeTimes++;
            var maxGole = int.MinValue;

            for (var i = 0; i < 2; i++)
            {
                int tempGole;
                if (deep < _abTree.LayerCount)
                {
                    tempGole = -ComputeMaxMin(
                        treeNode[i],
                        deep + 1,
                        -beta,
                        -maxGole);
                }
                else
                {
                    tempGole = treeNode.ABTreeItem.Gole;
                }
                if (tempGole >= beta)
                {
                    _abCut++;
                    return tempGole;
                }

                maxGole = Math.Max(maxGole, tempGole);
            }
            return maxGole;
        }
    }
}
