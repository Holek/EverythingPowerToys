﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Community.PowerToys.Run.Plugin.Everything
{
    public class Settings
    {
        // Settings from PTR settings
        public Interop.NativeMethods.Sort Sort { get; set; }
        public uint Max { get; set; }
        public string Context { get; set; }
        public bool Copy { get; set; }
        public bool MatchPath { get; set; }
        public bool Preview { get; set; }
        public bool QueryText { get; set; }
        public bool RegEx { get; set; }
        public bool Updates { get; set; }

        // Get Filters from settings.toml
        internal Dictionary<string, string> Filters { get; } = new Dictionary<string, string>();
        internal void Getfilters()
        {
            string[] strArr;
            try { strArr = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "settings.toml")); }
            catch { return; }
            var culture = new System.Globalization.CultureInfo("en-US");
            foreach (string str in strArr)
            {
                if (str.Length == 0 || str[0] == '#') continue;
                string[] kv = str.Split('=', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (kv.Length != 2) continue;

                if (kv[0].Contains(':'))
                    Filters.TryAdd(kv[0].Split(':')[0].ToLowerInvariant(), kv[1]);
            }
        }
    }
}
