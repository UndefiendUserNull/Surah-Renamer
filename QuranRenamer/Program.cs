using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    // Dictionary: Surah number -> Arabic name
    static readonly Dictionary<int, string> SurahNames = new()
    {
        {1, "الفاتحة"}, {2, "البقرة"}, {3, "آل عمران"}, {4, "النساء"},
        {5, "المائدة"}, {6, "الأنعام"}, {7, "الأعراف"}, {8, "الأنفال"},
        {9, "التوبة"}, {10, "يونس"}, {11, "هود"}, {12, "يوسف"},
        {13, "الرعد"}, {14, "إبراهيم"}, {15, "الحجر"}, {16, "النحل"},
        {17, "الإسراء"}, {18, "الكهف"}, {19, "مريم"}, {20, "طه"},
        {21, "الأنبياء"}, {22, "الحج"}, {23, "المؤمنون"}, {24, "النور"},
        {25, "الفرقان"}, {26, "الشعراء"}, {27, "النمل"}, {28, "القصص"},
        {29, "العنكبوت"}, {30, "الروم"}, {31, "لقمان"}, {32, "السجدة"},
        {33, "الأحزاب"}, {34, "سبإ"}, {35, "فاطر"}, {36, "يس"},
        {37, "الصافات"}, {38, "ص"}, {39, "الزمر"}, {40, "غافر"},
        {41, "فصلت"}, {42, "الشورى"}, {43, "الزخرف"}, {44, "الدخان"},
        {45, "الجاثية"}, {46, "الأحقاف"}, {47, "محمد"}, {48, "الفتح"},
        {49, "الحجرات"}, {50, "ق"}, {51, "الذاريات"}, {52, "الطور"},
        {53, "النجم"}, {54, "القمر"}, {55, "الرحمن"}, {56, "الواقعة"},
        {57, "الحديد"}, {58, "المجادلة"}, {59, "الحشر"}, {60, "الممتحنة"},
        {61, "الصف"}, {62, "الجمعة"}, {63, "المنافقون"}, {64, "التغابن"},
        {65, "الطلاق"}, {66, "التحريم"}, {67, "الملك"}, {68, "القلم"},
        {69, "الحاقة"}, {70, "المعارج"}, {71, "نوح"}, {72, "الجن"},
        {73, "المزمل"}, {74, "المدثر"}, {75, "القيامة"}, {76, "الإنسان"},
        {77, "المرسلات"}, {78, "النبإ"}, {79, "النازعات"}, {80, "عبس"},
        {81, "التكوير"}, {82, "الإنفطار"}, {83, "المطففين"}, {84, "الإنشقاق"},
        {85, "البروج"}, {86, "الطارق"}, {87, "الأعلى"}, {88, "الغاشية"},
        {89, "الفجر"}, {90, "البلد"}, {91, "الشمس"}, {92, "الليل"},
        {93, "الضحى"}, {94, "الشرح"}, {95, "التين"}, {96, "العلق"},
        {97, "القدر"}, {98, "البينة"}, {99, "الزلزلة"}, {100, "العاديات"},
        {101, "القارعة"}, {102, "التكاثر"}, {103, "العصر"}, {104, "الهمزة"},
        {105, "الفيل"}, {106, "قريش"}, {107, "الماعون"}, {108, "الكوثر"},
        {109, "الكافرون"}, {110, "النصر"}, {111, "المسد"}, {112, "الإخلاص"},
        {113, "الفلق"}, {114, "الناس"}
    };

    static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            ShowUsage();
            return;
        }

        if (args[0] == "--dummy")
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Please provide a folder path for dummy creation.");
                return;
            }

            string folder = args[1];
            int count = args.Length > 2 && int.TryParse(args[2], out int n) ? n : 10;
            CreateDummyFolder(folder, count);
        }
        else
        {
            string folder = args[0];
            RenameQuranFiles(folder);
        }
    }

    static void RenameQuranFiles(string folderPath)
    {
        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        foreach (string file in Directory.GetFiles(folderPath, "*.mp3"))
        {
            string filename = Path.GetFileName(file);
            string numberPart = Path.GetFileNameWithoutExtension(file);

            if (int.TryParse(numberPart, out int num) && SurahNames.ContainsKey(num))
            {
                string newName = $"{num:00} {SurahNames[num]}.mp3";
                string newPath = Path.Combine(folderPath, newName);

                File.Move(file, newPath, overwrite: true);
                Console.WriteLine($"Renamed: {filename} -> {newName}");
            }
            else
            {
                Console.WriteLine($"Skipping: {filename}");
            }
        }
    }

    static void CreateDummyFolder(string folderPath, int count)
    {
        Directory.CreateDirectory(folderPath);

        for (int i = 1; i <= count; i++)
        {
            string fileName = $"{i:000}.mp3";
            string path = Path.Combine(folderPath, fileName);

            // Create empty dummy file
            File.WriteAllBytes(path, Array.Empty<byte>());
        }

        Console.WriteLine($"Dummy folder created at: {folderPath} with {count} files");
    }

    static void ShowUsage()
    {
        Console.WriteLine("Usage:");
        Console.WriteLine("  QuranRenamer <folder_path>");
        Console.WriteLine("  QuranRenamer --dummy <folder_path> [count]");
    }
}
