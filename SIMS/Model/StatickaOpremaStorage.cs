using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class StatickaOpremaStorage : Storage<string, StatickaOprema, StatickaOpremaStorage>
    {
        protected override string getPath()
        {
            return @".\..\..\..\Data\statickaOprema.json";
        }
        protected override string getKey(StatickaOprema statickaOprema)
        {
            return statickaOprema.id;
        }

        protected override void RemoveReferences(string key)
        {

        }

    }
}
