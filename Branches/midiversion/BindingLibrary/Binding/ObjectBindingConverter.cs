using System;
using System.ComponentModel;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace BindingLibrary
{

    public class ObjectBindingConverter
    {
        public ObjectBindingConverter()
        {
            SourceMax = 1;
            SourceMin = 0;
            TargetMax = 1;
            TargetMin = 0;

        }

        [Category("Source")]
        [DisplayName("Source Min")]
        public float SourceMin { get; set; }

        [Category("Source")]
        [DisplayName("Source Max")]
        public float SourceMax { get; set; }


        [Category("Target")]
        [DisplayName("Target Min")]
        public float TargetMin { get; set; }

        [Category("Target")]
        [DisplayName("Target Max")]
        public float TargetMax { get; set; }


        public bool CanConvert(Type source, Type target)
        {
            // check if source is convertible
            Type type = source.GetInterface("IConvertible");
            Type type1 = target.GetInterface("IConvertible");
            return type != null && type1 != null;
        }

        public object Convert(object value, Type targetType)
        {
            try
            {

                var f = (double) System.Convert.ChangeType(value, typeof (double));


                if ((SourceMin <= f) && (f <= SourceMax))
                {
                    float diff = SourceMax - SourceMin;

                    f = (f - SourceMin)/((diff != 0) ? diff : 1); // f is in [0, 1]

                    f = (f * (TargetMax - TargetMin) + TargetMin); // f is in [min, max]

                    return System.Convert.ChangeType(f, targetType);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }
    }
}