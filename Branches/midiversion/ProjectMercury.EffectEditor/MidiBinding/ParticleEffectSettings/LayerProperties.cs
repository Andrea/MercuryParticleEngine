using System;
using System.ComponentModel;


namespace BindingLibrary
{
    //[TypeDescriptionProvider(tepeof(MaxediaDescriptionProvider))]
    public class LayerProperties : IBindableObject
    {
       

        public const string ID = "A798D9A5-0FA0-4ecb-AD9B-380A5232B6A1";
   
        [Browsable(false)]
        public string BindingId
        {
            get { return ID; }
        }

        [Browsable(false)]
        public object BindingObject
        {
            get { return this; }
        }

      

        public override string ToString()
        {
            return String.Format("Xna Engine");
        }



    }
}
