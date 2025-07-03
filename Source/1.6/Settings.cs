using UnityEngine;
using Verse;
using System;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;

namespace aRandomKiwi.RimThemes
{
    class Settings : ModSettings
    {
        public Settings(){
            Utils.modSettings = this;

            overrideThemeWindowFillColorAlphaLevelTmp = overrideThemeWindowFillColorAlphaLevel;
        }

        public static bool disabledOverrideThemeWindowFillColorAlpha = true;
        public static float overrideThemeWindowFillColorAlphaLevel = 0.75f;
        public static float overrideThemeWindowFillColorAlphaLevelTmp = 0.75f;
        public static int rimthemesLogoMode = 3;
        public static int windowsShadowMode = 3;
        public static bool keepCurrentBg = false;
        public static bool disableRandomBg = true;
        public static bool disableVideoBg = true;
        public static bool disableCustomFontsInConsole = true;
        public static bool disableWallpaper = false;
        public static bool disableCustomFonts = false;
        public static bool disableCustomCursors = false;
        public static bool disableParticle = true;
        public static bool modManagerAsOptionList = false;
        public static bool disableCustomLoader = false;
        public static bool disableCustomSounds = false;
        public static bool disableCustomSongs = false;
        public static int dialogStacking = 1;
        public static int menuAlignment = 3;
        public static bool disableButtonBG = false;
        public static int windowAnimation = (int)WindowAnim.Theme;
        public static bool disableTapestry = false;
        public static bool hideLoadingText = false;
        public static bool verboseMode = false;
        public static string curRandomBg = null;
        public static string lastVersionInfo = "";
        public static bool disableFontFilterModePoint = true;
        public static bool disableTinyCustomFont = false;
        public static int expansionsIconsMode = 3;

        public static bool disableTinyCustomFontPrev = false;
        public static bool disableCustomSongsPrev = false;
        public static bool disableCustomSoundsPrev = false;
        public static bool disableCustomFontsPrev = false;
        public static bool disableCustomCursorsPrev = false;
        public static bool disableRandomBgPrev = true;
        public static bool resetLabelWidth = false;
        public static bool disableFontFilterModePointPrev = true;
        public static int ludeonLogoMode = 1;
        public static int cornerInfoMode = 1;
        public static bool disableDefaultThemes = false;

        public static bool enableCentipedeTheme = true;
        public static bool enableClassicCassandraTheme = true;
        public static bool enableCyberpunkTheme = true;
        public static bool enableMechanoidClusterTheme = true;
        public static bool enableMuffaloTheme = true;
        public static bool enablePhoebeChillaxTheme = true;
        public static bool enableRimLife2Theme = true;
        public static bool enableScytherTheme = true;
        public static bool enableSingularityTheme = true;
        public static bool enableThrumboTheme = true;
        public static bool enableUSFMTheme = true;


        public static bool SectionGeneralExpanded = false;
        public static bool SectionDefaultThemesExpanded = false;
        public static bool SectionOpacityExpanded = false;
        public static bool SectionRandomBgExpanded = false;
        public static bool SectionDialogStackingExpanded = false;
        public static bool SectionMainMenuAlignmentExpanded = false;
        public static bool SectionWindowAnimExpanded = false;
        public static bool SectionRimThemesLogoExpanded = false;
        public static bool SectionWindowShadowExpanded = false;
        public static bool SectionExtensionIconExpanded = false;
        public static bool SectionLudeonLogoExpanded = false;
        public static bool SectionInfosExpanded = false;

        public static Vector2 scrollPosition = Vector2.zero;

        public static string curTheme = "-1§Vanilla";

