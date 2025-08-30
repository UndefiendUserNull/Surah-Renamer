import os
import sys

# Surah dictionary: key = number as int, value = Arabic name
surah_names = {
    1: "الفاتحة",
    2: "البقرة",
    3: "آل عمران",
    4: "النساء",
    5: "المائدة",
    6: "الأنعام",
    7: "الأعراف",
    8: "الأنفال",
    9: "التوبة",
    10: "يونس",
    11: "هود",
    12: "يوسف",
    13: "الرعد",
    14: "إبراهيم",
    15: "الحجر",
    16: "النحل",
    17: "الإسراء",
    18: "الكهف",
    19: "مريم",
    20: "طه",
    21: "الأنبياء",
    22: "الحج",
    23: "المؤمنون",
    24: "النور",
    25: "الفرقان",
    26: "الشعراء",
    27: "النمل",
    28: "القصص",
    29: "العنكبوت",
    30: "الروم",
    31: "لقمان",
    32: "السجدة",
    33: "الأحزاب",
    34: "سبإ",
    35: "فاطر",
    36: "يس",
    37: "الصافات",
    38: "ص",
    39: "الزمر",
    40: "غافر",
    41: "فصلت",
    42: "الشورى",
    43: "الزخرف",
    44: "الدخان",
    45: "الجاثية",
    46: "الأحقاف",
    47: "محمد",
    48: "الفتح",
    49: "الحجرات",
    50: "ق",
    51: "الذاريات",
    52: "الطور",
    53: "النجم",
    54: "القمر",
    55: "الرحمن",
    56: "الواقعة",
    57: "الحديد",
    58: "المجادلة",
    59: "الحشر",
    60: "الممتحنة",
    61: "الصف",
    62: "الجمعة",
    63: "المنافقون",
    64: "التغابن",
    65: "الطلاق",
    66: "التحريم",
    67: "الملك",
    68: "القلم",
    69: "الحاقة",
    70: "المعارج",
    71: "نوح",
    72: "الجن",
    73: "المزمل",
    74: "المدثر",
    75: "القيامة",
    76: "الإنسان",
    77: "المرسلات",
    78: "النبإ",
    79: "النازعات",
    80: "عبس",
    81: "التكوير",
    82: "الإنفطار",
    83: "المطففين",
    84: "الإنشقاق",
    85: "البروج",
    86: "الطارق",
    87: "الأعلى",
    88: "الغاشية",
    89: "الفجر",
    90: "البلد",
    91: "الشمس",
    92: "الليل",
    93: "الضحى",
    94: "الشرح",
    95: "التين",
    96: "العلق",
    97: "القدر",
    98: "البينة",
    99: "الزلزلة",
    100: "العاديات",
    101: "القارعة",
    102: "التكاثر",
    103: "العصر",
    104: "الهمزة",
    105: "الفيل",
    106: "قريش",
    107: "الماعون",
    108: "الكوثر",
    109: "الكافرون",
    110: "النصر",
    111: "المسد",
    112: "الإخلاص",
    113: "الفلق",
    114: "الناس",
}


def rename_quran_files(folder_path):
    for filename in os.listdir(folder_path):
        if filename.lower().endswith(".mp3"):
            try:
                num_str = filename.split(".")[0]
                num = int(num_str)

                if num in surah_names:
                    new_name = f"{num:02d} {surah_names[num]}.mp3"
                    old_path = os.path.join(folder_path, filename)
                    new_path = os.path.join(folder_path, new_name)

                    os.rename(old_path, new_path)
                    print(f"Renamed: {filename} -> {new_name}")
                else:
                    print(f"Surah number {num} not found in dictionary.")
            except ValueError:
                print(f"Skipping file (not numbered): {filename}")


def create_dummy_folder(folder_path, count=10):
    os.makedirs(folder_path, exist_ok=True)
    for i in range(1, count + 1):
        file_name = f"{i:03d}.mp3"
        file_path = os.path.join(folder_path, file_name)
        # Create empty dummy files
        with open(file_path, "wb") as f:
            f.write(b"")
    print(f"Dummy folder created at: {folder_path} with {count} files")


if __name__ == "__main__":
    if len(sys.argv) < 2:
        print("Usage:")
        print("  python rename_quran.py <folder_path>")
        print("  python rename_quran.py --dummy <folder_path> [count]")
        sys.exit(1)

    if sys.argv[1] == "--dummy":
        if len(sys.argv) < 3:
            print("Please provide folder path for dummy creation.")
            sys.exit(1)
        folder = sys.argv[2]
        count = int(sys.argv[3]) if len(sys.argv) > 3 else 10
        create_dummy_folder(folder, count)
    else:
        folder = sys.argv[1]
        rename_quran_files(folder)
