using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    public static bool isDev = false;
    public static bool isBeta = false;
    public static bool isTestingNeow = false;
    public static bool isModded = false;
    public static bool isDemo = false;
    public static bool isShowBuild = false;
    public static bool isPublisherBuild = false;
    public static bool isDebug = false;
    public static bool isInfo = false;
    public static bool isControllerMode = false;
    public static GameLanguage language;
    public static readonly string PARAM_CHAR_LOC = "paramChars.txt";
    public static bool lineBreakViaCharacter = false;
    public static bool usesOrdinal = true;
    public static bool isDailyRun;
    public static bool hasDoneDailyToday;
    public static long dailyDate = 0L;
    public static long totalPlayTime;
    public static bool isreadonlyActAvailable;
    public static bool hasRubyKey;
    public static bool hasEmeraldKey;
    public static bool hasSapphireKey;
    public static bool isEndless;
    public static bool isTrial;
    public static long specialSeed;
    public static string trialName;
    public static bool IS_FULLSCREEN;
    public static bool IS_W_FULLSCREEN;
    public static bool IS_V_SYNC;
    public static int MAX_FPS;
    public static int M_W;
    public static int M_H;
    public static int SAVED_WIDTH;
    public static int SAVED_HEIGHT;
    public static int WIDTH;
    public static int HEIGHT;
    public static bool isSixteenByTen = false;
    public static int HORIZ_LETTERBOX_AMT = 0;
    public static int VERT_LETTERBOX_AMT = 0;
    public static int displayIndex = 0;
    public static float scale;
    public static long seed;
    public static bool seedSet = false;
    public static long seedSourceTimestamp;
    public static bool isBackgrounded = false;
    public static float bgVolume = 0.0F;
    public static readonly string MASTER_VOLUME_PREF = "Master Volume";
    public static readonly string MUSIC_VOLUME_PREF = "Music Volume";
    public static readonly string SOUND_VOLUME_PREF = "Sound Volume";
    public static readonly string AMBIENCE_ON_PREF = "Ambience On";
    public static readonly string MUTE_IF_BG_PREF = "Mute in Bg";
    public static readonly float DEFAULT_MASTER_VOLUME = 0.5F;
    public static readonly float DEFAULT_MUSIC_VOLUME = 0.5F;
    public static readonly float DEFAULT_SOUND_VOLUME = 0.5F;
    public static float MASTER_VOLUME;
    public static float MUSIC_VOLUME;
    public static float SOUND_VOLUME;
    public static bool AMBIANCE_ON;
    public static readonly string SCREEN_SHAKE_PREF = "Screen Shake";
    public static readonly string SUM_DMG_PREF = "Summed Damage";
    public static readonly string BLOCKED_DMG_PREF = "Blocked Damage";
    public static readonly string HAND_CONF_PREF = "Hand Confirmation";
    public static readonly string EFFECTS_PREF = "Particle Effects";
    public static readonly string FAST_MODE_PREF = "Fast Mode";
    public static readonly string UPLOAD_PREF = "Upload Data";
    public static readonly string PLAYTESTER_ART = "Playtester Art";
    public static readonly string SHOW_CARD_HOTKEYS_PREF = "Show Card keys";
    public static readonly string CONTROLLER_ENABLED_PREF = "Controller Enabled";
    public static readonly string LAST_DAILY = "LAST_DAILY";
    public static bool SHOW_DMG_SUM;
    public static bool SHOW_DMG_BLOCK;
    public static bool FAST_HAND_CONF;
    public static bool FAST_MODE;
    public static bool CONTROLLER_ENABLED;
    public static bool DISABLE_EFFECTS;
    public static bool UPLOAD_DATA;
    public static bool SCREEN_SHAKE;
    public static bool PLAYTESTER_ART_MODE;
    public static bool SHOW_CARD_HOTKEYS;
    public static readonly float POST_ATTACK_WAIT_DUR = 0.1F;
    public static readonly float WAIT_BEFORE_BATTLE_TIME = 1.0F;
    public static float ACTION_DUR_XFAST = 0.1F;
    public static float ACTION_DUR_FASTER = 0.2F;
    public static float ACTION_DUR_FAST = 0.25F;
    public static float ACTION_DUR_MED = 0.5F;
    public static float ACTION_DUR_long = 1.0F;
    public static float ACTION_DUR_Xlong = 1.5F;
    public static float CARD_DROP_END_Y;
    public static float SCROLL_SPEED;
    public static float MAP_SCROLL_SPEED;
    public static readonly float SCROLL_LERP_SPEED = 12.0F;
    public static readonly float SCROLL_SNAP_BACK_SPEED = 10.0F;
    public static float DEFAULT_SCROLL_LIMIT;
    public static float MAP_DST_Y;
    public static readonly float CLICK_SPEED_THRESHOLD = 0.4F;
    public static float CLICK_DIST_THRESHOLD;
    public static float POTION_W;
    public static float POTION_Y;
    public static readonly Color BLACK_SCREEN_OVERLAY_COLOR = new Color(0.0F, 0.0F, 0.0F, 0.7F);
    public static readonly Color SHADOW_COLOR = new Color(0.0F, 0.0F, 0.0F, 0.5F);
    public static readonly float CARD_SOUL_SCALE = 0.12F;
    public static readonly float CARD_LERP_SPEED = 6.0F;
    public static float CARD_SNAP_THRESHOLD;
    public static float UI_SNAP_THRESHOLD;
    public static readonly float CARD_SCALE_LERP_SPEED = 7.5F;
    public static readonly float CARD_SCALE_SNAP_THRESHOLD = 0.003F;
    public static readonly float UI_LERP_SPEED = 9.0F;
    public static readonly float ORB_LERP_SPEED = 6.0F;
    public static readonly float MOUSE_LERP_SPEED = 20.0F;
    public static float POP_AMOUNT;
    public static readonly float POP_LERP_SPEED = 8.0F;
    public static readonly float FADE_LERP_SPEED = 12.0F;
    public static readonly float SLOW_COLOR_LERP_SPEED = 3.0F;
    public static readonly float FADE_SNAP_THRESHOLD = 0.01F;
    public static readonly float ROTATE_LERP_SPEED = 12.0F;
    public static readonly float SCALE_LERP_SPEED = 3.0F;
    public static readonly float SCALE_SNAP_THRESHOLD = 0.003F;
    public static readonly float HEALTH_BAR_WAIT_TIME = 1.5F;
    public static float HOVER_BUTTON_RISE_AMOUNT;
    public static readonly float HOVER_BUTTON_SCALE_AMOUNT = 1.2F;
    public static readonly float CARD_VIEW_SCALE = 0.75F;
    public static float CARD_VIEW_PAD_X;
    public static float CARD_VIEW_PAD_Y;
    public static float OPTION_Y;
    public static float EVENT_Y;
    public static readonly int MAX_ASCENSION_LEVEL = 20;
    public static readonly float POST_COMBAT_WAIT_TIME = 0.25F;
    public static readonly int MAX_HAND_SIZE = 10;
    public static readonly int NUM_POTIONS = 3;
    public static readonly int NORMAL_POTION_DROP_RATE = 40;
    public static readonly int ELITE_POTION_DROP_RATE = 40;
    public static readonly int BOSS_GOLD_AMT = 100;
    public static readonly int BOSS_GOLD_JITTER = 5;
    public static readonly int NORMAL_RARE_DROP_RATE = 3;
    public static readonly int NORMAL_UNCOMMON_DROP_RATE = 40;
    public static readonly int ELITE_RARE_DROP_RATE = 10;
    public static readonly int ELITE_UNCOMMON_DROP_RATE = 50;
    public static readonly int UNLOCK_PER_CHAR_COUNT = 5;
    public static bool hideTopBar = false;
    public static bool hidePopupDetails = false;
    public static bool hideRelics = false;
    public static bool hideLowerElements = false;
    public static bool hideCards = false;
    public static bool hideEndTurn = false;
    public static bool hideCombatElements = false;
    public static readonly string SENDTODEVS = "sendToDevs";

    public enum GameLanguage
    {
        ENG, EPO, PTB, ZHS, ZHT, FRA, DEU, GRE, IND, ITA, JPN, KOR, NOR, POL, RUS, SPA, SRP, SRB, THA, TUR, UKR, WWW
    }

    public static void initialize(bool reloaded)
    {
        if (!reloaded)
        {
            WIDTH = (int)GlobalManager.Instance.CanvasScaler.referenceResolution.x;
            HEIGHT = (int)GlobalManager.Instance.CanvasScaler.referenceResolution.y;

            float ratio = WIDTH / HEIGHT;
            if (ratio < 1.59F)
            {
                HEIGHT = (int)(WIDTH * 0.625F);
                HORIZ_LETTERBOX_AMT = (M_H - HEIGHT) / 2;
            }
            else if (ratio > 1.78F)
            {
                WIDTH = (int)(HEIGHT * 1.7777778F);
                VERT_LETTERBOX_AMT = (M_W - WIDTH) / 2;
            }
            SAVED_WIDTH = WIDTH;
            SAVED_HEIGHT = HEIGHT;
            if (WIDTH / HEIGHT < 1.7F)
            {
                isSixteenByTen = true;
            }
            scale = WIDTH / 1920.0F;
            SCROLL_SPEED = 75.0F * scale;
            MAP_SCROLL_SPEED = 75.0F * scale;
            DEFAULT_SCROLL_LIMIT = 50.0F * scale;
            MAP_DST_Y = 150.0F * scale;
            CLICK_DIST_THRESHOLD = 30.0F * scale;
            CARD_DROP_END_Y = HEIGHT * 0.81F;
            POTION_W = 56.0F * scale;
            POTION_Y = HEIGHT - 30.0F * scale;
            OPTION_Y = HEIGHT / 2.0F - 32.0F * scale;
            EVENT_Y = HEIGHT / 2.0F - 128.0F * scale;
            CARD_VIEW_PAD_X = 40.0F * scale;
            CARD_VIEW_PAD_Y = 40.0F * scale;
            HOVER_BUTTON_RISE_AMOUNT = 8.0F * scale;
            POP_AMOUNT = 1.75F * scale;
            CARD_SNAP_THRESHOLD = 1.0F * scale;
            UI_SNAP_THRESHOLD = 1.0F * scale;
        }
    }


    public static void setLanguageLegacy(string key, bool initial)
    {
        switch (key)
        {
            case "English":
                language = GameLanguage.ENG;
                if (initial)
                {
                    lineBreakViaCharacter = false;
                    usesOrdinal = true;
                }
                break;
            case "Brazilian Portuguese":
                language = GameLanguage.PTB;
                if (initial)
                {
                    lineBreakViaCharacter = false;
                    usesOrdinal = false;
                }
                break;
            case "Chinese (Simplified)":
                language = GameLanguage.ZHS;
                if (initial)
                {
                    lineBreakViaCharacter = true;
                    usesOrdinal = false;
                }
                break;
            case "Chinese (Traditional)":
                language = GameLanguage.ZHT;
                if (initial)
                {
                    lineBreakViaCharacter = true;
                    usesOrdinal = false;
                }
                break;
            case "French":
                language = GameLanguage.FRA;
                if (initial)
                {
                    lineBreakViaCharacter = false;
                    usesOrdinal = false;
                }
                break;
            case "German":
                language = GameLanguage.DEU;
                if (initial)
                {
                    lineBreakViaCharacter = false;
                    usesOrdinal = false;
                }
                break;
            case "Greek":
                language = GameLanguage.GRE;
                if (initial)
                {
                    lineBreakViaCharacter = false;
                    usesOrdinal = false;
                }
                break;
            case "Italian":
                language = GameLanguage.ITA;
                if (initial)
                {
                    lineBreakViaCharacter = false;
                    usesOrdinal = false;
                }
                break;
            case "Indonesian":
                language = GameLanguage.ITA;
                if (initial)
                {
                    lineBreakViaCharacter = false;
                    usesOrdinal = false;
                }
                break;
            case "Japanese":
                language = GameLanguage.JPN;
                if (initial)
                {
                    lineBreakViaCharacter = true;
                    usesOrdinal = false;
                }
                break;
            case "Korean":
                language = GameLanguage.KOR;
                if (initial)
                {
                    lineBreakViaCharacter = false;
                    usesOrdinal = false;
                }
                break;
            case "Norwegian":
                language = GameLanguage.NOR;
                if (initial)
                {
                    lineBreakViaCharacter = false;
                    usesOrdinal = false;
                }
                break;
            case "Polish":
                language = GameLanguage.POL;
                if (initial)
                {
                    lineBreakViaCharacter = false;
                    usesOrdinal = false;
                }
                break;
            case "Russian":
                language = GameLanguage.RUS;
                if (initial)
                {
                    lineBreakViaCharacter = false;
                    usesOrdinal = false;
                }
                break;
            case "Spanish":
                language = GameLanguage.SPA;
                if (initial)
                {
                    lineBreakViaCharacter = false;
                    usesOrdinal = false;
                }
                break;
            case "Serbian-Cyrillic":
                language = GameLanguage.SRP;
                if (initial)
                {
                    lineBreakViaCharacter = false;
                    usesOrdinal = false;
                }
                break;
            case "Serbian-Latin":
                language = GameLanguage.SRB;
                if (initial)
                {
                    lineBreakViaCharacter = false;
                    usesOrdinal = false;
                }
                break;
            case "Thai":
                language = GameLanguage.THA;
                if (initial)
                {
                    lineBreakViaCharacter = false;
                    usesOrdinal = false;
                }
                break;
            case "Turkish":
                language = GameLanguage.TUR;
                if (initial)
                {
                    lineBreakViaCharacter = false;
                    usesOrdinal = false;
                }
                break;
            case "Ukrainian":
                language = GameLanguage.UKR;
                if (initial)
                {
                    lineBreakViaCharacter = false;
                    usesOrdinal = false;
                }
                break;
            default:
                language = GameLanguage.ENG;
                if (initial)
                {
                    lineBreakViaCharacter = false;
                    usesOrdinal = true;
                }
                break;
        }
    }

    public static bool isStandardRun()
    {
        return (!isDailyRun) && (!isTrial) && (!seedSet);
    }

    public static bool treatEverythingAsUnlocked()
    {
        return (isDailyRun) || (isTrial);
    }
}