using System.Collections.Generic;

namespace TyperPad.Common.Model
{
    public class OutputState
    {
        public Key Key { set; get; }
        public List<Key> Modificators { set; get; } = new List<Key>();
    }
}