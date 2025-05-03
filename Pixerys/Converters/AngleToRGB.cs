using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace Pixerys.Converters
{
    internal class AngleToRGB : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if(value is double inputAngle)
            {
                const double baseAngle = 0.9375;
                const double step = 1.875;
                const int maxIndex = 191;
                int index = (int)Math.Round((inputAngle - baseAngle) / step);
                index = Math.Clamp(index, 0, maxIndex);
                double snappedAngle = baseAngle + index * step;

                byte r = 0;
                byte g = 0;
                byte b = 0;

                switch (index)
                {
                    case < 15:
                        r = 31;
                        b = (byte)(15 - index);
                        g = 0;
                        break;
                    case 15:
                        r = 31;
                        b = 0;
                        g = 0;
                        break;
                    case < 47:
                        r = 31;
                        g = (byte)(index - 15);
                        b = 0;
                        break;
                    case 47:
                        r = 31;
                        g = 31;
                        b = 0;
                        break;
                    case < 78:
                        r = (byte)(78 - index);
                        g = 31;
                        b = 0;
                        break;
                    case 78:
                        r = 0;
                        g = 31;
                        b = 0;
                        break;
                    case < 109:
                        r = 0;
                        g = 31;
                        b = (byte)(index - 78);
                        break;
                    case 109:
                        r = 0;
                        g = 31;
                        b = 31;
                        break;
                    case < 140:
                        r = 0;
                        g = (byte)(140 - index);
                        b = 31;
                        break;
                    case 140:
                        r = 0;
                        g = 0;
                        b = 31;
                        break;
                    case < 171:
                        r = (byte)(index - 140);
                        g = 0;
                        b = 31;
                        break;
                    case 171:
                        r = 31;
                        g = 0;
                        b = 31;
                        break;
                    case < 192:
                        r = 31;
                        g = 0;
                        b = (byte)(202 - index);
                        break;
                }

                if(targetType == typeof(double))
                {
                    return snappedAngle;
                }
                if(targetType == typeof(string))
                {
                    return snappedAngle;
                }
                if(targetType == typeof(IBrush) || targetType == typeof(Brush))
                {
                    return new SolidColorBrush(Color.FromRgb((byte)(r << 3), (byte)(g << 3), (byte)(b << 3)));
                }
                if(targetType == typeof(Color))
                {
                    return new Color(255, (byte)(r << 3), (byte)(g << 3), (byte)(b << 3));
                }
            }
            return 0;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
