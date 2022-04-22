using Sudo.Core.Models;
using System.Linq;

namespace Sudo.Core;

public class Resolver
{
    private SudoNode[,] _nodes;

    public SudoNode[,] SudoNodes => _nodes;

    private int _recursionCnt = 0;
    public int RecursionTimes => _recursionCnt;

    public Resolver(SudoNode[,] nodes)
    {
        this._nodes = nodes;
    }

    public bool ResolveSudo(SudoNode[,] nodes)
    {
        this._recursionCnt = 0;
        this._nodes = nodes;
        bool isResolved = TryResolve();
        return isResolved;
    }

    private bool TryResolve()
    {
        _recursionCnt +=1;
        SudoNode? emptyNode = GetNextEmptyNode();
        if (emptyNode is null)
            return true;

        var suits = emptyNode.SuitableNumbers;
        if (suits is null || !suits.Any())
            return false;

        for (int i = 0; i < suits.Count; i++)
        {
            emptyNode.Value = suits[i];
            Common.LinkNodes(ref this._nodes);

            var isResolved = TryResolve();
            if (isResolved)
                return true;
        }

        return false;
    }

    public Enums.GameStatusEnum TestSudo(SudoNode[,] sudoNodes)
    {
        bool isCompletelyInsert = true;
        bool isCellVerify = true;
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                var node = sudoNodes[row,col];
                if(node.Value == 0)
                {
                    isCompletelyInsert= false;
                    break;
                }

                if(!node.IsVerify)
                {
                    isCellVerify=false;
                    break;
                }
            }
        }
        if(!isCompletelyInsert)
            return Enums.GameStatusEnum.Pending;

        if(!isCellVerify)
            return Enums.GameStatusEnum.Failed;

        return Enums.GameStatusEnum.Win;
    }

    private SudoNode? GetNextEmptyNode()
    {
        SudoNode? node = null;
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if(this._nodes[row,col].Value != 0)
                    continue;
                
                node = this._nodes[row,col];
                break;
            }
        }
        
        return node;
    }
}
