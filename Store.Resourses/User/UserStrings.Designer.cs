﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Store.Resourses.User {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class UserStrings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal UserStrings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Store.Resourses.User.UserStrings", typeof(UserStrings).Assembly);
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
        ///   Looks up a localized string similar to Email.
        /// </summary>
        public static string Email {
            get {
                return ResourceManager.GetString("Email", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Enter valid email.
        /// </summary>
        public static string Email_validation_error_message {
            get {
                return ResourceManager.GetString("Email_validation_error_message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Password.
        /// </summary>
        public static string Password {
            get {
                return ResourceManager.GetString("Password", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Enter correct password.
        /// </summary>
        public static string Password_validation_error_message {
            get {
                return ResourceManager.GetString("Password_validation_error_message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Remember me.
        /// </summary>
        public static string Remember_me {
            get {
                return ResourceManager.GetString("Remember_me", resourceCulture);
            }
        }
    }
}
