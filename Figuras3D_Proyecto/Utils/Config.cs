using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Figuras3D_Proyecto.Utils
{
    public class Config
    {
        private static Config _instance;
        public static Config Instance => _instance ?? (_instance = new Config());

        public Color FillColor { get; set; } = Color.Yellow;
        public int AnimationDelay { get; set; } = 0;

        private Config() { }
    }
}
