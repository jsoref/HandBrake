﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThemeImageConverter.cs" company="HandBrake Project (http://handbrake.fr)">
//   This file is part of the HandBrake source code - It may be used under the terms of the GNU General Public License.
// </copyright>
// <summary>
//   Handles the Image files for the Theme selected. 
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HandBrakeWPF.Converters
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Windows.Data;

    using Caliburn.Micro;

    using HandBrakeWPF.Services.Interfaces;

    public class ThemeImageConverter : IValueConverter
    {
        private readonly IUserSettingService userSettingService;

        private readonly bool isDarkTheme;

        public ThemeImageConverter()
        {
            this.userSettingService = IoC.Get<IUserSettingService>();
            this.isDarkTheme = this.userSettingService.GetUserSetting<bool>(UserSettingConstants.UseDarkTheme);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string image = parameter as string;
            if (!string.IsNullOrEmpty(image))
            {
                
                string directory = "Images/"; 
                if (image.Contains("/"))
                {
                    string[] components = image.Split('/');
                    string file = components.LastOrDefault();
                    directory = image.Replace(file, string.Empty);
                    image = file;
                }

                if (this.isDarkTheme)
                {
                    return directory + "Dark/" + image;
                }
                else
                {
                    return directory + "Light/" + image;
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
