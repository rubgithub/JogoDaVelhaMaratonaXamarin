using JogoDaVelhaMaratona.Game;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace JogoDaVelhaMaratona.Converter
{
    public class GameColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Color.Transparent;
            if((string)value == GameManage.PlayerSymbolX)
                return Color.Blue;
            else
                return Color.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
