﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;
using HarmonyLib;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
using UnityEngine.Video;

namespace aRandomKiwi.RimThemes
{
    [StaticConstructorOnStartup]
    static class Utils
    {
        public static Color accessibilityColor = new Color(0.1f,0.1f,0.1f,0.5f);
        public static bool tempDisableDynColor= false;
        public static bool tempEnableAccessibilityMode = false;
        public static bool tempDisableNoTransparentText = false;
        public static bool tempDisableButtonsBackground = false;
        public static bool textFontSetterLock = false;
        public static int squeezedDrawOptionListingIndex = 0;
        public static float squeezedDrawOptionListingIndexReturnVal = 0;

        public static VideoPlayer CurrentMainAnimatedBg = null;
        public static bool CurrentMainAnimatedBgPlaying = false;
        public static bool CurrentMainAnimatedBgSourceSet = false;

        public static string sponsor = "M";

        //static public bool firstOpenedConsole = false;
        //static public int vipWindowID = -1;
        static public List<WDESC> lastShowedWin = new List<WDESC>();
        static public bool needRefresh = false;
        static public ModContentPack currentMod;
        static public Mod currentModInst;
        static public Settings modSettings;
        static public string releaseInfo = "RimThemes NX rev10";
        static public string releaseDesc =
            "-Added the ability to disable all defaults themes (or specific ones), allowing to significantly reduce RimThemes memory footprint" + Environment.NewLine + Environment.NewLine
            + "-Only 3 default themes are enabled by default (RimThemes memory consumption reduced by 40% on average)" + Environment.NewLine + Environment.NewLine
            + "-Added the ability to have multiple random game textures" + Environment.NewLine + Environment.NewLine
            + "-Improved settings menu accessibility UI" + Environment.NewLine + Environment.NewLine
            + "-Added the ability to have multiple random images in the loading screen" + Environment.NewLine + Environment.NewLine
            + "-Loader now display tips" + Environment.NewLine + Environment.NewLine
            + "-Added the ability for themes to hide the translations infos dialog (determined by the theme by default)" + Environment.NewLine + Environment.NewLine
            + "-Improved readability of certain default themes" + Environment.NewLine + Environment.NewLine
            + "-Fix opacity override setting not working properly" + Environment.NewLine + Environment.NewLine + Environment.NewLine
            + "Note : For Themers the 'Example Theme' has been updated, check the 'Themes Makers' thread on Steam." + Environment.NewLine;

        private static Traverse cachedLabelWidthCache = null;
        static private bool initCachedLabelWidthCache = false;

        public static void resetCachedLabelWidthCache()
        {
            if (!initCachedLabelWidthCache)
            {
                initCachedLabelWidthCache = true;
                cachedLabelWidthCache = Traverse.CreateWithType("Verse.GenUI").Field("labelWidthCache");
            }

            //Reset label width caches
            Dictionary<string, Vector2> labelWidthCache = (Dictionary<string, Vector2>)cachedLabelWidthCache.GetValue();
            labelWidthCache.Clear();
            //GenUI.labelWidthCache.Clear();
        }

        public static void startLoadingTheme()
        {
            AssetBundle cab = null;
            LoaderGM.curStep = LoaderSteps.loadingTheme;
            //Loading all dependances from the themes THEN generating theme textures inside LoaderGM
            Themes.startInit();

            //We notify the loader to load the preloaded textures
            LoaderGM.themeLoadMode = 1;
            Thread.Sleep(250);
            LoaderGM.texThemesToLoad = true;

            //We notify the loader to load the preloaded songs
            LoaderGM.themeLoadMode = 2;
            Thread.Sleep(250);
            LoaderGM.songsToLoad = true;

            //Loading the font asset bundle
            try
            {
                LoaderGM.themeLoadMode = 3;
                Thread.Sleep(250);
                //Loading the encoded font bundle into memory
                Themes.fontsPackage.Add(AssetBundle.LoadFromMemory(FontsPackage.fonts)); //LoadFromFile(Utils.currentMod.RootDir + Path.DirectorySeparatorChar + "fontspackage");
                Themes.LogMsg("Load main fonts package OK");
            }
            catch (Exception e)
            {
                Themes.fontsPackage = null;
                Themes.LogError("Loading fonts package : " + e.Message);
            }

            //Loading of potential font assetsbundle provided by mods
            foreach (string fbPath in Themes.DBfontsBundleToLoad)
            {
                try
                {
                    cab = AssetBundle.LoadFromFile(fbPath);
                    if (cab == null)
                        throw new Exception("Invalid font package "+fbPath);

                    Themes.fontsPackage.Add(cab);
                    Themes.LogMsg("Load external fonts package "+fbPath+" OK");
                }
                catch (Exception e)
                {
                    Themes.LogError("Loading external fonts package : " + e.Message);
                }
            }


            //Loading Fonts
            LoaderGM.themeLoadMode = 4;
            Thread.Sleep(250);
            LoaderGM.fontsToLoad = true;

            //If enabled we try to change the background of the main menu
            if (!Settings.disableRandomBg)
                Themes.setNewRandomBg();
        }


