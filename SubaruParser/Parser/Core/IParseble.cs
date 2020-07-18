using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.Core
{
    interface IParseble
    {
        void Parse(INode node);
    }
}
