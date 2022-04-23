using Game.Service.Sudo.Common;
using Game.Service.Sudo.Models;

namespace Game.Service.Sudo;

public class Resolver
{
    private readonly SudoModel _sudoModel;
    private readonly bool _isUseSuggest = false;
    public List<SudoNode[,]> Resolves => _sudoModel.Resolves;

    public Resolver(SudoModel sudoModel, bool isUseSuggest = false)
    {
        this._isUseSuggest= isUseSuggest;
        this._sudoModel = sudoModel;
    }

    public bool Resolve(out int deep)
    {
        deep = 0;
        var isResolved = ResolveSudo(ref deep);
        if(this._sudoModel.Resolves.Count>0)
            return true;

        return false;
    }

    private bool ResolveSudo(ref int deep)
    {
        SudoNode? node = _sudoModel.NextEmpty(_isUseSuggest);
        if(node is null)
        {
            if(_sudoModel.VerifySudoNodes())
            {
                _sudoModel.Resolves.Add(this._sudoModel.SudoNodes.Copy());
                if(_sudoModel.Resolves.Count <=9)
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
            var isResolved = ResolveSudo(ref deep);
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
