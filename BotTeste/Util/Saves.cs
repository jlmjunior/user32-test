using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotTeste.Util
{
    class Saves
    {
        public void SalvarArquivo(string path, List<string> commandos)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter($"{path}", false, Encoding.Default))
                {
                    foreach (var commando in commandos)
                    {
                        sw.WriteLine($"{commando};");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> LerArquivo(string path)
        {
            List<string> comandos = new List<string>();

            try
            {
                using (StreamReader sw = new StreamReader(path))
                {
                    string comando = string.Empty;

                    foreach (char c in sw.ReadToEnd())
                    {
                        if (c.Equals(';'))
                        {
                            comandos.Add(comando);
                            comando = string.Empty;
                        }
                        else
                        {
                            comando += c;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return comandos;
        }
    }
}
