using System.Collections.Generic;

namespace Assets.Scripts.Entities
{
    internal interface IBuffable
    {
        List<Buff> buffs { get;  set; }
    }
}
