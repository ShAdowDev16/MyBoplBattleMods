using BepInEx;
using UnityEngine;
using BoplFixedMath;
using Steamworks;
using UnityEngine.Networking;
using JetBrains.Annotations;
using HarmonyLib;
using System;
using System.Reflection;

namespace BoplBattlePluginCool
{
    [HarmonyPatch]
    public class Patches
    {

        [HarmonyPostfix]
        [HarmonyPatch(typeof(InstantAbility), nameof(InstantAbility.GetCooldown))]
        private static void ChangeCooldownLength_Instant(ref Fix __result)
        {
            if (Plugin.ShortenCooldowns)
                __result = Fix.Zero;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Ability), nameof(Ability.GetCooldown))]
        private static void ChangeCooldownLength(ref Fix __result)
        {
            if (Plugin.ShortenCooldowns)
                __result = Fix.Zero;
        }


        public static void PlayerSpeedChangeThing(PlayerPhysics __instance)
        {
            __instance.Speed = (Fix)Plugin.SpeedSliderValue;


        }

        public static void InvisibleThing(Player __instance)
        {
            

        }


    }


    [BepInPlugin("com.shadow_dev.BoplPanel", "BoplPanel", "1.2.0")]//Unique name used by BepInEX, the name of your plugin, the version of your plugin.
    [BepInProcess("BoplBattle.exe")]//Makes the plugin only work on "BoplBattle.exe".
    public class Plugin : BaseUnityPlugin
    {
        public static bool ShortenCooldowns;

        public bool IsInvisible;

        public static int NewCooldownLenght;
        private void Awake()
        {
            ShortenCooldowns = false;
            IsInvisible = false;


            Harmony harmony = new("com.shadow_dev.BoplPanel");
            MethodInfo original = AccessTools.Method(typeof(PlayerPhysics), "UpdateSim");
            MethodInfo patch = AccessTools.Method(typeof(Patches), "PlayerSpeedChangeThing");
            harmony.Patch(original, new HarmonyMethod(patch));





            harmony.PatchAll();

            // Plugin startup logic
            Logger.LogInfo($"Plugin shadow_dev.BoplPanel is loaded!");
        }

        public string GUIName = "BoplPanel";
        private Color guiColor = Color.black;
        private float colorTimer = 0f;
        private Rect windowRect = new Rect(60, 20, 500, 500);//Size of GUI
        public string GUIText = "Enable Panel";
        void Update()//all RGB stuff
        {
            float r = Mathf.Abs(Mathf.Sin(colorTimer * 0.4f));
            float g = Mathf.Abs(Mathf.Sin(colorTimer * 0.5f));
            float b = Mathf.Abs(Mathf.Sin(colorTimer * 0.6f));
            guiColor = new Color(r, g, b);
            colorTimer += Time.deltaTime;

            DisplaySpeedValue = SpeedSliderValue.ToString();





        }
        public void Start()
        {
            ColorUtility.TryParseHtmlString("#ffff00", out textColor);//uses hex codes
            ColorUtility.TryParseHtmlString("#00ccff", out buttonColor);
        }
        public bool youturnnedmeon = false;

        void OnGUI()//Dont really mess with dis
        {
            GUI.backgroundColor = guiColor;
            GUI.color = guiColor;
            if (GUI.Button(new Rect(20, 20, 100, 20), GUIText))
            {
                if (youturnnedmeon == false)
                {
                    GUIText = "Disable Panel";
                    youturnnedmeon = true;
                }
                else
                {
                    GUIText = "Enable Panel";
                    youturnnedmeon = false;
                }
            }
            if (youturnnedmeon)
            {
                windowRect = GUI.Window(10000, windowRect, MainGUI, GUIName);//opens GUI
            }
        }
        public int PageNum;
        public Color textColor;
        public Color buttonColor;
        public string InputText = "";
        public string Credits = "By Shadow";
        public static float SpeedSliderValue = 19f;
        public string DisplaySpeedValue = SpeedSliderValue.ToString();


        public static bool Toggle1;
        public static bool Toggle2;

        void MainGUI(int windowID)
        {
            GUI.contentColor = textColor;//sets text color
            GUI.backgroundColor = buttonColor;//sets button color
            //GUI.Color <- that sets every color
            int PageNumlol = PageNum + 1;//the real page number
            GUI.Label(new Rect(230, 20, 200, 20), "Page: " + PageNumlol);//page label
            switch (PageNum)
            {
                case 0://Page 1//

                    GUI.Label(new Rect(200, 400, 150, 20), Credits);

                    //Collum 1//



                    InputText = GUI.TextArea(new Rect(20, 50, 100, 20), InputText);


                    Toggle2 = GUI.Toggle(new Rect(20, 80, 100, 20), Toggle2, "PlaceHolder");
                    if (Toggle2)
                    {
                        IsInvisible = true;
                    }
                    else
                    {
                        IsInvisible = false;
                    }


                    if (GUI.Button(new Rect(20, 110, 100, 20), "PlaceHolder"))
                    {

                    }






                    //Collum 2//
                    Toggle1 = GUI.Toggle(new Rect(200, 50, 100, 20), Toggle1, "No Cooldown");
                    if (Toggle1)
                    {
                        ShortenCooldowns = true;
                    }
                    else
                    {
                        ShortenCooldowns = false;
                    }


                    if (GUI.Button(new Rect(200, 80, 100, 20), "Invite Friends"))
                    {
                        SteamFriends.OpenOverlay("friends");
                    }

                    SpeedSliderValue = GUI.HorizontalSlider(new Rect(200, 110, 100, 20), SpeedSliderValue, 0.0F, 50.0F);
                    GUI.Label(new Rect(200, 120, 150, 20), "Movement Speed");

                    GUI.Label(new Rect(200, 150, 150, 20), DisplaySpeedValue);




                    //Collum 3//
                    if (GUI.Button(new Rect(390, 50, 100, 20), "Placeholder"))
                    {
                        
                    }

                    if (GUI.Button(new Rect(390, 80, 100, 20), "PlaceHolder"))
                    {

                    }

                    if (GUI.Button(new Rect(390, 110, 100, 20), "PlaceHolder"))
                    {

                    }



                    if (GUI.Button(new Rect(0, 450, 500, 20), ">>>>>>>"))//forward
                    {
                        PageNum++;
                    }
                    break;
                case 1://Page 2//






                    //Collum 1//
                    if (GUI.Button(new Rect(20, 50, 100, 20), "PlaceHolder"))
                    {

                    }
                    if (GUI.Button(new Rect(20, 80, 100, 20), "PlaceHolder"))
                    {

                    }

                    if (GUI.Button(new Rect(20, 110, 100, 20), "PlaceHolder"))
                    {

                    }






                    //Collum 2//
                    if (GUI.Button(new Rect(200, 50, 100, 20), "PlaceHolder"))
                    {

                    }

                    if (GUI.Button(new Rect(200, 80, 100, 20), "PlaceHolder"))
                    {

                    }

                    if (GUI.Button(new Rect(200, 110, 100, 20), "PlaceHolder"))
                    {

                    }




                    //Collum 3//
                    if (GUI.Button(new Rect(390, 50, 100, 20), "PlaceHolder"))
                    {

                    }

                    if (GUI.Button(new Rect(390, 80, 100, 20), "PlaceHolder"))
                    {

                    }

                    if (GUI.Button(new Rect(390, 110, 100, 20), "PlaceHolder"))
                    {

                    }




                    if (GUI.Button(new Rect(0, 450, 500, 20), ">>>>>>>"))//forward
                    {
                        PageNum++;
                    }
                    if (GUI.Button(new Rect(0, 470, 500, 20), "<<<<<<<"))//Backward
                    {
                        PageNum--;
                    }
                    break;
                case 2://Page 3//





                    //Collum 1//
                    if (GUI.Button(new Rect(20, 50, 100, 20), "PlaceHolder"))
                    {

                    }
                    if (GUI.Button(new Rect(20, 80, 100, 20), "PlaceHolder"))
                    {

                    }

                    if (GUI.Button(new Rect(20, 110, 100, 20), "PlaceHolder"))
                    {

                    }






                    //Collum 2//
                    if (GUI.Button(new Rect(200, 50, 100, 20), "PlaceHolder"))
                    {

                    }

                    if (GUI.Button(new Rect(200, 80, 100, 20), "PlaceHolder"))
                    {

                    }

                    if (GUI.Button(new Rect(200, 110, 100, 20), "PlaceHolder"))
                    {

                    }




                    //Collum 3//
                    if (GUI.Button(new Rect(390, 50, 100, 20), "PlaceHolder"))
                    {

                    }

                    if (GUI.Button(new Rect(390, 80, 100, 20), "PlaceHolder"))
                    {

                    }

                    if (GUI.Button(new Rect(390, 110, 100, 20), "PlaceHolder"))
                    {

                    }




                    if (GUI.Button(new Rect(0, 470, 500, 20), "<<<<<<<"))//Backward
                    {
                        PageNum--;
                    }

                    break;

            }
            GUI.DragWindow();
        }
    }
}
