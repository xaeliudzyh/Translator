using System.Text.RegularExpressions;
using System.Text;


namespace Translator
{


    class Program
    {
        //считываем файл
        public static string[] ReadFile(string fileName)
        {
            string[] initialCode = File.ReadAllLines(fileName);
            return initialCode;
        }

        //записываем в файл
        public static void WriteFile(string fileName, string[] translatedCode)
        {
            File.WriteAllLines(fileName, translatedCode);
        }

            public static Dictionary<string, string> data = new Dictionary<string, string>()
            {           {"unsigned long long", "ulong"},
                        {"unsigned long", "uint" },
                        {"unsigned short", "ushort" },
                        {"long long" , "long"},
                        {"long", "int" }
            };
        //меняем содержимое(собственно процесс ре-транляции)
        public static string[] VarChanger(string file)
        {
            

            //изначальный код(на плюсах)
            string[] initialString = ReadFile(file);
            for(int i = 0; i < initialString.Length; i++)
            {
                string line = initialString[i];
                //string[] subs = line.Split(' ');

                if (line.Contains("#include") || line.Contains("namespace"))
                {
                    initialString[i] = "";
                    continue;
                }

                // "переводим" считывание с клавиатуры
                if(line.Contains("cin>>"))
                {
                    line.Replace("cin>>", "Console.ReadLine(");
                    while (Regex.Matches(line, ">>").Count > 0)
                        line.Replace(">>", ",");
                    line.Replace(";", ");");
                  
                }

                // "переводим" вывод с консоли
                if (line.Contains("cout<<"))
                {
                    line.Replace("cout<<", "Console.WriteLine(");
                    while (Regex.Matches(line, "<<").Count > 0)
                        line.Replace("<<", ",");
                    line.Replace(";", ");");

                }
                line = line.Replace("long", "xxx");
                // пробегаемся по типам данных, и меняем на соответствующие в нужном ЯП
                foreach(string key in data.Keys)
                {
                    if (line.Contains(key))
                    {
                        line.Replace(key, data[key]);

                    }
                }

                /*if(h.Contains("unsigned long long"))
                {
                    while (Regex.Matches(h, "unsigned long long").Count > 0)
                        h.Replace("unsigned long long", data["unsigned long long"]);

                }

                if (h.Contains("unsigned long"))
                {
                    while (Regex.Matches(h, "unsigned long").Count > 0)
                        h.Replace("unsigned long", data["unsigned long"]);

                }

                if (h.Contains("unsigned short"))
                {
                    while (Regex.Matches(h, "unsigned short").Count > 0)
                        h.Replace("unsigned short", data["unsigned short"]);

                }


                for (int j = 0; j < subs.Length; j++)
                {
                    if (subs[j] == "unsigned")
                    {
                        if (subs[j + 1] == "short")
                        {
                            subs[j] = "ushort";
                            subs[j + 1] = "";
                        }
                        else
                        {
                            if (subs[j + 1] == "long" && subs[j + 2] == "long")
                            {
                                subs[j] = "ulong";
                                subs[j + 1] = "";
                                subs[j + 2] = "";
                                j++;
                            }
                            else
                            {
                                if (subs[j + 1] == "long")
                                {
                                    subs[j] = "uint";
                                    subs[j + 1] = "";
                                }
                            }
                        }
                        j++;
                    }
                    else
                    {
                        if (subs[j]=="long" && subs[j + 1] == "long")
                        {
                            subs[j + 1] = "";
                            j++;
                        }
                        else
                            if (data.ContainsKey(subs[j]))
                                subs[j] = data[subs[j]];

                    }
                }
                string res = "";
                for (int z = 0; z < subs.Length; z++)
                {
                    res += subs[z];
                    res += " ";
                }
                initialString[i] = res;*/
                initialString[i] = line;

            }
          

            return initialString;

        }





        static void Main(string[] fileName)
        {
            string[] j=VarChanger("../../../data/reTranslated1.txt");
            Console.WriteLine("namespace Namespace{\n tclass Program{ \n   static void Translator(string[] args){\n");
            for (int i = 0; i < j.Length; i++)
            {
                Console.Write("       ");
                Console.WriteLine(j[i]);
            }
            Console.WriteLine("}");
        }
    }
       
}