        /*
         * Check if an image whose path is passed as a parameter exists (without the extension, the test function exists for this image in .png and .jpg format)
         */
        static public bool texFileExist(string path)
        {
            if (File.Exists(path + ".png"))
                return true;
            else if (File.Exists(path + ".jpg"))
                return true;
            else
                return false;
        }

        static public byte[] readAllBytesTexFile(string path)
        {
            if (File.Exists(path + ".png"))
                return File.ReadAllBytes(path + ".png");
            else if (File.Exists(path + ".jpg"))
                return File.ReadAllBytes(path + ".jpg");
            else
                return null;
        }


        static public bool isValidImgExt(string ext)
        {
            if (ext == ".png" || ext == ".jpg")
                return true;
            else
                return false;
        }

        static public bool isDefaultThemeAllowed(string theme)
        {
            if(theme == "Centipede" && Settings.enableCentipedeTheme
                    || theme == "Classic Cassandra" && Settings.enableClassicCassandraTheme
                    || theme == "Cyberpunk" && Settings.enableCyberpunkTheme
                    || theme == "Mechanoid Cluster" && Settings.enableMechanoidClusterTheme
                    || theme == "Muffalo" && Settings.enableMuffaloTheme
                    || theme == "Phoebe Chillax" && Settings.enablePhoebeChillaxTheme
                    || theme == "Rim-Life 2" && Settings.enableRimLife2Theme
                    || theme == "Scyther" && Settings.enableScytherTheme
                    || theme == "Singularity" && Settings.enableSingularityTheme
                    || theme == "Thrumbo" && Settings.enableThrumboTheme
                    || theme == "USFM" && Settings.enableUSFMTheme
                    || theme == "CentipedeClassic" && Settings.enableCentipedeClassicTheme)
            {
                return true;
            }
            return false;
        }


        static public bool isNSBlacklistedWindowsType(Window win)
        {
            string type = win.GetType().FullName;
            if (type == "DubsMintMinimap.MainTabWindow_MiniMap" || type == "DubsMintMinimap.MainTabWindow_MiniMapSetting")
                return true;
            else
                return false;
        }

        static public string ReplaceLastOccurrence(string str, string toReplace, string replacement)
        {
            return Regex.Replace(str, $@"^(.*){toReplace}(.*?)$", $"$1{replacement}$2");
        }

        public static string RWBaseFolderPath
        {
            get
            {
                return new DirectoryInfo(UnityData.dataPath).Parent.FullName;
            }
        }

        public static void applyWindowFillColorOpacityOverride(string newTheme)
        {
            if (Settings.disabledOverrideThemeWindowFillColorAlpha)
                return;

            Type classType = typeof(FloatMenuOption).Assembly.GetType("Verse.Widgets");
            if (classType == null)
                return;

            //Change tapestry transparency level
            if (Themes.DBTexTapestry.ContainsKey(Settings.curTheme))
            {
                for (int x = 0; x < Themes.DBTexTapestry[Settings.curTheme].width; x++)
                {
                    for (int y = 0; y < Themes.DBTexTapestry[Settings.curTheme].height; y++)
                    {
                        Color pxl = Themes.DBTexTapestry[Settings.curTheme].GetPixel(x, y);
                        pxl.a = Settings.overrideThemeWindowFillColorAlphaLevel;
                        Themes.DBTexTapestry[Settings.curTheme].SetPixel(x, y, pxl);
                    }
                }
                Themes.DBTexTapestry[Settings.curTheme].Apply();
            }

            Color cColor=Color.black;

            //Change of color component filling of the current theme
            if (Themes.DBColor.ContainsKey(newTheme) && Themes.DBColor[newTheme].ContainsKey("Widgets") && Themes.DBColor[newTheme]["Widgets"].ContainsKey("WindowBGFillColor"))
            {
                cColor = Themes.DBColor[newTheme]["Widgets"]["WindowBGFillColor"];
                cColor.a = Settings.overrideThemeWindowFillColorAlphaLevel;
                Themes.DBColor[newTheme]["Widgets"]["WindowBGFillColor"] = cColor;


            }
            else
            {
                if (Themes.DBColor.ContainsKey("-1§Vanilla") && Themes.DBColor["-1§Vanilla"].ContainsKey("Widgets") && Themes.DBColor["-1§Vanilla"]["Widgets"].ContainsKey("WindowBGFillColor"))
                {
                    //Change alpha component of the vanilla theme
                    cColor = Themes.DBColor["-1§Vanilla"]["Widgets"]["WindowBGFillColor"];
                    cColor.a = Settings.overrideThemeWindowFillColorAlphaLevel;
                    Themes.DBColor["-1§Vanilla"]["Widgets"]["WindowBGFillColor"] = cColor;
                }
                else
                    return;
            }

            classType.GetField("WindowBGFillColor", (BindingFlags)(BindingFlags.Public | BindingFlags.Static)).SetValue(null, cColor);
        }
    }
}
