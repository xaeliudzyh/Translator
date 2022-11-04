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
        {
            {"unsigned long long", "ulong"},

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
              
                // убираем инклуды, пространства имен и пустые строки, удаляем строки с ними
                if (line.Contains("#include") || line.Contains("namespace") || line.Contains("int main()") || line=="")
                {
                    initialString = initialString.Where(k
                        => k != initialString[i]).ToArray();
                    i--;
                    continue;
                }
                
                // "переводим" считывание с клавиатуры (beta-version)
                if(line.Contains("cin>>"))
                {
                    line = line.Replace("cin>>", "Console.ReadLine(");
                    while (Regex.Matches(line, ">>").Count > 0)
                        line = line.Replace(">>", ",");
                    line = line.Replace(";", ");");                
                }

                // "переводим" вывод с консоли
                if (line.Contains("cout<<"))
                {
                    line = line.Replace("cout<<", "Console.Write(");
                    while (Regex.Matches(line, "<<").Count > 0)
                        line = line.Replace("<<", ";\nConsole.Write(");
                    line = line.Replace(";", ");");
                }
                
                // пробегаемся по типам данных, и меняем на соответствующие в нужном ЯП
                foreach(string key in data.Keys)
                {
                    if (line.Contains(key))
                    {
                        line = line.Replace(key, data[key]);
                    }
                }
                initialString[i] = line;
            }
            return initialString;
        }



        static void Main(string[] fileNames)
        {
            string initialFile = fileNames[0];
            string destinationFile = fileNames[1];
            string[] changedCode= VarChanger(initialFile);
            string finalCode = "";

            finalCode+="namespace Namespace{\n class Program{ " +
                       "\n   static void Main(string[] args){\n";
            for (int i = 0; i < changedCode.Length; i++)
            {
                
                finalCode+="        ";
                finalCode+=changedCode[i];
                finalCode += "\n";
            }
            finalCode+=" }\n}";
            string[] codeArray = new[] { finalCode };
            Console.WriteLine(finalCode);
            WriteFile(destinationFile,codeArray);
            
        }
    }
       
}