using UnityEngine;

public static class GameMathHelper
{
    public static float CARD_SNAP_THRESHOLD = 0.03f;

    public static bool CloseToZero(Vector3 val)
    {
        return Mathf.Abs(val.x) < 0.3f &&
            Mathf.Abs(val.y) < 0.3f &&
            Mathf.Abs(val.z) < 0.3f;
    }

    public static float CardPositionLerpSnap(float start, float target, float deltaTime)
    {
        if (start != target)
        {
            start = Mathf.Lerp(start, target, deltaTime * 6.0F);
            if (Mathf.Abs(start - target) < CARD_SNAP_THRESHOLD)
            {
                start = target;
            }
        }
        return start;
    }

    public static float CardScaleLerSnap(float start, float target, float deltaTime)
    {
        if (start != target)
        {
            start = Mathf.Lerp(start, target, deltaTime * 7.5F);
            if (Mathf.Abs(start - target) < 0.003F)
            {
                start = target;
            }
        }
        return start;
    }

    //public static float uiLerpSnap(float startX, float targetX)
    //{
    //    if (startX != targetX)
    //    {
    //        startX = MathUtils.lerp(startX, targetX, Gdx.graphics.getDeltaTime() * 9.0F);
    //        if (Math.abs(startX - targetX) < Settings.UI_SNAP_THRESHOLD)
    //        {
    //            startX = targetX;
    //        }
    //    }
    //    return startX;
    //}

    //public static float orbLerpSnap(float startX, float targetX)
    //{
    //    if (startX != targetX)
    //    {
    //        startX = MathUtils.lerp(startX, targetX, Gdx.graphics.getDeltaTime() * 6.0F);
    //        if (Math.abs(startX - targetX) < Settings.UI_SNAP_THRESHOLD)
    //        {
    //            startX = targetX;
    //        }
    //    }
    //    return startX;
    //}

    //public static float mouseLerpSnap(float startX, float targetX)
    //{
    //    if (startX != targetX)
    //    {
    //        startX = MathUtils.lerp(startX, targetX, Gdx.graphics.getDeltaTime() * 20.0F);
    //        if (Math.abs(startX - targetX) < Settings.UI_SNAP_THRESHOLD)
    //        {
    //            startX = targetX;
    //        }
    //    }
    //    return startX;
    //}

    //public static float scaleLerpSnap(float startX, float targetX)
    //{
    //    if (startX != targetX)
    //    {
    //        startX = MathUtils.lerp(startX, targetX, Gdx.graphics.getDeltaTime() * 8.0F);
    //        if (Math.abs(startX - targetX) < 0.003F)
    //        {
    //            startX = targetX;
    //        }
    //    }
    //    return startX;
    //}

    //public static float fadeLerpSnap(float startX, float targetX)
    //{
    //    if (startX != targetX)
    //    {
    //        startX = MathUtils.lerp(startX, targetX, Gdx.graphics.getDeltaTime() * 12.0F);
    //        if (Math.abs(startX - targetX) < 0.01F)
    //        {
    //            startX = targetX;
    //        }
    //    }
    //    return startX;
    //}

    //public static float popLerpSnap(float startX, float targetX)
    //{
    //    if (startX != targetX)
    //    {
    //        startX = MathUtils.lerp(startX, targetX, Gdx.graphics.getDeltaTime() * 8.0F);
    //        if (Math.abs(startX - targetX) < 0.003F)
    //        {
    //            startX = targetX;
    //        }
    //    }
    //    return startX;
    //}

    //public static float angleLerpSnap(float startX, float targetX)
    //{
    //    if (startX != targetX)
    //    {
    //        startX = MathUtils.lerp(startX, targetX, Gdx.graphics.getDeltaTime() * 12.0F);
    //        if (Math.abs(startX - targetX) < 0.003F)
    //        {
    //            startX = targetX;
    //        }
    //    }
    //    return startX;
    //}

    //public static float slowColorLerpSnap(float startX, float targetX)
    //{
    //    if (startX != targetX)
    //    {
    //        startX = MathUtils.lerp(startX, targetX, Gdx.graphics.getDeltaTime() * 3.0F);
    //        if (Math.abs(startX - targetX) < 0.01F)
    //        {
    //            startX = targetX;
    //        }
    //    }
    //    return startX;
    //}

    //public static float scrollSnapLerpSpeed(float startX, float targetX)
    //{
    //    if (startX != targetX)
    //    {
    //        startX = MathUtils.lerp(startX, targetX, Gdx.graphics.getDeltaTime() * 10.0F);
    //        if (Math.abs(startX - targetX) < Settings.UI_SNAP_THRESHOLD)
    //        {
    //            startX = targetX;
    //        }
    //    }
    //    return startX;
    //}

    //public static float valueFromPercentBetween(float min, float max, float percent)
    //{
    //    float diff = max - min;
    //    return min + diff * percent;
    //}

    //public static float percentFromValueBetween(float min, float max, float value)
    //{
    //    float diff = max - min;
    //    float offset = value - min;
    //    return offset / diff;
    //}
}