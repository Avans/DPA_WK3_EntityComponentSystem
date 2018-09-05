using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityComponentSystemDemo.Components
{
    public class VisualComponent : Component
    {
        public ConsoleColor Color;
        public ConsoleColor AlternativeColor;

        public bool UseAlternativeColor;
    }
}
