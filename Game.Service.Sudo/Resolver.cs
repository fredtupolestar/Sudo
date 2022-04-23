using Game.Service.Sudo.Common;
using Game.Service.Sudo.Models;

namespace Game.Service.Sudo;

public class Resolver
{
    private readonly SudoModel _sudoModel;

    public List<SudoNode[,]> Resolves => _sudoModel.Resolves;

    public Resolver(SudoModel sudoModel)
    {
        this._sudoModel = sudoModel;
    }

    public bool Resolve(ref int deep)
    {
        SudoNode? node = _sudoModel.NextEmpty();
        if(node is null)
        {
            if(_sudoModel.VerifySudoNodes())
            {
                _sudoModel.Resolves.Add(this._sudoModel.SudoNodes.Copy());
                if(_sudoModel.Resolves.Count <=1)
                    return false;
                else
                    return true;
            }
        }

        if( node is null || !node.Suggest.Any())
            return false;
        
        foreach (var suggestNum in node.Suggest)
        {
            node.SetValue(suggestNum);
            deep+=1;
            var isResolved = Resolve(ref deep);
            if(isResolved)
                return true;
            node.SetValue(0);
        }
        
        return false;
    }

    private void Print(){
        Console.WriteLine("-----Step-----");
        for (int row = 0; row < 9; row++)
        {
            var rowData = _sudoModel.SudoNodes.SliceRow(row);
            Console.WriteLine(string.Join(",", rowData.Select(a=>a.Value)));
        }
    }
}
