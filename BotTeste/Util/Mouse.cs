using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotTeste.Util
{
    public static class Mouse
    {
        public static Point BuscaPosicao(List<string> parametros)
        {
            int x;
            int y;

            try
            {
                x = Convert.ToInt32(parametros[0]);
                y = Convert.ToInt32(parametros[1]);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return new Point(x, y);

        }
    }
}
