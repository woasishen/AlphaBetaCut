using System;
using System.Collections.Generic;
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
            _computeThread = new Thread(FindBestPos);
        }

        public void StartFind()
        {
            _computeThread.Start();
        }

        private ABTreeNode GenTreeNode(int layer, int index)
        {
            var isLeafNode = layer == Configs.LAYER_COUNT - 1;
            var result = new ABTreeNode(_abTree.ABTreeLines[layer].ABTreeItems[index], isLeafNode);
            if (!isLeafNode)
            {
                for (int i = 0; i < Configs.CHILD_COUNT; i++)
                {
                    result[i] = GenTreeNode(layer + 1, index * Configs.CHILD_COUNT + i);
                }
            }

            return result;
        }

        private void FindBestPos()
        {
            _lastNode = _baseNode;
            _baseNode.ABTreeItem.Handling = true;

            Configs.ExcutSemaphore.WaitOne();
            Configs.EnableNextStep();

            _computeTimes = 0;
            _abCut = 0;
            var maxGoleABTreeItemList = new List<ABTreeItem>();
            var maxGole = Configs.MIN;

            for (var i = 0; i < 2; i++)
            {
                var tempGole = ComputeMaxMin(
                    _baseNode[i],
                    Configs.MIN,
                    maxGole);
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
            Configs.ComputeFinished();
        }

        private int ComputeMaxMin(ABTreeNode treeNode, int alpha, int beta)
        {
            _lastNode.ABTreeItem.Handling = false;
            _lastNode = treeNode;
            treeNode.ABTreeItem.Handling = true;
            treeNode.ABTreeItem.Alpha = alpha;
            treeNode.ABTreeItem.Beta = beta;

            Configs.ExcutSemaphore.WaitOne();
            Configs.EnableNextStep();

            var maxGole = Configs.MIN;


            for (var i = 0; i < Configs.CHILD_COUNT; i++)
            {
                int tempGole;
                if (treeNode[i].IsLeafNode)
                {
                    _lastNode.ABTreeItem.Handling = false;
                    _lastNode = treeNode[i];
                    treeNode[i].ABTreeItem.Handling = true;

                    tempGole = treeNode[i].ABTreeItem.Gole;
                    _computeTimes++;

                    treeNode[i].ABTreeItem.Alpha = alpha;
                    treeNode[i].ABTreeItem.Beta = beta;

                    Configs.ExcutSemaphore.WaitOne();
                    Configs.EnableNextStep();

                    treeNode[i].ABTreeItem.Best = tempGole;
                }
                else
                {
                    tempGole = ComputeMaxMin(
                        treeNode[i],
                        -beta,
                        -maxGole);
                }
                maxGole = Math.Max(maxGole, tempGole);

                _lastNode.ABTreeItem.Handling = false;
                _lastNode = treeNode;
                treeNode.ABTreeItem.Handling = true;
                treeNode.ABTreeItem.Best = maxGole;

                Configs.ExcutSemaphore.WaitOne();
                Configs.EnableNextStep();

                if (tempGole >= beta)
                {
                    _abCut++;
                    Configs.LogMsg($"Alpha Beta Cut 一次，共：{_abCut}次");

                    for (int j = i + 1; j < Configs.CHILD_COUNT; j++)
                    {
                        treeNode[j].ABTreeItem.ShowAbCut();
                    }


                    return tempGole;
                }

                
            }

            treeNode.ABTreeItem.Best = maxGole;
            return maxGole;
        }
    }
}
