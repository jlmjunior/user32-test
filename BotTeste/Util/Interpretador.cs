using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotTeste.Util
{
    class Interpretador
    {
        public string InterpretaComando(string comando)
        {
            string exec = string.Empty;

            try
            {
                exec = comando.Substring(comando.IndexOf('[') + 1, comando.IndexOf(']') - (comando.IndexOf('[') + 1));
            }
            catch (Exception)
            {
                return null;
            }
            return exec;
        }

        public List<string> InterpretaParametro(string comando)
        {
            List<string> parametros = new List<string>();

            try
            {
                comando = comando.Substring(comando.IndexOf('(') + 1, comando.IndexOf(')') - (comando.IndexOf('(') + 1));
            }
            catch (Exception)
            {
                return parametros;
            }

            string temp = string.Empty;

            foreach (char c in comando)
            {
                if (c.Equals(','))
                {
                    parametros.Add(temp);
                    temp = string.Empty;
                }
                else
                {
                    temp += c;
                }
            }

            if (!string.IsNullOrWhiteSpace(temp)) parametros.Add(temp);

            return parametros;
        }
    }
}
