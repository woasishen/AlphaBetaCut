using System;
using System.Diagnostics;
using System.Threading;

namespace AlphaBetaCut
{
    public class MaxMin
    {
        readonly Thread _computeThread;
        private int _computeTimes;
        private int _abCut;
        private readonly ABTree _abTree;
        private readonly ABTreeNode _baseNode;
        private ABTreeNode _lastNode;

        public MaxMin(ABTree abtree)
        {
            _abTree = abtree;
            _baseNode = GenTreeNode(0, 0);
            _lastNode = _baseNode;
            _computeThread = new Thread(FindBestPos);
        }

        public void StartFind()
        {
            _computeThread.Start();
        }

        private ABTreeNode GenTreeNode(int layer, int index)
        {
            var result = new ABTreeNode(_abTree.ABTreeLines[layer].ABTreeItems[index]);
            if (layer == Configs.LAYER_COUNT - 1)
            {
                return result;
            }
            for (var i = 0; i < Configs.CHILD_COUNT; i++)
            {
                result[i] = GenTreeNode(layer + 1, index * Configs.CHILD_COUNT + i);
            }
            return result;
        }

        private void FindBestPos()
        {
            ComputeMaxMin(Configs.LAYER_COUNT%2 == 0, _baseNode);
            Configs.LogMsg($"搜索完成，共递归{_computeTimes}次，ab剪枝{_abCut}次");
            Configs.ComputeFinished();
        }

        private int ComputeMaxMin(bool isMaxLayer, ABTreeNode treeNode, int? alpha = null, int? beta = null)
        {
            _lastNode.ABTreeItem.Handling = false;
            _lastNode = treeNode;
            treeNode.ABTreeItem.Handling = true;
            treeNode.ABTreeItem.Alpha = alpha;
            treeNode.ABTreeItem.Beta = beta;
            treeNode.ABTreeItem.Best = null;

            Configs.ExcutSemaphore.WaitOne();
            Configs.EnableNextStep();

            if (treeNode.ABTreeItem.TextEnable)
            {
                _computeTimes++;
                treeNode.ABTreeItem.Best = treeNode.ABTreeItem.Gole;
                return treeNode.ABTreeItem.Gole;
            }

            for (var i = 0; i < Configs.CHILD_COUNT; i++)
            {
                var tempGole = ComputeMaxMin(
                    !isMaxLayer,
                    treeNode[i],
                    isMaxLayer ? Configs.MIN : treeNode.ABTreeItem.Best,
                    isMaxLayer ? treeNode.ABTreeItem.Best : Configs.MAX);
                if (treeNode.ABTreeItem.Best == null)
                {
                    treeNode.ABTreeItem.Best = tempGole;
                }
                else
                {
                    treeNode.ABTreeItem.Best = isMaxLayer
                        ? Math.Max(treeNode.ABTreeItem.Best.Value, tempGole)
                        : Math.Min(treeNode.ABTreeItem.Best.Value, tempGole);
                }

                if (i != Configs.CHILD_COUNT - 1)
                {
                    if (alpha <= tempGole && tempGole <= beta)
                    {
                        _abCut++;
                        Configs.LogMsg($"Alpha Beta Cut 一次，共：{_abCut}次");

                        for (var j = i + 1; j < Configs.CHILD_COUNT; j++)
                        {
                            ABTreeNode.ShowABCut(treeNode[j]);
                        }

                        return tempGole;
                    }
                }
            }
            return treeNode.ABTreeItem.Best ?? 0;
        }
    }
}
