// Copyright © 2018 3D Haven.  All Rights Reserved.
#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Haven.PathPainter
{
    internal class CompMgr
    {
        // TODO: get these from config
        public const string DLL_KEY = "h8i493ZcWPaY0FUgQmhYfrVilSvFkq5X"; //check this in config
        private const string DNAME = "Pptr";
        private const string DEXT = ".pp";
        private const string LP = "Library/" + DNAME + ".3dhp";

        private static string ms_f;

        [InitializeOnLoadMethod]
        static void Onload()
        {
            if (NC())
            {
                return;
            }

            // Need to wait for things to import before creating the common menu - Using delegates and only check menu when something gets imported
            AssetDatabase.importPackageCompleted -= OnImportPackageCompleted;
            AssetDatabase.importPackageCompleted += OnImportPackageCompleted;

            AssetDatabase.importPackageCancelled -= OnImportPackageCancelled;
            AssetDatabase.importPackageCancelled += OnImportPackageCancelled;

            AssetDatabase.importPackageFailed -= OnImportPackageFailed;
            AssetDatabase.importPackageFailed += OnImportPackageFailed;
        }

        /// <summary>
        /// Called when a package import is Completed.
        /// </summary>
        private static void OnImportPackageCompleted(string packageName)
        {
            OnPackageImport();
        }

        /// <summary>
        /// Called when a package import is Cancelled.
        /// </summary>
        private static void OnImportPackageCancelled(string packageName)
        {
            OnPackageImport();
        }

        /// <summary>
        /// Called when a package import fails.
        /// </summary>
        private static void OnImportPackageFailed(string packageName, string error)
        {
            OnPackageImport();
        }

        /// <summary>
        /// Used to run things after a package was imported.
        /// </summary>
        private static void OnPackageImport()
        {
            // No need for these anymore
            AssetDatabase.importPackageCompleted -= OnImportPackageCompleted;
            AssetDatabase.importPackageCancelled -= OnImportPackageCancelled;
            AssetDatabase.importPackageFailed -= OnImportPackageFailed;

            M();
        }

        private static void F()
        {
#if !HAVEN_DEV
            Debug.LogError("Files are corrupt or missing. Please Reimport Path Painter.");
#endif
        }

        private static bool M()
        {
            if (C())
            {
#if !HAVEN_REL
                G();
                RE(ms_f);
#endif
                return true;
            }

            G();
            if (string.IsNullOrEmpty(ms_f))
            {
                return false;
            }

            R(ms_f);
#if !HAVEN_REL
            string f = ms_f.Replace("Editor/", "");
            string p = ms_f + DNAME + T + "e";
            AssetDatabase.DeleteAsset(f + "Path Painter.dll");
            if (!AssetDatabase.CopyAsset(f + DNAME + T + DEXT, f + "Path Painter.dll") ||
                !AssetDatabase.CopyAsset(p + DEXT, p + ".dll"))
            {
                EditorUtility.DisplayDialog("Path Painter",
                    "\nPath Painter was not completely imported.\n\n" +
                    "You can try right-click -> \"Reimport\" once Unity completes importing and compiling.\n\n" +
                    "Please contact support with your invoice number if this message keeps popping up.\n\n", "Ok");
                return false;
            }
            RE(ms_f);
            AssetDatabase.Refresh();
#endif
            return true;
        }

        private static bool NC()
        {
            string re = GRE();

            if (!string.IsNullOrEmpty(re))
            {
                if (re == GE())
                {
                    return true;
                }

                return M();
            }

            if (OE())
            {
                return true;
            }

            string f = DNAME + T + "e";
            string x = ".dll";

            string p = P(f, f + x);
            if (string.IsNullOrEmpty(p))
            {
                return M();
            }

            ms_f = p.Replace(f + x, "");
            if (C())
            {
#if !HAVEN_REL
                RE(ms_f); 
#endif
                return true;
            }

            return false;
        }

        private static void R(string f)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(f);
            if (!dirInfo.Exists)
            {
                return;
            }

            FileInfo[] files = dirInfo.GetFiles(DNAME + "*.dll");
            foreach (var file in files)
            {
                AssetDatabase.DeleteAsset(f + file.Name);
            }
        }

        private static void MV(string ofn, string nfn)
        {
            AssetDatabase.MoveAsset(ofn, nfn);
        }

        private static void G()
        {
            if (string.IsNullOrEmpty(ms_f))
            {
                string f = DNAME + "jhce";
                string x = DEXT;

                string p = P(f, f + x);
                if (!string.IsNullOrEmpty(p))
                {
                    ms_f = p.Replace(f + x, "");
                    return;
                }
            }
        }

        private static string GRE()
        {
            if (!File.Exists(LP))
            {
                return null;
            }

            string p = File.ReadAllText(LP);
            if (!File.Exists(p))
            {
                return null;
            }

            return File.ReadAllText(p);
        }

        private static string GE()
        {
            string[] s = Application.unityVersion.Split('.');
            string v = s[0] + "." + s[1];
#if NET_4_6
            return v + "-n4";
#else
            return v + "-n3";
#endif
        }

        private static void RE(string p)
        {
            p += "currenv.3dht";
            File.WriteAllText(LP, p);
            File.WriteAllText(p, GE());
        }

        private static bool OE()
        {
            string[] ends = new string[] { "/Editor/Pptrjhc.pp", "/Editor/Pptrjhc.dll",
                "Path Painter 1.0.0 U5.dll", "Path Painter 1.0.0 U5.dll.pp" };
            foreach (var path in AssetDatabase.GetAllAssetPaths())
            {
                foreach (string end in ends)
                {
                    if (path.EndsWith(end))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static string P(string fn, string f)
        {
            string[] a = AssetDatabase.FindAssets(fn, null);
            for (int i = 0; i < a.Length; i++)
            {
                string p = AssetDatabase.GUIDToAssetPath(a[i]);
                if (Path.GetFileName(p) == f)
                {
                    return p;
                }
            }
            return "";
        }

#if UNITY_5
#if NET_4_6
        private const string T = "e";
#else
        private const string T = "e3";
#endif
#elif UNITY_2017
#if NET_4_6
        private const string T = "jg";
#else
        private const string T = "jg3";
#endif
#elif UNITY_2018_2 || UNITY_2018_1
#if NET_4_6
        private const string T = "jh";
#else
        private const string T = "jh3";
#endif
#else
#if NET_4_6
        private const string T = "jhc";
#else
        private const string T = "jhc3";
#endif
#endif


        private static bool C()
        {
#if !HAVEN_REL
            string[] i = new string[]
            {
                "ee",
                "jge",
                "jhe",
                "jhce",
                "e3e",
                "jg3e",
                "jh3e",
                "jhc3e",
            };

            HashSet<string> w = new HashSet<string>();
            HashSet<string> b = new HashSet<string>();

            string t = T + "e";
            foreach (string a in i)
            {
                if (a != t)
                {
                    b.Add(DNAME + a + ".dll");
                }
            }
            w.Add(DNAME + t + ".dll");

            foreach (string p in AssetDatabase.GetAllAssetPaths())
            {
                string f = Path.GetFileName(p);
                if (w.Contains(f))
                {
                    w.Remove(f);
                    ms_f = p.Replace(f, "");
                }

                if (b.Contains(f))
                {
                    return false;
                }
            }

            return w.Count < 1;
#else
            return false;
#endif
        }
    }
}
#endif
