﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ArcTouch.Movies {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ArcTouch.Movies.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Alert.
        /// </summary>
        internal static string Alert {
            get {
                return ResourceManager.GetString("Alert", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 1f54bd990f1cdfb230adb312546d765d.
        /// </summary>
        internal static string ApiKey {
            get {
                return ResourceManager.GetString("ApiKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://api.themoviedb.org/3/configuration?api_key={0}.
        /// </summary>
        internal static string GetConfigurationsUrl {
            get {
                return ResourceManager.GetString("GetConfigurationsUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://api.themoviedb.org/3/genre/movie/list?api_key={0}&amp;language=en-US.
        /// </summary>
        internal static string GetGenresUrl {
            get {
                return ResourceManager.GetString("GetGenresUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://api.themoviedb.org/3/search/movie?page={0}&amp;api_key={1}&amp;language=en-US&amp;query={2}.
        /// </summary>
        internal static string GetSearchMoviesUrl {
            get {
                return ResourceManager.GetString("GetSearchMoviesUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://api.themoviedb.org/3/movie/upcoming?page={0}&amp;language=en-US&amp;api_key={1}.
        /// </summary>
        internal static string GetUpcomingMoviesUrl {
            get {
                return ResourceManager.GetString("GetUpcomingMoviesUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ok.
        /// </summary>
        internal static string Ok {
            get {
                return ResourceManager.GetString("Ok", resourceCulture);
            }
        }
    }
}