        public static void DoSettingsWindowContents(Rect inRect)
        {
            inRect.yMin += 15f;
            inRect.yMax -= 15f;

            var defaultColumnWidth = (inRect.width - 50);
            Listing_Standard list = new Listing_Standard() { ColumnWidth = defaultColumnWidth };

            //Image logo
            if( Widgets.ButtonImage(new Rect((inRect.width / 2) - 90, inRect.y, 180, 144), Loader.logoTex, Color.white, Color.green))
                Find.WindowStack.Add(new Dialog_ThemesList());

            var outRect = new Rect(inRect.x, inRect.y+135, inRect.width, inRect.height-135);
            var scrollRect = new Rect(0f, 150f, inRect.width - 16f, inRect.height * 5f + 50 );
            Widgets.BeginScrollView(outRect, ref scrollPosition, scrollRect, true);

            list.Begin(scrollRect);

            list.Gap(10);
            if (SectionGeneralExpanded)
                GUI.color = Color.gray;
            else
                GUI.color = Color.green;

            if (list.ButtonText("RimTheme_SettingsGlobalSection".Translate()))
            {
                SectionGeneralExpanded = !SectionGeneralExpanded;
            }
            GUI.color = Color.white;

            if (SectionGeneralExpanded)
            {
                list.CheckboxLabeled("RimTheme_SettingsDisableTinyCustomFont".Translate(), ref disableTinyCustomFont);
                list.CheckboxLabeled("RimTheme_SettingsDisableVideoBackground".Translate(), ref disableVideoBg);
                list.CheckboxLabeled("RimTheme_SettingsDisableWallpaper".Translate(), ref disableWallpaper);
                list.CheckboxLabeled("RimTheme_SettingsDisableFont".Translate(), ref disableCustomFonts);
                list.CheckboxLabeled("RimTheme_SettingsDisableCustomFontConsole".Translate(), ref disableCustomFontsInConsole);
                list.CheckboxLabeled("RimTheme_SettingsDisableCursor".Translate(), ref disableCustomCursors);
                list.CheckboxLabeled("RimTheme_SettingsDisableParticle".Translate(), ref disableParticle);
                list.CheckboxLabeled("RimTheme_SettingsShowThemeManagerAsList".Translate(), ref modManagerAsOptionList);
                list.CheckboxLabeled("RimTheme_SettingsDisableCustomSounds".Translate(), ref disableCustomSounds);
                list.CheckboxLabeled("RimTheme_SettingsDisableCustomSongs".Translate(), ref disableCustomSongs);
                list.CheckboxLabeled("RimTheme_SettingsDisableButtonBG".Translate(), ref disableButtonBG);
                list.CheckboxLabeled("RimTheme_SettingsDisableTapestry".Translate(), ref disableTapestry);
                list.CheckboxLabeled("RimTheme_SettingsHideLoadingText".Translate(), ref hideLoadingText);
                list.CheckboxLabeled("RimTheme_SettingsDisableCustomLoader".Translate(), ref disableCustomLoader);
                list.CheckboxLabeled("RimTheme_SettingsVerboseLogs".Translate(), ref verboseMode);
                list.CheckboxLabeled("RimTheme_SettingsDisableFontFilterModePoint".Translate(), ref disableFontFilterModePoint);
            }

            if (SectionDefaultThemesExpanded)
                GUI.color = Color.gray;
            else
                GUI.color = Color.green;

            if (list.ButtonText("RimTheme_SettingsDefaultThemesSection".Translate()))
            {
                SectionDefaultThemesExpanded = !SectionDefaultThemesExpanded;
            }
            GUI.color = Color.white;

            if (SectionDefaultThemesExpanded)
            {
                list.CheckboxLabeled("RimTheme_SettingsDisableDefaultThemes".Translate(), ref disableDefaultThemes);
                if (!disableDefaultThemes)
                {
                    list.Gap(25);
                    list.CheckboxLabeled("Centipede", ref enableCentipedeTheme);
                    list.CheckboxLabeled("Classic Cassandra", ref enableClassicCassandraTheme);
                    list.CheckboxLabeled("Cyberpunk", ref enableCyberpunkTheme);
                    list.CheckboxLabeled("Mechanoid Cluster", ref enableMechanoidClusterTheme);
                    list.CheckboxLabeled("Muffalo", ref enableMuffaloTheme);
                    list.CheckboxLabeled("Phoebe Chillax", ref enablePhoebeChillaxTheme);
                    list.CheckboxLabeled("Rim-Life 2", ref enableRimLife2Theme);
                    list.CheckboxLabeled("Scyther", ref enableScytherTheme);
                    list.CheckboxLabeled("Singularity", ref enableSingularityTheme);
                    list.CheckboxLabeled("Thrumbo", ref enableThrumboTheme);
                    list.CheckboxLabeled("USFM", ref enableUSFMTheme);
                }
                list.GapLine();
                GUI.color = Color.cyan;
                list.Label("RimTheme_SettingsDefaultThemesNote".Translate());
                GUI.color = Color.white;
            }

            if (SectionOpacityExpanded)
                GUI.color = Color.gray;
            else
                GUI.color = Color.green;

            if (list.ButtonText("RimTheme_SettingsAlphaSection".Translate()))
            {
                SectionOpacityExpanded = !SectionOpacityExpanded;
            }
            GUI.color = Color.white;

            if (SectionOpacityExpanded)
            {
                bool disabledOverrideThemeWindowFillColorAlphaPrev = disabledOverrideThemeWindowFillColorAlpha;
                list.CheckboxLabeled("RimTheme_SettingsDisableOverrideWindowFillColorAlphaLevel".Translate(), ref disabledOverrideThemeWindowFillColorAlpha);

                if (!disabledOverrideThemeWindowFillColorAlpha)
                {
                    list.Label("RimTheme_SettingsOverrideWindowFillColorAlphaLevel".Translate((int)(overrideThemeWindowFillColorAlphaLevelTmp * 100)));
                    overrideThemeWindowFillColorAlphaLevelTmp = list.Slider(overrideThemeWindowFillColorAlphaLevelTmp, 0.0f, 1.0f);

                    if (overrideThemeWindowFillColorAlphaLevelTmp != overrideThemeWindowFillColorAlphaLevel)
                        GUI.color = Color.red;
                    else
                        GUI.color = Color.gray;

                    if (list.ButtonText("Apply"))
                    {
                        overrideThemeWindowFillColorAlphaLevel = overrideThemeWindowFillColorAlphaLevelTmp;
                        Utils.applyWindowFillColorOpacityOverride(Settings.curTheme);
                    }
                    GUI.color = Color.white;
                    list.Gap(40);
                }
            }

            if (SectionRandomBgExpanded)
                GUI.color = Color.gray;
            else
                GUI.color = Color.green;

            if (list.ButtonText("RimTheme_SettingsRandomBackgroundSection".Translate()))
            {
                SectionRandomBgExpanded = !SectionRandomBgExpanded;
            }
            GUI.color = Color.white;

            if (SectionRandomBgExpanded)
            {
                list.CheckboxLabeled("RimTheme_SettingsDisableRandomBg".Translate(), ref disableRandomBg);
                list.CheckboxLabeled("RimTheme_SettingsKeepCurrentRandomBg".Translate(), ref keepCurrentBg);
                if (keepCurrentBg && disableRandomBg)
                    keepCurrentBg = false;
            }

            if (SectionDialogStackingExpanded)
                GUI.color = Color.gray;
            else
                GUI.color = Color.green;

            if (list.ButtonText("RimTheme_SettingsDialogStacking".Translate()))
            {
                SectionDialogStackingExpanded = !SectionDialogStackingExpanded;
            }
            GUI.color = Color.white;

            if (SectionDialogStackingExpanded)
            {
                if (list.RadioButton("RimTheme_SettingsDeterminedByTheme".Translate(), (dialogStacking == 1)))
                    dialogStacking = 1;
                if (list.RadioButton("RimTheme_SettingsDialogStackingEnabled".Translate(), (dialogStacking == 2)))
                    dialogStacking = 2;
                if (list.RadioButton("RimTheme_SettingsDialogStackingDisabled".Translate(), (dialogStacking == 3)))
                    dialogStacking = 3;
            }

            if (SectionMainMenuAlignmentExpanded)
                GUI.color = Color.gray;
            else
                GUI.color = Color.green;

            if (list.ButtonText("RimTheme_SettingsMenuAlignment".Translate()))
            {
                SectionMainMenuAlignmentExpanded = !SectionMainMenuAlignmentExpanded;
            }
            GUI.color = Color.white;

            if (SectionMainMenuAlignmentExpanded)
            {

                if (list.RadioButton("RimTheme_SettingsMenuAlignmentLeft".Translate(), (menuAlignment == 0)))
                    menuAlignment = 0;
                if (list.RadioButton("RimTheme_SettingsMenuAlignmentMiddle".Translate(), (menuAlignment == 1)))
                    menuAlignment = 1;
                if (list.RadioButton("RimTheme_SettingsMenuAlignmentRight".Translate(), (menuAlignment == 2)))
                    menuAlignment = 2;
                if (list.RadioButton("RimTheme_SettingsDeterminedByTheme".Translate(), (menuAlignment == 3)))
                    menuAlignment = 3;
            }

            if (SectionWindowAnimExpanded)
                GUI.color = Color.gray;
            else
                GUI.color = Color.green;

            if (list.ButtonText("RimTheme_SettingsWindowAnimation".Translate()))
            {
                SectionWindowAnimExpanded = !SectionWindowAnimExpanded;
            }
            GUI.color = Color.white;

            if (SectionWindowAnimExpanded)
            {

                if (list.RadioButton("RimTheme_SettingsWindowAnimationNone".Translate(), (windowAnimation == (int)WindowAnim.None)))
                    windowAnimation = (int)WindowAnim.None;
                if (list.RadioButton("RimTheme_SettingsDeterminedByTheme".Translate(), (windowAnimation == (int)WindowAnim.Theme)))
                    windowAnimation = (int)WindowAnim.Theme;
                if (list.RadioButton("RimTheme_SettingsWindowAnimationClip".Translate(), (windowAnimation == (int)WindowAnim.Clip)))
                    windowAnimation = (int)WindowAnim.Clip;
                if (list.RadioButton("RimTheme_SettingsWindowAnimationSlideLeft".Translate(), (windowAnimation == (int)WindowAnim.SlideLeft)))
                    windowAnimation = (int)WindowAnim.SlideLeft;
                if (list.RadioButton("RimTheme_SettingsWindowAnimationSlideRight".Translate(), (windowAnimation == (int)WindowAnim.SlideRight)))
                    windowAnimation = (int)WindowAnim.SlideRight;
                if (list.RadioButton("RimTheme_SettingsWindowAnimationSlideTop".Translate(), (windowAnimation == (int)WindowAnim.SlideTop)))
                    windowAnimation = (int)WindowAnim.SlideTop;
                if (list.RadioButton("RimTheme_SettingsWindowAnimationSlideBottom".Translate(), (windowAnimation == (int)WindowAnim.SlideBottom)))
                    windowAnimation = (int)WindowAnim.SlideBottom;
            }

            if (SectionRimThemesLogoExpanded)
                GUI.color = Color.gray;
            else
                GUI.color = Color.green;

            if (list.ButtonText("RimTheme_SettingsMenuRTLogo".Translate()))
            {
                SectionRimThemesLogoExpanded = !SectionRimThemesLogoExpanded;
            }
            GUI.color = Color.white;

            if (SectionRimThemesLogoExpanded)
            {

                if (list.RadioButton("RimTheme_SettingsMenuRTLogoShow".Translate(), (rimthemesLogoMode == 1)))
                    rimthemesLogoMode = 1;
                if (list.RadioButton("RimTheme_SettingsMenuRTLogoHide".Translate(), (rimthemesLogoMode == 2)))
                    rimthemesLogoMode = 2;
                if (list.RadioButton("RimTheme_SettingsDeterminedByTheme".Translate(), (rimthemesLogoMode == 3)))
                    rimthemesLogoMode = 3;
            }
            if (disableCustomCursorsPrev != disableCustomCursors)
            {
                CustomCursor.Deactivate();
                CustomCursor.Activate();
                disableCustomCursorsPrev = disableCustomCursors;
            }

            if (SectionWindowShadowExpanded)
                GUI.color = Color.gray;
            else
                GUI.color = Color.green;

            if (list.ButtonText("RimTheme_SettingsWindowsShadow".Translate()))
            {
                SectionWindowShadowExpanded = !SectionWindowShadowExpanded;
            }
            GUI.color = Color.white;

            if (SectionWindowShadowExpanded)
            {
                if (list.RadioButton("RimTheme_SettingsMenuRTLogoShow".Translate(), (windowsShadowMode == 1)))
                    windowsShadowMode = 1;
                if (list.RadioButton("RimTheme_SettingsMenuRTLogoHide".Translate(), (windowsShadowMode == 2)))
                    windowsShadowMode = 2;
                if (list.RadioButton("RimTheme_SettingsDeterminedByTheme".Translate(), (windowsShadowMode == 3)))
                    windowsShadowMode = 3;
            }

            if (SectionExtensionIconExpanded)
                GUI.color = Color.gray;
            else
                GUI.color = Color.green;

            if (list.ButtonText("RimTheme_SettingsExpansionsIcons".Translate()))
            {
                SectionExtensionIconExpanded = !SectionExtensionIconExpanded;
            }
            GUI.color = Color.white;

            if (SectionExtensionIconExpanded)
            {

                if (list.RadioButton("RimTheme_SettingsMenuRTLogoShow".Translate(), (expansionsIconsMode == 1)))
                    expansionsIconsMode = 1;
                if (list.RadioButton("RimTheme_SettingsMenuRTLogoHide".Translate(), (expansionsIconsMode == 2)))
                    expansionsIconsMode = 2;
                if (list.RadioButton("RimTheme_SettingsDeterminedByTheme".Translate(), (expansionsIconsMode == 3)))
                    expansionsIconsMode = 3;
            }

            if (SectionLudeonLogoExpanded)
                GUI.color = Color.gray;
            else
                GUI.color = Color.green;

            if (list.ButtonText("RimTheme_LudeonLogo".Translate()))
            {
                SectionLudeonLogoExpanded = !SectionLudeonLogoExpanded;
            }
            GUI.color = Color.white;

            if (SectionLudeonLogoExpanded)
            {

                if (list.RadioButton("RimTheme_SettingsMenuRTLogoShow".Translate(), (ludeonLogoMode == 1)))
                    ludeonLogoMode = 1;
                if (list.RadioButton("RimTheme_SettingsMenuRTLogoHide".Translate(), (ludeonLogoMode == 2)))
                    ludeonLogoMode = 2;
            }

            if (SectionInfosExpanded)
                GUI.color = Color.gray;
            else
                GUI.color = Color.green;

            if (list.ButtonText("RimTheme_InfoCorner".Translate()))
            {
                SectionInfosExpanded = !SectionInfosExpanded;
            }
            GUI.color = Color.white;

            if (SectionInfosExpanded)
            {
                if (list.RadioButton("RimTheme_SettingsMenuRTLogoShow".Translate(), (cornerInfoMode == 1)))
                    cornerInfoMode = 1;
                if (list.RadioButton("RimTheme_SettingsMenuRTLogoHide".Translate(), (cornerInfoMode == 2)))
                    cornerInfoMode = 2;
            }

            //Change in disable custom font
            if (disableCustomFontsPrev != disableCustomFonts)
            {
                Utils.resetCachedLabelWidthCache();
                disableCustomFontsPrev = disableCustomFonts;
            }

            //Change in the disable custom sounds
            if (disableCustomSoundsPrev != disableCustomSounds)
            {
                Themes.changeSoundTheme();
                disableCustomSoundsPrev = disableCustomSounds;
            }

            //Change in the disable custom songs
            if (disableCustomSongsPrev != disableCustomSongs)
            {
                Themes.changeSongTheme();
                disableCustomSongsPrev = disableCustomSongs;
            }

            //Change in disable random bg
            if (disableRandomBgPrev != disableRandomBg)
            {
                //Current video bg stop if applicable
                Themes.stopCurrentAnimatedBackground();
                Themes.setNewRandomBg();
                disableRandomBgPrev = disableRandomBg;
            }

            if(disableFontFilterModePointPrev != disableFontFilterModePoint)
            {
                try
                {
                    //Impact of the change on loaded fonts
                    foreach (var entry in Themes.DBGUIStyle)
                    {
                        try
                        {
                            foreach (var entry2 in entry.Value)
                            {
                                if (disableFontFilterModePoint)
                                {
                                    entry2.Value.font.material.mainTexture.filterMode = FilterMode.Trilinear;
                                }
                                else
                                {
                                    entry2.Value.font.material.mainTexture.filterMode = FilterMode.Point;
                                }
                            }
                        }
                        catch(Exception e)
                        {
                            Themes.LogError("Error while trying to change setting filterMode : " + e.Message);
                        }
                    }
                }
                catch(Exception e)
                {
                    Themes.LogError("Fatal error while trying to change setting filterMode : " + e.Message);
                }
                disableFontFilterModePointPrev = disableFontFilterModePoint;
            }

            if (disableTinyCustomFontPrev != disableTinyCustomFont)
            {
                try
                {
                    //Impact of the change on loaded fonts
                    foreach (var entry in Themes.DBGUIStyle)
                    {
                        try
                        {
                            if (disableTinyCustomFont)
                            {
                                Themes.DBGUIStyle[entry.Key][GameFont.Tiny].font = Text.fontStyles[0].font;
                                Themes.DBGUIStyle[entry.Key][GameFont.Tiny].fontSize = Text.fontStyles[0].fontSize;
                            }
                            else
                            {
                                //Si contient une police d'écriture petite on la définie
                                if (Themes.DBTinyFontByTheme.ContainsKey(entry.Key))
                                {
                                    Themes.DBGUIStyle[entry.Key][GameFont.Tiny].font = Themes.DBTinyFontByTheme[entry.Key];
                                    Themes.DBGUIStyle[entry.Key][GameFont.Tiny].fontSize = Themes.DBTinyFontSizeByTheme[entry.Key];

                                    //On applique la politique actuelle de filtrage des points
                                    if (disableFontFilterModePoint)
                                        Themes.DBGUIStyle[entry.Key][GameFont.Tiny].font.material.mainTexture.filterMode = FilterMode.Trilinear;
                                    else
                                        Themes.DBGUIStyle[entry.Key][GameFont.Tiny].font.material.mainTexture.filterMode = FilterMode.Point;
                                }
                                else
                                {
                                    Themes.DBGUIStyle[entry.Key][GameFont.Tiny].font = Text.fontStyles[0].font;
                                    Themes.DBGUIStyle[entry.Key][GameFont.Tiny].fontSize = Text.fontStyles[0].fontSize;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Themes.LogError("Error while trying to toggle tinyFont : " + e.Message);
                        }
                    }
                }
                catch (Exception e)
                {
                    Themes.LogError("Fatal error while trying to change setting tinyFont toggling : " + e.Message);
                }
                Utils.resetCachedLabelWidthCache();
                disableTinyCustomFontPrev = disableTinyCustomFont;
            }

            list.End();
            Widgets.EndScrollView();
            //settings.Write();
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<bool>(ref disabledOverrideThemeWindowFillColorAlpha, "disabledOverrideThemeWindowFillColorAlpha", true);
            Scribe_Values.Look<float>(ref overrideThemeWindowFillColorAlphaLevel, "overrideThemeWindowFillColorAlpha", 0.75f);


            Scribe_Values.Look<bool>(ref disableDefaultThemes, "disableDefaultThemes", false);
            Scribe_Values.Look<int>(ref cornerInfoMode, "cornerInfoMode", 1);
            Scribe_Values.Look<int>(ref ludeonLogoMode, "ludeonLogoMode", 1);
            Scribe_Values.Look<int>(ref rimthemesLogoMode, "rimthemesLogoMode", 3);
            Scribe_Values.Look<int>(ref windowsShadowMode, "windowsShadowMode", 3);
            Scribe_Values.Look<int>(ref expansionsIconsMode, "expansionsIconsMode", 3);
            Scribe_Values.Look<string>(ref curTheme, "curTheme", "-1§Vanilla");
            Scribe_Values.Look<bool>(ref disableWallpaper, "disableWallpaper", false);
            Scribe_Values.Look<bool>(ref disableCustomFonts, "disableCustomFonts", false);
            Scribe_Values.Look<bool>(ref disableCustomFontsInConsole, "disableCustomFontsInConsole", true);
            Scribe_Values.Look<bool>(ref disableCustomCursors, "disableCustomCursors", false);
            Scribe_Values.Look<bool>(ref disableParticle, "disableParticle", true);
            Scribe_Values.Look<bool>(ref modManagerAsOptionList, "modManagerAsOptionList", false);
            Scribe_Values.Look<bool>(ref disableCustomLoader, "disableCustomLoader", false);
            Scribe_Values.Look<bool>(ref disableCustomSounds, "disableCustomSounds", false);
            Scribe_Values.Look<bool>(ref disableCustomSongs, "disableCustomSongs", false);
            Scribe_Values.Look<int>(ref dialogStacking, "dialogStacking", 1);
            Scribe_Values.Look<bool>(ref disableButtonBG, "disableButtonBG", false);
            Scribe_Values.Look<bool>(ref disableTapestry, "disableTapestry2", false);            
            Scribe_Values.Look<int>(ref menuAlignment, "menuAlignment", 3);
            Scribe_Values.Look<int>(ref windowAnimation, "windowAnimation", (int)WindowAnim.Theme);
            Scribe_Values.Look<bool>(ref hideLoadingText, "hideLoadingText", false);
            Scribe_Values.Look<bool>(ref verboseMode, "verboseMode", false);
            Scribe_Values.Look<bool>(ref disableVideoBg, "disableVideoBg", true);
            Scribe_Values.Look<bool>(ref disableRandomBg, "disableRandomBg", true);
            Scribe_Values.Look<string>(ref curRandomBg, "curRandomBg",null);
            Scribe_Values.Look<bool>(ref keepCurrentBg, "keepCurrentBg", false);
            Scribe_Values.Look<bool>(ref disableFontFilterModePoint, "disableFontFilterModePoint", true);
            Scribe_Values.Look<string>(ref lastVersionInfo, "lastVersionInfo", "");
            Scribe_Values.Look<bool>(ref disableTinyCustomFont, "disableTinyCustomFont", false);

            Scribe_Values.Look<bool>(ref enableCentipedeTheme, "enableCentipedeTheme", true);
            Scribe_Values.Look<bool>(ref enableClassicCassandraTheme, "enableClassicCassandraTheme", true);
            Scribe_Values.Look<bool>(ref enableCyberpunkTheme, "enableCyberpunkTheme", true);
            Scribe_Values.Look<bool>(ref enableMechanoidClusterTheme, "enableMechanoidClusterTheme", true);
            Scribe_Values.Look<bool>(ref enableMuffaloTheme, "enableMuffaloTheme", true);
            Scribe_Values.Look<bool>(ref enablePhoebeChillaxTheme, "enablePhoebeChillaxTheme", true);
            Scribe_Values.Look<bool>(ref enableRimLife2Theme, "enableRimLife2Theme", true);
            Scribe_Values.Look<bool>(ref enableScytherTheme, "enableScytherTheme", true);
            Scribe_Values.Look<bool>(ref enableSingularityTheme, "enableSingularityTheme", true);
            Scribe_Values.Look<bool>(ref enableThrumboTheme, "enableThrumboTheme", true);
            Scribe_Values.Look<bool>(ref enableUSFMTheme, "enableUSFMTheme", true);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                //Used to track changes in parameters for certain variables
                disableTinyCustomFontPrev = disableTinyCustomFont;
                disableCustomFontsPrev = disableCustomFonts;
                disableCustomCursorsPrev = disableCustomCursors;
                disableCustomSoundsPrev = disableCustomSounds;
                disableCustomSongsPrev = disableCustomSongs;
                disableRandomBgPrev = disableRandomBg;
                disableFontFilterModePointPrev = disableFontFilterModePoint;

                overrideThemeWindowFillColorAlphaLevelTmp = overrideThemeWindowFillColorAlphaLevel;
    }
        }
    }
}