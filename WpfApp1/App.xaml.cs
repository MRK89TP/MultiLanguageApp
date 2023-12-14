using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string ResourcesLanguages = "ResourcesDictionary/Languages/";
        private const string ResourcesThemes = "ResourcesDictionary/Themes/";

        public List<string> Languages = new List<string>();

        public App()
        {
           
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Initialize();
        }

        private void Initialize()
        {
            CheckLanguages();
            SetLanguage(Languages[0]);
        }

        internal void SetLanguage(string code)
        {
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri($"{ResourcesLanguages}{code}.xaml", UriKind.RelativeOrAbsolute) });
        }

        //Uguale si possono aggiungere altri metodi per cambire altre risorse: esempio una colorpalette
        internal void ChangePalette(string palette)
        {
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri($"{ResourcesThemes}{palette}.xaml", UriKind.RelativeOrAbsolute) });
        }

        private void CheckLanguages()
        {
            foreach (string str in Application.ResourceAssembly.GetManifestResourceNames())
            {
                Stream st = Application.ResourceAssembly.GetManifestResourceStream(str);
                using (ResourceReader resourceReader = new ResourceReader(st))
                {
                    foreach (DictionaryEntry resourceEntry in resourceReader)
                    {
                        if (resourceEntry.Key.ToString().StartsWith(ResourcesLanguages.ToLower()))
                        {
                            Languages.Add(Path.GetFileNameWithoutExtension(resourceEntry.Key.ToString()));
                        }     
                    }
                }
            }
        }
    }
}
