﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WhatsAppAPI.Models.ValidationMessages {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ErrorMsgEn {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMsgEn() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WhatsAppAPI.Models.ValidationMessages.ErrorMsgEn", typeof(ErrorMsgEn).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please Enter Correct Aadhar Number.
        /// </summary>
        public static string AadharNumber {
            get {
                return ResourceManager.GetString("AadharNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please Enter DateOfBirth In Format (DD-MM-YYYY).
        /// </summary>
        public static string DateOfBirth {
            get {
                return ResourceManager.GetString("DateOfBirth", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to FirstName Should Not Contain Numbers.
        /// </summary>
        public static string FirstNameNoNumbers {
            get {
                return ResourceManager.GetString("FirstNameNoNumbers", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to LastName Should Not Contain Numbers.
        /// </summary>
        public static string LastNameNoNumbers {
            get {
                return ResourceManager.GetString("LastNameNoNumbers", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please Enter Correct PAN Number.
        /// </summary>
        public static string PanNumber {
            get {
                return ResourceManager.GetString("PanNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This Value Is Required.
        /// </summary>
        public static string RequiredAttribute {
            get {
                return ResourceManager.GetString("RequiredAttribute", resourceCulture);
            }
        }
    }
}
