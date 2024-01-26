using System;

public class EventManager
{
    public static event Action<bool> OnKidsCanTalk;
    public static event Action<bool> OnAnswerTantangan;
    public static event Action<bool> WhatNpcGender;
    public static void BroadcastOnKidsCanTalk(bool canTalk)
    {
        OnKidsCanTalk?.Invoke(canTalk);
    }
    public static void BroadcastOnAnswerTantangan(bool answer)
    {
        OnKidsCanTalk?.Invoke(answer);
    }
    public static void BroadcastNpcGender(bool gender)
    {
        WhatNpcGender?.Invoke(gender);
    }
}
