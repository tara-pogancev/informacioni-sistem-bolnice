using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;
using SIMS.Model;

namespace SIMS.ViewConverters
{
    class AddressConverter:MarkupExtension, IValueConverter
    {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        Address address= (Address)value;
        return address.Street + "," + address.Number + "," + address.City.Name;

    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        String addressValues = (String)value;
        Address address = new Address();
        address.Street = addressValues.Split(",")[0];
        address.Number = addressValues.Split(",")[1];
        address.City.Name = addressValues.Split(",")[2];
        return address;

    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
    }
    
    }
