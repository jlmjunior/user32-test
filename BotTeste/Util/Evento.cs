using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BotTeste.Util
{
    public static class Evento
    {
        public static int x;
        public static int y;
        public static int currentType = 0;

        [DllImport("user32.dll")]
        public static extern void mouse_event(uint dwFlags, uint dx = 0, uint dy = 0, uint cButtons = 0, int dwExtraInfo = 0);
        //public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        //public const int MOUSEEVENTF_LEFTUP = 0x04;

        public static void ExecutaComando(string comando)
        {
            Interpretador interpretador = new Interpretador();

            string exec = interpretador.InterpretaComando(comando);
            List<string> parametros = interpretador.InterpretaParametro(comando);

            ExecutaEvento(exec, parametros);
        }

        private static void ExecutaEvento(string exec, List<string> parametros)
        {
            switch (exec.ToLower())
            {
                case "mouse_click":
                    Cursor.Position = Mouse.BuscaPosicao(parametros);
                    mouse_event(0x02);
                    mouse_event(0x04);
                    break;
                case "mouse_down":
                    Cursor.Position = Mouse.BuscaPosicao(parametros);
                    mouse_event(0x02);
                    break;
                case "mouse_up":
                    Cursor.Position = Mouse.BuscaPosicao(parametros);
                    mouse_event(0x04);
                    break;
                case "sleep":
                    System.Threading.Thread.Sleep(3000);
                    break;

                default: return;
            }
        }
    }
}
