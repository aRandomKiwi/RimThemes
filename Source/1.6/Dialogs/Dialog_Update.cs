using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using Verse;
using RimWorld;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Collections;

namespace aRandomKiwi.RimThemes
{
    public class Dialog_Update : Window
    {
        protected Dictionary<string, bool> UDBExpanded = new Dictionary<string, bool>();
        protected OrderedDictionary UDB = new OrderedDictionary();
        protected float bottomAreaHeight;
        protected Vector2 scrollPosition = Vector2.zero;

        public override Vector2 InitialSize
        {
            get
            {
                return new Vector2(620f, 700f);
            }
        }

        public Dialog_Update()
        {
            this.doCloseButton = true;
            this.doCloseX = true;
            this.forcePause = true;
            this.absorbInputAroundWindow = true;
            this.closeOnAccept = false;

            UDB.Add("NX rev10", "-Added the ability to disable all defaults themes (or specific ones), allowing to significantly reduce RimThemes memory footprint" + Environment.NewLine + Environment.NewLine
            + "-Only 3 default themes are enabled by default (RimThemes memory consumption reduced by 40% on average)" + Environment.NewLine + Environment.NewLine
            + "-Added the ability to have multiple random game textures" + Environment.NewLine + Environment.NewLine
            + "-Improved settings menu accessibility UI" + Environment.NewLine + Environment.NewLine
            + "-Added the ability to have multiple random images/videos in the loading screen" + Environment.NewLine + Environment.NewLine
            + "-Added the ability to have multiple random main menu songs" + Environment.NewLine + Environment.NewLine
            + "-Loader can now display video background" + Environment.NewLine + Environment.NewLine
            + "-Loader now display tips" + Environment.NewLine + Environment.NewLine
            + "-Added the ability for themes to hide the translations infos dialog (determined by the theme by default)" + Environment.NewLine + Environment.NewLine
            + "-Added new setting 'Accessibility' making certain elements of the interface more visible ingame (particularly in winter, etc.)" + Environment.NewLine + Environment.NewLine
            + "-Improved readability of default themes (especially regarding transparency issues)." + Environment.NewLine + Environment.NewLine
            + "-Updates windows is now accessible via the settings menu, now with the change history." + Environment.NewLine + Environment.NewLine
            + "-Fix opacity override setting not working properly" + Environment.NewLine + Environment.NewLine + Environment.NewLine
            + "-Fix Themes without EntrySong generating error on theme change action" + Environment.NewLine + Environment.NewLine + Environment.NewLine
            + "-Fix the loading screen sometimes does not fit the screen" + Environment.NewLine + Environment.NewLine + Environment.NewLine
            + "-Fix random background setting now work correctly" + Environment.NewLine + Environment.NewLine + Environment.NewLine
            + "Note : For Themers the 'Example Theme' has been updated, check the 'Themes Makers' thread on Steam." + Environment.NewLine);

            UDB.Add("NX rev6", "-Fix BetterLoading mod incompatibilities" + Environment.NewLine);

            UDB.Add("NX rev5", "-Switch video rendering engine from MovieTextures to VideoPlayer" + Environment.NewLine + Environment.NewLine
            + "- Change video format from OGV to WEBM(only affect 1.3 Rimthemes instance)" + Environment.NewLine);

            UDB.Add("NX rev0", "-Added the name of the current main donor in the themes selection menu" + Environment.NewLine + Environment.NewLine
            + "-Improved all default themes" + Environment.NewLine + Environment.NewLine
            + "-Added new setting allowing to adjust all windows opacity level" + Environment.NewLine + Environment.NewLine
            + "-Added new setting allowing to hide the RimThemes logo in the main menu" + Environment.NewLine + Environment.NewLine
            + "-Added new settings allowing to hide the main menu expansions icons, info corner and more" + Environment.NewLine + Environment.NewLine
            + "-Added new setting allowing to hide windows shadows" + Environment.NewLine + Environment.NewLine
            + "-Added new default theme 'Rim-Life 2' and 'Mechanoid cluster'" + Environment.NewLine + Environment.NewLine
            + "-Fixed the overlapping issue with the expansions icons buttons (in the bottom left)" + Environment.NewLine + Environment.NewLine
            + "-Fixed confirm button texture issue (vanilla texture applied instead of the current theme)" + Environment.NewLine + Environment.NewLine
            + "-Few others minors improvements" + Environment.NewLine + Environment.NewLine
            + "For themes makers :" + Environment.NewLine + Environment.NewLine
            + "-Fixed tapestry border color tag bug (color was never applied in themes)" + Environment.NewLine + Environment.NewLine
            + "-Added support for custom APNG loader FPS with the new tag 'loaderFPS'" + Environment.NewLine + Environment.NewLine
            + "-Few others new tags (download the Theme example package for more details)" + Environment.NewLine + Environment.NewLine
            + "/!\\ Notice : Support for 1.0 is dropped, only RimThemes 2020R1 is compatible with Rimworld 1.0." + Environment.NewLine);

            UDB.Add("2020 R1", "-Kept only popular themes as RimThemes is a themes engine and provide themes only to show the capacity of the engine." + Environment.NewLine + Environment.NewLine
            + "By default particles on buttons are disabled (can be changed in mod's settings)." + Environment.NewLine + Environment.NewLine
            + "-Add compatibility with BetterLoading mod (RimThemes is no longer broken when his loader is disabled in the mod settings and BetterLoading is loaded)." + Environment.NewLine + Environment.NewLine
            + "-Others themes are still available (see how to get them in the mod's steam workshop description) " + Environment.NewLine + Environment.NewLine
            + "-Themers : Add the ability to modify all Rimworld custom colors like the red 'confirm' button,... (see the new dynColor tag in the 'Theme Example')" + Environment.NewLine);

            bool s = true;
            foreach(DictionaryEntry el in UDB)
            {
                UDBExpanded[el.Key.ToString()] = s;
                if (s)
                    s = false;
            }
        }

        public override void DoWindowContents(Rect inRect)
        {
            //inRect.yMin += 15f;
            //inRect.yMax -= 15f;

            var defaultColumnWidth = (inRect.width - 25);
            Listing_Standard list = new Listing_Standard() { ColumnWidth = defaultColumnWidth };

            //Image logo
            Widgets.ButtonImage(new Rect((inRect.width / 2) - 90, inRect.y, 180, 144), Loader.rtUpdateIconTex, Color.white, Color.white);

            var outRect = new Rect(inRect.x, inRect.y + 150, inRect.width, inRect.height-150);
            var scrollRect = new Rect(0f, 0f, inRect.width - 16f, inRect.height*UDBExpanded.Count());

            outRect.height -= (this.bottomAreaHeight + 50);
            Widgets.BeginScrollView(outRect, ref scrollPosition, scrollRect, true);

            list.Begin(scrollRect);

            foreach(DictionaryEntry el in UDB)
            {
                if (UDBExpanded[el.Key.ToString()])
                    GUI.color = Color.gray;
                else
                    GUI.color = Color.green;

                if (list.ButtonText(el.Key.ToString()))
                {
                    UDBExpanded[el.Key.ToString()] = !UDBExpanded[el.Key.ToString()];
                }
                GUI.color = Color.white;

                if (UDBExpanded[el.Key.ToString()])
                {
                    list.Label((string)el.Value);
                }
            }

            list.End();
            Widgets.EndScrollView();
        }
    }
}