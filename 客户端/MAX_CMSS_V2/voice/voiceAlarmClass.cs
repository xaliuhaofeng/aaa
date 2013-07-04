namespace MAX_CMSS_V2.voice
{
    using Logic;
    using SpeechLib;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
    using System.Threading;

    internal class voiceAlarmClass
    {
        private Queue<string> alarmQ = new Queue<string>();

        private void getAlarmVoice()
        {
            if (this.alarmQ.Count <= 0)
            {
                if (GlobalParams.cutList.Count > -1)
                {
                    if (GlobalParams.cutList.Count > 30)
                    {
                        GlobalParams.cutList.Clear();
                    }
                    else
                    {
                        foreach (string item in GlobalParams.cutList)
                        {
                            this.alarmQ.Enqueue(item.Clone().ToString());
                        }
                    }
                }
                if (GlobalParams.replayList.Count > -1)
                {
                    if (GlobalParams.replayList.Count > 30)
                    {
                        GlobalParams.replayList.Clear();
                    }
                    else
                    {
                        foreach (string item in GlobalParams.warnList)
                        {
                            this.alarmQ.Enqueue(item.Clone().ToString());
                        }
                    }
                }
                if (GlobalParams.warnList.Count > -1)
                {
                    if (GlobalParams.warnList.Count > 30)
                    {
                        GlobalParams.warnList.Clear();
                    }
                    else
                    {
                        foreach (string item in GlobalParams.warnList)
                        {
                            this.alarmQ.Enqueue(item.Clone().ToString());
                        }
                    }
                }
            }
        }

        public string getNextAlarm()
        {
            return this.alarmQ.Dequeue().ToString();
        }

        private static bool isHanzi(string s)
        {
            Regex rx = new Regex("^[一-龥]$");
            char Reflector0001 = s[0];
            return rx.IsMatch(Reflector0001.ToString());
        }

        public static void SaySome(string say)
        {
            SpVoiceClass sp = new SpVoiceClass();
            if (sp.AudioOutput != null)
            {
                if (isHanzi(say))
                {
                    ISpeechObjectTokens sps = sp.GetVoices("Language = 409", "");
                    sps = sp.GetVoices("Language = 804", "");
                    if (sps.Count > 0)
                    {
                        sp.Voice = sps.Item(0);
                        sp.Speak(say, SpeechVoiceSpeakFlags.SVSFDefault);
                    }
                    Marshal.ReleaseComObject(sp);
                }
                else
                {
                    sp.Voice = sp.GetVoices("Language = 409", "").Item(0);
                    sp.Speak(say, SpeechVoiceSpeakFlags.SVSFDefault);
                }
            }
        }

        public void voiceAlarmThread()
        {
            while (true)
            {
                Thread.Sleep(10);
                try
                {
                    SaySome(this.getNextAlarm().Trim());
                }
                finally
                {
                }
            }
        }
    }
}

