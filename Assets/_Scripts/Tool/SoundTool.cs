#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Assets._Scripts.Tool
{
    public class SoundTool : EditorWindow
    {
        private const string TEMPLATE = "namespace Assets._Scripts.Manager.Sound.Enum\r\n{\r\n    public class SoundManagerEnum\r\n    {\r\n        %s1\r\n\r\n        %s2\r\n    }\r\n}";
        private static readonly string CLASS_EXPORT = $"{Application.dataPath}/_Scripts/Manager/Sound/Enum/";
        private const string SOURCE_PATH = "Resources/Manager/Sound";

        [MenuItem("Tools/Sound/Update SoundManagerEnum")]
        private static void GenerateSoundManager()
        {
            string sfxPath = $"{Application.dataPath}/{SOURCE_PATH}/SFX";
            string musiPath = $"{Application.dataPath}/{SOURCE_PATH}/Music";

            List<string> sfxFiles = Directory.GetFiles(sfxPath, "*.wav").Union(Directory.GetFiles(sfxPath, "*.mp3")).Union(Directory.GetFiles(sfxPath, "*.aif")).ToList();
            List<string> musicFiles = Directory.GetFiles(musiPath, "*.wav").Union(Directory.GetFiles(musiPath, "*.mp3")).Union(Directory.GetFiles(musiPath, "*.aif")).ToList();

            string sfxEnum = "public enum SFX\r\n" +
                "        {\r\n";

            foreach (string file in sfxFiles)
            {
                string[] split = Path.GetFileNameWithoutExtension(file).Split('_');

                if (split.Length > 1)
                {
                    string sound = "";

                    if (int.TryParse(split[0], out int index))
                    {
                        for (int i = 1; i < split.Length; i++)
                            sound += i < split.Length - 1 ? split[i] + "_" : split[i];

                        sound += $"_{split[0]}";

                        sfxEnum += "            " + sound + " = " + split[0] + ",\r\n";
                    }
                }
            }

            sfxEnum += "        }";

            string musicEnum = "public enum Music\r\n" +
               "        {\r\n";
            musicEnum += "            none = -1,\r\n";

            foreach (string file in musicFiles)
            {
                string[] split = Path.GetFileNameWithoutExtension(file).Split('_');

                if (split.Length > 1)
                {
                    string sound = "";

                    if (int.TryParse(split[0], out int index))
                    {
                        for (int i = 1; i < split.Length; i++)
                            sound += i < split.Length - 1 ? split[i] + "_" : split[i];

                        sound += $"_{split[0]}";

                        musicEnum += "            " + sound + " = " + split[0] + ",\r\n";
                    }
                }
            }

            musicEnum += "        }";

            string write = TEMPLATE.Replace("%s1", sfxEnum).Replace("%s2", musicEnum);

            File.WriteAllText($"{CLASS_EXPORT}/SoundManagerEnum.cs", write);

            AssetDatabase.Refresh();

            Debug.Log("Update SoundManagerEnum Complete");
        }
    }
}
#endif