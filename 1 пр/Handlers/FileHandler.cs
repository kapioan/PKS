using CorporatePractice1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CorporatePractice1.Handlers
{
    public static class FileHandler
    {
        public static void Setup()
        {
            bool exception_flag = false;

            if (!Directory.Exists(Program.Dir))
            {
                Directory.CreateDirectory(Program.Dir);
;
                exception_flag = true;
            }

            if (!File.Exists(Program.Fullpath))
            {
                File.Create(Program.Fullpath)
                    .Close();

                var json_string = JsonSerializer.Serialize(new Everything());

                File.WriteAllText(Program.Fullpath, json_string);

                exception_flag = true;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            if (exception_flag)
                Console.WriteLine("Не был найден ресурсный файл JSON, создаём новый пустой!");
        }

        public static Everything Deserialize()
        {
            var file_contents = File.ReadAllText(Program.Fullpath);

            Everything obj = JsonSerializer.Deserialize<Everything>(file_contents, new JsonSerializerOptions { IncludeFields = true }) ?? new Everything();

            return obj;
        }

        public static void Serialize(Everything obj)
        {
            var string_json = JsonSerializer.Serialize(obj);

            File.WriteAllText(Program.Fullpath, string_json);
        }
    }
}
