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
            ComputeMaxMin(0, _baseNode, Configs.MIN, Configs.MAX);
            Configs.LogMsg($"搜索完成，共递归{_computeTimes}次，ab剪枝{_abCut}次");
            Configs.ComputeFinished();
        }

        private int ComputeMaxMin(int layer, ABTreeNode treeNode, int alpha, int beta)
        {
            _lastNode.ABTreeItem.Handling = false;
            _lastNode = treeNode;
            treeNode.ABTreeItem.Handling = true;
            treeNode.ABTreeItem.Alpha = alpha;
            treeNode.ABTreeItem.Beta = beta;

            Configs.ExcutSemaphore.WaitOne();
            Configs.EnableNextStep();

            bool isMaxLayer = (Configs.LAYER_COUNT - layer) % 2 == 0;

            int? bestGole = null;

            for (var i = 0; i < Configs.CHILD_COUNT; i++)
            {
                int tempGole;
                if (layer == Configs.LAYER_COUNT - 2)
                {
                    _lastNode.ABTreeItem.Handling = false;
                    _lastNode = treeNode[i];
                    treeNode[i].ABTreeItem.Handling = true;

                    tempGole = treeNode[i].ABTreeItem.Gole;
                    _computeTimes++;

                    treeNode[i].ABTreeItem.Alpha = alpha;
                    treeNode[i].ABTreeItem.Beta = bestGole ?? Configs.MAX;

                    Configs.ExcutSemaphore.WaitOne();
                    Configs.EnableNextStep();

                    treeNode[i].ABTreeItem.Best = tempGole;
                }
                else
                {
                    tempGole = ComputeMaxMin(
                        layer + 1,
                        treeNode[i],
                        alpha,
                        bestGole ?? Configs.MAX);
                }
                bestGole = isMaxLayer
                    ? bestGole == null ? tempGole : Math.Max(bestGole.Value, tempGole) 
                    : bestGole == null ? tempGole : Math.Min(bestGole.Value, tempGole);

                _lastNode.ABTreeItem.Handling = false;
                _lastNode = treeNode;
                treeNode.ABTreeItem.Handling = true;
                treeNode.ABTreeItem.Best = bestGole;

                Configs.ExcutSemaphore.WaitOne();
                Configs.EnableNextStep();

                if (i != Configs.CHILD_COUNT - 1 && tempGole >= beta)
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
            treeNode.ABTreeItem.Best = bestGole;
            Debug.Assert(bestGole != null, "bestGole != null");
            return bestGole.Value;
        }
    }
}
