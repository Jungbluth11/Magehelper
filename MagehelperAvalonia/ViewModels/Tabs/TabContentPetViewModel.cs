﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magehelper.Avalonia.ViewModels.Tabs
{
    public class TabContentPetViewModel
    {
        private static TabContentPetViewModel _instance = new();
        public static TabContentPetViewModel Instance => _instance;

        public TabContentPetViewModel()
        {
        }
    }
}